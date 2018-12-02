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
    public class ActiveNpcCollection
    {
        DataManager.Maps.MapDump rawMap;
        MapNpc[] mapNpcs;

        public ActiveNpcCollection(DataManager.Maps.MapDump rawMap)
        {
            this.rawMap = rawMap;
            mapNpcs = new MapNpc[rawMap.ActiveNpc.Length];
            for (int i = 0; i < mapNpcs.Length; i++)
            {
                mapNpcs[i] = new MapNpc(rawMap.MapID, rawMap.ActiveNpc[i]);
            }
        }

        public int Length
        {
            get { return mapNpcs.Length; }
        }

        public MapNpc this[int index]
        {
            get
            {
                return mapNpcs[index];
            }
            set
            {
                mapNpcs[index] = value;
                rawMap.ActiveNpc[index] = value.RawNpc;
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return mapNpcs.GetEnumerator();
        }

        public IEnumerable<MapNpc> Enumerate()
        {
            foreach (var mapNpc in mapNpcs)
            {
                yield return mapNpc;
            }
        }
    }
}
