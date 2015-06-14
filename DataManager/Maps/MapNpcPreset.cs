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
    public class MapNpcPreset
    {
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }


        public int NpcNum { get; set; }

        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }

        public int AppearanceRate { get; set; }

        public int StartStatus { get; set; }
        public int StartStatusCounter { get; set; }
        public int StartStatusChance { get; set; }


        public MapNpcPreset() {
            NpcNum = 0;
            SpawnX = -1;
            SpawnY = -1;
            MinLevel = -1;
            MaxLevel = -1;
            AppearanceRate = 100;
        }
    }
}
