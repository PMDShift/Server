// This file is part of Mystery Dungeon eXtended.

// Copyright (C) 2015 Pikablu, MDX Contributors, PMU Staff

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Server.Debug
{
    class ThreadCPUWatcher
    {
        [DllImport("kernel32.dll")]
        private static extern long GetThreadTimes
            (IntPtr threadHandle, out long createionTime,
             out long exitTime, out long kernelTime, out long userTime);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();

        bool watcherRunning = false;
        long percentage = -1;
        long cpuTimeStart;
        long cpuTimeEnd;

        public ThreadCPUWatcher() {
        }

        public ThreadCPUWatcher(Int16 nativeThreadID) {
        }

        public bool IsRunning {
            get { return watcherRunning; }
        }

        public long CPUusage {
            get { return percentage; }
        }

        public void StopWatcher(long elapsedMilliseconds) {
            watcherRunning = false;

            cpuTimeEnd = GetThreadTimes();

            long cpuDiff = (cpuTimeEnd - cpuTimeStart) / 10000;
            if (elapsedMilliseconds > 0) {
                percentage = (long)((cpuDiff / elapsedMilliseconds) * 100);
            } else {
                percentage = 0;
            }

            if (percentage > 100) percentage = 100;

            Thread.EndThreadAffinity(); 
        }

        public void Start() {
            System.Threading.Thread.BeginThreadAffinity();

            watcherRunning = true;

            cpuTimeStart = GetThreadTimes();
        }

        private long GetThreadTimes() {
            IntPtr threadHandle = GetCurrentThread();

            long notIntersting;
            long kernelTime, userTime;

            long retcode = GetThreadTimes
                (threadHandle, out notIntersting,
                out notIntersting, out kernelTime, out userTime);

            bool success = Convert.ToBoolean(retcode);
            if (!success)
                throw new Exception(string.Format
                ("failed to get timestamp. error code: {0}",
                retcode));

            long result = kernelTime + userTime;
            return result;
        }

    }
}
