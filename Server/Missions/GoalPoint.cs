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
using Server.Maps;

namespace Server.Missions
{
    public class GoalPoint
    {
        public int JobListIndex { get; set; }
        public int GoalX { get; set; }
        public int GoalY { get; set; }

        public void DetermineGoalPoint(IMap map)
        {
            // We'll try 100 times to randomly select a tile
            for (int i = 0; i < 100; i++)
            {
                int x = Server.Math.Rand(0, map.MaxX + 1);
                int y = Server.Math.Rand(0, map.MaxY + 1);

                // Check if the tile is walk able
                if (map.Tile[x, y].Type == Enums.TileType.Walkable)
                {
                    GoalX = x;
                    GoalY = y;
                    return;
                }
            }

            // Didn't select anything, so now we'll just try to find a free tile
            //if (!selected)
            //{
            for (int Y = 0; Y <= map.MaxY; Y++)
            {
                for (int X = 0; X <= map.MaxX; X++)
                {
                    if (map.Tile[X, Y].Type == Enums.TileType.Walkable)
                    {
                        GoalX = X;
                        GoalY = Y;
                        return;
                    }
                }
            }
            //}
        }
    }
}
