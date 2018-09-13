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
    public class TimedEventCollection
    {
        List<ITimedEvent> timedEvents;

        public int Count
        {
            get { return timedEvents.Count; }
        }

        public TimedEventCollection()
        {
            timedEvents = new List<ITimedEvent>();
        }

        public void Add(ITimedEvent timedEvent)
        {
            timedEvents.Add(timedEvent);
        }

        public void Remove(ITimedEvent timedEvent)
        {
            timedEvents.Remove(timedEvent);
        }

        public ITimedEvent this[int index]
        {
            get { return timedEvents[index]; }
        }

        public ITimedEvent this[string id]
        {
            get
            {
                for (int i = 0; i < timedEvents.Count; i++)
                {
                    if (timedEvents[i].ID == id)
                    {
                        return timedEvents[i];
                    }
                }
                return null;
            }
        }
    }
}
