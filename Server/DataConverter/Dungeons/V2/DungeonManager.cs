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

namespace Server.DataConverter.Dungeons.V2
{
    public class DungeonManager
    {
        public static void SaveDungeon(int dungeonNum, Dungeon dungeon)
        {
            
            string Filepath = IO.Paths.DungeonsFolder + "dungeon" + dungeonNum.ToString() + ".dat";
            
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(Filepath))
            {
                writer.WriteLine("DungeonData|V2");
                writer.WriteLine("Data|" + dungeon.Name + "|" + dungeon.AllowsRescue + "|");
                for (int i = 0; i < dungeon.StandardMaps.Count; i++)
                {
                    writer.WriteLine("SMap|" + dungeon.StandardMaps[i].Difficulty + "|" + dungeon.StandardMaps[i].IsBadGoalMap + "|"
                         + dungeon.StandardMaps[i].MapNum + "|");

                }
                for (int i = 0; i < dungeon.RandomMaps.Count; i++)
                {
                    writer.WriteLine("Map|" + dungeon.RandomMaps[i].Difficulty + "|" + dungeon.RandomMaps[i].IsBadGoalMap + "|"
                         + dungeon.RandomMaps[i].RDungeonIndex + "|" + dungeon.RandomMaps[i].RDungeonFloor + "|");

                }
            }
        }
    }
}
