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

namespace Server.Maps
{
    public class MapNpcPreset
    {
        DataManager.Maps.MapNpcPreset rawNpc;

        public DataManager.Maps.MapNpcPreset RawNpcPreset {
            get { return rawNpc; }
        }

        public int SpawnX {
            get { return rawNpc.SpawnX; }
            set { rawNpc.SpawnX = value; }
        }

        public int SpawnY {
            get { return rawNpc.SpawnY; }
            set { rawNpc.SpawnY = value; }
        }


        public int NpcNum {
            get { return rawNpc.NpcNum; }
            set { rawNpc.NpcNum = value; }
        }

        public int MinLevel {
            get { return rawNpc.MinLevel; }
            set { rawNpc.MinLevel = value; }
        }
        public int MaxLevel {
            get { return rawNpc.MaxLevel; }
            set { rawNpc.MaxLevel = value; }
        }

        public int AppearanceRate {
            get { return rawNpc.AppearanceRate; }
            set { rawNpc.AppearanceRate = value; }
        }

        public Enums.StatusAilment StartStatus {
            get { return (Enums.StatusAilment)rawNpc.StartStatus; }
            set { rawNpc.StartStatus = (int)value; }
        }
        public int StartStatusCounter {
            get { return rawNpc.StartStatusCounter; }
            set { rawNpc.StartStatusCounter = value; }
        }
        public int StartStatusChance {
            get { return rawNpc.StartStatusChance; }
            set { rawNpc.StartStatusChance = value; }
        }

        public MapNpcPreset() {
            this.rawNpc = new DataManager.Maps.MapNpcPreset();

            NpcNum = 0;
            SpawnX = -1;
            SpawnY = -1;
            MinLevel = -1;
            MaxLevel = -1;
            AppearanceRate = 100;
        }

        public MapNpcPreset(DataManager.Maps.MapNpcPreset rawNpc) {
            this.rawNpc = rawNpc;
        }
    }
}
