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
    public class MapDump : RawMap
    {
        int MaxMapNpcs = 20;
        int MaxMapItems = 30;

        #region Constructors

        public MapDump(string mapID, int maxMapNpcs, int maxMapItems)
            : base(mapID) {
            this.MaxMapNpcs = maxMapNpcs;
            this.MaxMapItems = maxMapItems;

            this.TempStatus = new List<MapStatus>();
            this.IsSaving = false;
            this.ActiveNpc = new MapNpc[MaxMapNpcs];
            this.ActiveItem = new MapItem[MaxMapItems];
            for (int i = 0; i < MaxMapNpcs; i++) {
                this.ActiveNpc[i] = new MapNpc(i);
            }
            for (int i = 0; i < MaxMapItems; i++) {
                this.ActiveItem[i] = new MapItem();
            }
            Darkness = -1;
            TimeLimit = -1;
        }

        #endregion Constructors

        #region Properties

        public int ActivationTime {
            get;
            set;
        }

        public bool ProcessingPaused {
            get;
            set;
        }

        public MapItem[] ActiveItem {
            get;
            set;
        }

        public MapNpc[] ActiveNpc {
            get;
            set;
        }

        public int SpawnMarker { get; set; }

        public int NpcSpawnWait { get; set; }

        public bool IsSaving {
            get;
            set;
        }

        public List<MapStatus> TempStatus { get; set; }

        public bool TempChange { get; set; }

        public byte CurrentWeather { get; set; }

        #endregion Properties
    }
}
