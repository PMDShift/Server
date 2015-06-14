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

namespace Server
{
    public class TickCount
    {
        int tick;

        public int Tick {
            get { return tick; }
        }

        public TickCount(int tick) {
            this.tick = tick;
        }

        public bool Elapsed(int storedTick, int interval) {
            int tickSign = System.Math.Sign(this.tick);
            int storedTickSign = System.Math.Sign(storedTick);

            if (tickSign == -1 && storedTickSign == -1) {
                return (this.tick > storedTick + interval);
            } else if (tickSign == 1 && storedTickSign == 1) {
                return (this.tick > storedTick + interval);
            } else if (tickSign == -1 && storedTickSign == 1) {
                int remainder = Int32.MaxValue - storedTick;
                if (remainder > interval) {
                    return true;
                }
                return (this.tick > Int32.MinValue + (interval - remainder));
            }

            return (this.tick > storedTick + interval);
        }

        public bool Elapsed(TickCount storedTick, int interval) {
            if (storedTick != null) {
                return Elapsed(storedTick.Tick, interval);
            } else {
                return true;
            }
        }

        public static TickCount operator +(TickCount tick1, TickCount tick2) {
            int overflowVal = Int32.MaxValue - tick2.Tick - tick1.tick;
            int newTick;
            if (overflowVal < 0) {
                newTick = System.Math.Abs(overflowVal);
            } else {
                newTick = tick1.Tick + tick2.Tick;
            }
            return new TickCount(newTick);
        }
    }
}
