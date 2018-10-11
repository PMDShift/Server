using System;
using System.Collections.Generic;
using System.Text;

using DataManager.Maps;
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


namespace Server.Maps
{
    public class MapBase
    {
        RawMap rawMap;

        #region Constructors

        public MapBase(RawMap rawMap)
        {
            this.rawMap = rawMap;

            Tile = new TileCollection(rawMap);
            Npc = new MapNpcPresetCollection(rawMap);
        }

        public MapBase(string mapID)
        {
            rawMap = new RawMap(mapID);

            rawMap.MaxX = 19;
            rawMap.MaxY = 14;
            rawMap.Tile = new DataManager.Maps.Tile[rawMap.MaxX + 1, rawMap.MaxY + 1];
        }

        #endregion Constructors

        #region Properties

        public Enums.GameplayMode GameplayMode
        {
            get { return (Enums.GameplayMode)rawMap.GameplayMode; }
            set { rawMap.GameplayMode = (int)value; }
        }

        public bool IsSandboxed
        {
            get { return rawMap.IsSandboxed; }
            set { rawMap.IsSandboxed = value; }
        }

        public int ZoneID
        {
            get { return rawMap.ZoneID; }
            set { rawMap.ZoneID = value; }
        }

        public string YouTubeMusicID
        {
            get { return rawMap.YouTubeMusicID; }
            set { rawMap.YouTubeMusicID = value; }
        }

        public int Down
        {
            get { return rawMap.Down; }
            set { rawMap.Down = value; }
        }

        public bool Indoors
        {
            get { return rawMap.Indoors; }
            set { rawMap.Indoors = value; }
        }

        public int Left
        {
            get { return rawMap.Left; }
            set { rawMap.Left = value; }
        }

        public string MapID
        {
            get { return rawMap.MapID; }
            set { rawMap.MapID = value; }
        }

        public int MaxX
        {
            get { return rawMap.MaxX; }
            set { rawMap.MaxX = value; }
        }

        public int MaxY
        {
            get { return rawMap.MaxY; }
            set { rawMap.MaxY = value; }
        }

        public Enums.MapMoral Moral
        {
            get { return (Enums.MapMoral)rawMap.Moral; }
            set { rawMap.Moral = (byte)value; }
        }

        public string Music
        {
            get { return rawMap.Music; }
            set { rawMap.Music = value; }
        }

        public string Name
        {
            get { return rawMap.Name; }
            set { rawMap.Name = value; }
        }

        public int MinNpcs
        {
            get { return rawMap.MinNpcs; }
            set { rawMap.MinNpcs = value; }
        }
        public int MaxNpcs
        {
            get { return rawMap.MaxNpcs; }
            set { rawMap.MaxNpcs = value; }
        }

        public int NpcSpawnTime
        {
            get { return rawMap.NpcSpawnTime; }
            set { rawMap.NpcSpawnTime = value; }
        }

        public MapNpcPresetCollection Npc
        {
            get;
            set;
        }

        public int OriginalDarkness
        {
            get { return rawMap.Darkness; }
            set
            {
                rawMap.Darkness = value;
                Darkness = value;
            }
        }

        public int Darkness
        {
            get;
            set;
        }

        public int TimeLimit
        {
            get { return rawMap.TimeLimit; }
            set { rawMap.TimeLimit = value; }
        }

        public int Revision
        {
            get { return rawMap.Revision; }
            set { rawMap.Revision = value; }
        }

        public int Right
        {
            get { return rawMap.Right; }
            set { rawMap.Right = value; }
        }

        public TileCollection Tile
        {
            get;
            set;
        }

        public int Up
        {
            get { return rawMap.Up; }
            set { rawMap.Up = value; }
        }

        public virtual Enums.Weather Weather
        {
            get { return (Enums.Weather)rawMap.Weather; }
            set { rawMap.Weather = (byte)value; }
        }

        public int DungeonIndex
        {
            get { return rawMap.DungeonIndex; }
            set { rawMap.DungeonIndex = value; }
        }

        public bool HungerEnabled
        {
            get { return rawMap.HungerEnabled; }
            set { rawMap.HungerEnabled = value; }
        }

        public bool RecruitEnabled
        {
            get { return rawMap.RecruitEnabled; }
            set { rawMap.RecruitEnabled = value; }
        }

        public bool ExpEnabled
        {
            get { return rawMap.ExpEnabled; }
            set { rawMap.ExpEnabled = value; }
        }
        #endregion Properties
    }
}
