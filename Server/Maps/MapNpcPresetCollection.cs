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
    public class MapNpcPresetCollection
    {
        DataManager.Maps.RawMap rawMap;
        List<MapNpcPreset> npcPresets;

        public MapNpcPresetCollection(DataManager.Maps.RawMap rawMap)
        {
            this.rawMap = rawMap;
            npcPresets = new List<MapNpcPreset>();
            for (int i = 0; i < rawMap.Npc.Count; i++)
            {
                npcPresets.Add(new MapNpcPreset(rawMap.Npc[i]));
            }
        }

        public int Count
        {
            get { return npcPresets.Count; }
        }

        public void Add(MapNpcPreset npcPreset)
        {
            npcPresets.Add(npcPreset);
            rawMap.Npc.Add(npcPreset.RawNpcPreset);
        }

        public void RemoveAt(int index)
        {
            npcPresets.RemoveAt(index);
            rawMap.Npc.RemoveAt(index);
        }

        public void Remove(MapNpcPreset npcPreset)
        {
            npcPresets.Remove(npcPreset);
            rawMap.Npc.Remove(npcPreset.RawNpcPreset);
        }

        public MapNpcPreset this[int index]
        {
            get
            {
                return npcPresets[index];
            }
            set
            {
                npcPresets[index] = value;
                rawMap.Npc[index] = value.RawNpcPreset;
            }
        }

        public IEnumerator<MapNpcPreset> GetEnumerator()
        {
            return npcPresets.GetEnumerator();
        }
    }
}
