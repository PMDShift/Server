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

namespace DataManager.Maps
{
    public class MapStatus
    {

        public MapStatus() {

        }

        public MapStatus(string name, int counter, string tag, int graphicEffect) {
            Name = name;
            Counter = counter;
            Tag = tag;
            GraphicEffect = graphicEffect;

        }

        public string Name { get; set; }
        public int Counter { get; set; }
        public String Tag { get; set; }
        public int GraphicEffect { get; set; }
    }
}
