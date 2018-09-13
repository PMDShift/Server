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

using Characters = DataManager.Characters;
namespace Server.Players
{
    public class InventoryItem
    {
        Characters.InventoryItem baseInventoryItem;

        bool updated = false;

        public Characters.InventoryItem BaseInventoryItem
        {
            get { return baseInventoryItem; }
        }

        public InventoryItem()
        {
            baseInventoryItem = new Characters.InventoryItem();
        }

        public InventoryItem(Characters.InventoryItem baseInventoryItem)
        {
            this.baseInventoryItem = baseInventoryItem;
        }

        public int Num
        {
            get { return baseInventoryItem.Num; }
            set
            {
                baseInventoryItem.Num = value;
                updated = true;
            }
        }

        public int Amount
        {
            get { return baseInventoryItem.Amount; }
            set
            {
                baseInventoryItem.Amount = value;
                updated = true;
            }
        }

        public bool Sticky
        {
            get { return baseInventoryItem.Sticky; }
            set
            {
                baseInventoryItem.Sticky = value;
                updated = true;
            }
        }

        public string Tag
        {
            get { return baseInventoryItem.Tag; }
            set
            {
                if (value == null)
                {
                    value = "";
                }
                baseInventoryItem.Tag = value;

                updated = true;
            }
        }

        public bool Updated
        {
            get { return updated; }
            internal set
            {
                updated = value;
            }
        }
    }
}
