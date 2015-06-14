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

using PMDCP.Core;

namespace Server.Players
{
    public class Inventory
    {
        ListPair<int, InventoryItem> items;

        public Inventory(ListPair<int, DataManager.Characters.InventoryItem> loadedInventory) {
            items = new ListPair<int, InventoryItem>();
            for (int i = 0; i < loadedInventory.Count; i++) {
                items.Add(loadedInventory.KeyByIndex(i), new InventoryItem(loadedInventory.ValueByIndex(i)));
            }
        }

        public void Add(int slot, InventoryItem item) {
            items.Add(slot, item);
        }

        public InventoryItem this[int slot] {
            get {
                return items[slot];
            }
            set {
                items[slot] = value;
            }
        }

        public bool ContainsKey(int slot) {
            return items.ContainsKey(slot);
        }

        public int Count {
            get { return items.Count; }
        }
    }
}
