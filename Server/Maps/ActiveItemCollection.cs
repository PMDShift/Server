﻿// This file is part of Mystery Dungeon eXtended.

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
    public class ActiveItemCollection
    {
        DataManager.Maps.MapDump rawMap;
        MapItem[] mapItems;

        public ActiveItemCollection(DataManager.Maps.MapDump rawMap)
        {
            this.rawMap = rawMap;
            mapItems = new MapItem[rawMap.ActiveItem.Length];
            for (int i = 0; i < mapItems.Length; i++)
            {
                mapItems[i] = new MapItem(rawMap.ActiveItem[i]);
                if (mapItems[i].Num == 0)
                {
                    mapItems[i].Num = -1;
                }
            }
        }

        public int Length
        {
            get { return mapItems.Length; }
        }

        public MapItem this[int index]
        {
            get
            {
                return mapItems[index];
            }
            set
            {
                mapItems[index] = value;
                rawMap.ActiveItem[index] = value.RawItem;
            }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return mapItems.GetEnumerator();
        }
    }
}
