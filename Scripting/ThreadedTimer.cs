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
using System.Threading;

namespace Server.Scripting
{
    public class ThreadedTimer
    {
        #region Fields

        bool autoStart;
        bool autoStop;
        int hitCount;
        int interval;
        string methodName;
        object[] @params;
        Timer timer;

        #endregion Fields

        #region Constructors

        public ThreadedTimer(string methodName, int inverval, bool autoStart, bool autoStop, params object[] param) {
            this.methodName = methodName;
            this.interval = inverval;
            this.autoStart = autoStart;
            this.autoStop = autoStop;
            this.@params = param;
            if (autoStart) {
                timer = new Timer(new TimerCallback(TimerCallback), null, 0, inverval);
            } else {
                timer = new Timer(new TimerCallback(TimerCallback), null, Timeout.Infinite, Timeout.Infinite);
            }
        }

        #endregion Constructors

        #region Methods

        public int GetHitCount() {
            return hitCount;
        }

        public void Start() {
            timer.Change(interval, interval);
        }

        public void Stop() {
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void TimerCallback(object obj) {
            if (autoStop)
                Stop();
            ScriptManager.InvokeSubSimple(methodName, @params);
            hitCount++;
        }

        public void UpdateParams(params object[] param) {
            this.@params = param;
        }

        internal void Dispose() {
            timer.Dispose();
        }

        #endregion Methods
    }
}
