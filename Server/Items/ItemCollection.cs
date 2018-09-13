using System;
using System.Collections.Generic;
using System.Text;
using PMDCP.Core;
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


namespace Server.Items
{
    public class ItemCollection
    {
        #region Fields

        ListPair<int, Item> items;
        int maxItems;

        #endregion Fields

        #region Constructors

        public ItemCollection(int maxItems)
        {
            if (maxItems == 0)
                maxItems = 50;
            this.maxItems = maxItems;
            items = new ListPair<int, Item>();
        }

        #endregion Constructors

        #region Properties

        public ListPair<int, Item> Items
        {
            get { return items; }
        }

        public int MaxItems
        {
            get { return maxItems; }
        }

        #endregion Properties

        #region Indexers

        public Item this[int index]
        {
            get { return items[index]; }
        }

        #endregion Indexers

        #region Methods

        public bool ContainsItem(int itemNum)
        {
            return items.ContainsKey(itemNum);
        }

        #endregion Methods
    }
}