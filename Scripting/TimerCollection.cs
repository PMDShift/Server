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
using System.Text;
using PMDCP.Core;

namespace Server.Scripting
{
    public class TimerCollection
    {
        ListPair<string, ThreadedTimer> timers;

        public TimerCollection() {
            timers = new ListPair<string, ThreadedTimer>();
        }

        public ThreadedTimer CreateTimer(string timerID, string methodName, int interval, bool autoStart, bool autoStop, params object[] param) {
            ThreadedTimer timer = new ThreadedTimer(methodName, interval, autoStart, autoStop, param);
            timers.Add(timerID, timer);
            return timer;
        }

        public ThreadedTimer GetTimer(string timerID) {
            if (timers.ContainsKey(timerID)) {
                return timers[timerID];
            } else {
                return null;
            }
        }

        public void RemoveTimer(string timerID) {
            if (TimerExists(timerID)) {
                timers[timerID].Stop();
                timers[timerID].Dispose();
                timers.RemoveAtKey(timerID);
            }
        }

        public bool TimerExists(string timerID) {
            return timers.ContainsKey(timerID);
        }

        public void RemoveAllTimers() {
            for (int i = timers.Count - 1; i > 0; i--) {
                timers[timers.KeyByIndex(i)].Stop();
                timers[timers.KeyByIndex(i)].Dispose();
                timers.RemoveAt(i);
            }
        }
    }
}
