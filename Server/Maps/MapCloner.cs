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
    public class MapCloner
    {
        public static void CloneMapTiles(IMap sourceMap, IMap destinationMap)
        {
            destinationMap.Name = sourceMap.Name;
            CloneMapTileProperties(sourceMap, destinationMap);
            for (int Y = 0; Y <= destinationMap.MaxY; Y++)
            {
                for (int X = 0; X <= destinationMap.MaxX; X++)
                {
                    if (destinationMap.Tile[X, Y] == null)
                    {
                        destinationMap.Tile[X, Y] = new Tile(new DataManager.Maps.Tile());
                    }
                    CloneTile(sourceMap, X, Y, destinationMap.Tile[X, Y]);
                }
            }
        }

        public static void CloneTile(IMap map, int x, int y, Tile tile)
        {
            tile.Ground = map.Tile[x, y].Ground;
            tile.GroundAnim = map.Tile[x, y].GroundAnim;
            tile.Mask = map.Tile[x, y].Mask;
            tile.Anim = map.Tile[x, y].Anim;
            tile.Mask2 = map.Tile[x, y].Mask2;
            tile.M2Anim = map.Tile[x, y].M2Anim;
            tile.Mask3 = map.Tile[x, y].Mask3;
            tile.M3Anim = map.Tile[x, y].M3Anim;
            tile.Mask4 = map.Tile[x, y].Mask4;
            tile.M4Anim = map.Tile[x, y].M4Anim;
            tile.Mask5 = map.Tile[x, y].Mask5;
            tile.M5Anim = map.Tile[x, y].M5Anim;
            tile.Fringe = map.Tile[x, y].Fringe;
            tile.FAnim = map.Tile[x, y].FAnim;
            tile.Fringe2 = map.Tile[x, y].Fringe2;
            tile.F2Anim = map.Tile[x, y].F2Anim;
            tile.Fringe3 = map.Tile[x, y].Fringe3;
            tile.F3Anim = map.Tile[x, y].F3Anim;
            tile.Fringe4 = map.Tile[x, y].Fringe4;
            tile.F4Anim = map.Tile[x, y].F4Anim;
            tile.Fringe5 = map.Tile[x, y].Fringe5;
            tile.F5Anim = map.Tile[x, y].F5Anim;
            tile.Type = map.Tile[x, y].Type;
            tile.Data1 = map.Tile[x, y].Data1;
            tile.Data2 = map.Tile[x, y].Data2;
            tile.Data3 = map.Tile[x, y].Data3;
            tile.String1 = map.Tile[x, y].String1;
            tile.String2 = map.Tile[x, y].String2;
            tile.String3 = map.Tile[x, y].String3;
            tile.RDungeonMapValue = map.Tile[x, y].RDungeonMapValue;
            tile.GroundSet = map.Tile[x, y].GroundSet;
            tile.GroundAnimSet = map.Tile[x, y].GroundAnimSet;
            tile.MaskSet = map.Tile[x, y].MaskSet;
            tile.AnimSet = map.Tile[x, y].AnimSet;
            tile.Mask2Set = map.Tile[x, y].Mask2Set;
            tile.M2AnimSet = map.Tile[x, y].M2AnimSet;
            tile.Mask3Set = map.Tile[x, y].Mask3Set;
            tile.M3AnimSet = map.Tile[x, y].M3AnimSet;
            tile.Mask4Set = map.Tile[x, y].Mask4Set;
            tile.M4AnimSet = map.Tile[x, y].M4AnimSet;
            tile.Mask5Set = map.Tile[x, y].Mask5Set;
            tile.M5AnimSet = map.Tile[x, y].M5AnimSet;
            tile.FringeSet = map.Tile[x, y].FringeSet;
            tile.FAnimSet = map.Tile[x, y].FAnimSet;
            tile.Fringe2Set = map.Tile[x, y].Fringe2Set;
            tile.F2AnimSet = map.Tile[x, y].F2AnimSet;
            tile.Fringe3Set = map.Tile[x, y].Fringe3Set;
            tile.F3AnimSet = map.Tile[x, y].F3AnimSet;
            tile.Fringe4Set = map.Tile[x, y].Fringe4Set;
            tile.F4AnimSet = map.Tile[x, y].F4AnimSet;
            tile.Fringe5Set = map.Tile[x, y].Fringe5Set;
            tile.F5AnimSet = map.Tile[x, y].F5AnimSet;
        }

        public static void CloneMapNpcs(IMap sourceMap, IMap destinationMap)
        {
            for (int i = 0; i < sourceMap.Npc.Count; i++)
            {
                MapNpcPreset newNpc = new MapNpcPreset();
                newNpc.NpcNum = sourceMap.Npc[i].NpcNum;
                newNpc.MinLevel = sourceMap.Npc[i].MinLevel;
                newNpc.MaxLevel = sourceMap.Npc[i].MaxLevel;
                newNpc.SpawnX = sourceMap.Npc[i].SpawnX;
                newNpc.SpawnY = sourceMap.Npc[i].SpawnY;
                newNpc.AppearanceRate = sourceMap.Npc[i].AppearanceRate;
                newNpc.StartStatus = sourceMap.Npc[i].StartStatus;
                newNpc.StartStatusCounter = sourceMap.Npc[i].StartStatusCounter;
                newNpc.StartStatusChance = sourceMap.Npc[i].StartStatusChance;

                destinationMap.Npc.Add(newNpc);
            }
        }

        public static void CloneMapGeneralProperties(IMap sourceMap, IMap destinationMap)
        {
            destinationMap.Up = sourceMap.Up;
            destinationMap.Down = sourceMap.Down;
            destinationMap.Left = sourceMap.Left;
            destinationMap.Right = sourceMap.Right;
            destinationMap.HungerEnabled = sourceMap.HungerEnabled;
            destinationMap.RecruitEnabled = sourceMap.RecruitEnabled;
            destinationMap.ExpEnabled = sourceMap.ExpEnabled;
            destinationMap.TimeLimit = sourceMap.TimeLimit;
            destinationMap.Indoors = sourceMap.Indoors;
            destinationMap.Moral = sourceMap.Moral;
            destinationMap.Music = sourceMap.Music;
            destinationMap.YouTubeMusicID = sourceMap.YouTubeMusicID;
            destinationMap.Name = sourceMap.Name;
            destinationMap.OriginalDarkness = sourceMap.OriginalDarkness;
            destinationMap.Weather = sourceMap.Weather;
            destinationMap.DungeonIndex = sourceMap.DungeonIndex;
            destinationMap.MinNpcs = sourceMap.MinNpcs;
            destinationMap.MaxNpcs = sourceMap.MaxNpcs;
            destinationMap.NpcSpawnTime = sourceMap.NpcSpawnTime;
        }

        public static void CloneMapTileProperties(IMap sourceMap, IMap destinationMap)
        {
            destinationMap.Tile = new TileCollection(destinationMap.BaseMap, sourceMap.MaxX, sourceMap.MaxY);
        }

        public static InstancedMap CreateInstancedMap(IMap sourceMap)
        {
            DataManager.Maps.InstancedMap dmInstancedMap = new DataManager.Maps.InstancedMap(MapManager.GenerateMapID("i"));
            InstancedMap iMap = new InstancedMap(dmInstancedMap);
            CloneMapTileProperties(sourceMap, iMap);
            CloneMapTiles(sourceMap, iMap);
            CloneMapGeneralProperties(sourceMap, iMap);
            CloneMapNpcs(sourceMap, iMap);
            return iMap;
        }
    }
}
