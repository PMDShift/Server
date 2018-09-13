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
using Server.RDungeons;

namespace Server.DataConverter.RDungeons.V1
{
    public class RDungeonFloor
    {
        public Enums.Weather Weather { get; set; }
        public int WeatherIntensity { get; set; }

        public int[] Npc { get; set; }
        public List<int> Traps { get; set; }

        public int ItemSpawnRate { get; set; }
        public int[] Items { get; set; }

        public Enums.RFloorGoalType GoalType { get; set; }

        public int GoalMap { get; set; }
        public int GoalX { get; set; }
        public int GoalY { get; set; }

        public string Music { get; set; }

        public RDungeonFloor()
        {
            Traps = new List<int>();
            Npc = new int[15];
            Items = new int[8];
        }
    }
}
