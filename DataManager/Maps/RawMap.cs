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
    public class RawMap
    {
        public RawMap(string mapID) {
            MapID = mapID;
            Name = "";
            Music = "";
            Npc = new List<MapNpcPreset>();
            MaxX = 19;
            MaxY = 14;
            Tile = new Tile[MaxX + 1, MaxY + 1];
        }

        public int Down {
            get;
            set;
        }

        public bool Indoors {
            get;
            set;
        }

        public int Left {
            get;
            set;
        }

        public string MapID {
            get;
            set;
        }

        public int MaxX {
            get;
            set;
        }

        public int MaxY {
            get;
            set;
        }

        public byte Moral {
            get;
            set;
        }

        public string Music {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }

        public int MinNpcs { get; set; }
        public int MaxNpcs { get; set; }

        public int NpcSpawnTime { get; set; }

        public List<MapNpcPreset> Npc {
            get;
            set;
        }

        public int Darkness {
            get;
            set;
        }

        public int TimeLimit {
            get;
            set;
        }

        public int Revision {
            get;
            set;
        }

        public int Right {
            get;
            set;
        }

        public Tile[,] Tile {
            get;
            set;
        }

        public int Up {
            get;
            set;
        }

        public virtual byte Weather {
            get;
            set;
        }

        public int DungeonIndex {
            get;
            set;
        }

        public bool HungerEnabled {
            get;
            set;
        }

        public bool RecruitEnabled {
            get;
            set;
        }

        public bool ExpEnabled {
            get;
            set;
        }
    }
}
