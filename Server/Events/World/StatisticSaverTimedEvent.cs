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

namespace Server.Events.World
{
    class StatisticSaverTimedEvent : ITimedEvent
    {
        int interval;
        TickCount storedTime;
        string id;

        public StatisticSaverTimedEvent(string id)
        {
            this.id = id;
        }

        public bool TimeElapsed(TickCount currentTick)
        {
            if (currentTick.Elapsed(storedTime, interval))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TickCount StoredTime
        {
            get { return storedTime; }
        }

        public int Interval
        {
            get { return interval; }
        }

        public void OnTimeElapsed(TickCount currentTick)
        {
            Server.Statistics.PerformanceStatistics.DumpStatistics();

            storedTime = currentTick;
        }

        public void SetInterval(TickCount currentTick, int interval)
        {
            this.interval = interval;
            storedTime = currentTick;
        }

        public string ID
        {
            get { return id; }
        }
    }
}
