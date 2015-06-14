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

namespace Server.DataConverter
{
    public class DungeonConverter
    {
        public static void ConvertV1toV2(int num)
        {
            Dungeons.V1.Dungeon dungeonV1 = Dungeons.V1.DungeonManager.LoadDungeon(num);
            Dungeons.V2.Dungeon dungeonV2 = new Dungeons.V2.Dungeon();

            dungeonV2.Name = dungeonV1.Name;
            dungeonV2.AllowsRescue = true;
            foreach (Dungeons.V1.DungeonMap dungeonMap in dungeonV1.Maps)
            {
                Dungeons.V2.StandardDungeonMap standardMap = new Dungeons.V2.StandardDungeonMap();
                standardMap.Difficulty = dungeonMap.Difficulty;
                standardMap.IsBadGoalMap = dungeonMap.IsBadGoalMap;
                standardMap.MapNum = dungeonMap.MapNumber;
                dungeonV2.StandardMaps.Add(standardMap);
            }

            Dungeons.V2.DungeonManager.SaveDungeon(num, dungeonV2);
        }
    }
}
