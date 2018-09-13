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

namespace Server.Maps
{
    public class MapStatus
    {
        DataManager.Maps.MapStatus rawStatus;


        public DataManager.Maps.MapStatus RawStatus
        {
            get
            {
                return rawStatus;
            }
        }

        public MapStatus(DataManager.Maps.MapStatus rawStatus)
        {
            this.rawStatus = rawStatus;
        }

        public MapStatus(string name, int counter, string tag, int graphicEffect)
            : this(new DataManager.Maps.MapStatus(name, counter, tag, graphicEffect))
        {
        }

        public string Name
        {
            get { return rawStatus.Name; }
            set { rawStatus.Name = value; }
        }

        public int Counter
        {
            get { return rawStatus.Counter; }
            set { rawStatus.Counter = value; }
        }

        public String Tag
        {
            get { return rawStatus.Tag; }
            set { rawStatus.Tag = value; }
        }

        public int GraphicEffect
        {
            get { return rawStatus.GraphicEffect; }
            set { rawStatus.GraphicEffect = value; }
        }
    }
}
