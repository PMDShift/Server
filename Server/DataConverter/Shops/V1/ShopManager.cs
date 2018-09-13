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
using System.Text;

namespace Server.DataConverter.Shops.V1
{
    /// <summary>
    /// Description of ShopManager.
    /// </summary>
    public class ShopManager
    {
        #region Methods



        public static Shop LoadShop(int shopNum)
        {
            Shop shop = new Shop();
            using (System.IO.StreamReader Read = new System.IO.StreamReader(IO.Paths.ShopsFolder + "shop" + shopNum + ".dat"))
            {
                string[] ShopInfo = Read.ReadLine().Split('|');

                string[] info;
                shop.Name = ShopInfo[0];
                shop.JoinSay = ShopInfo[1];
                shop.LeaveSay = ShopInfo[2];
                shop.FixesItems = ShopInfo[3].ToBool();
                for (int i = 1; i <= 7; i++)
                {
                    for (int z = 1; z <= 66; z++)
                    {
                        info = Read.ReadLine().Split('|');
                        shop.Sections[i].Items[z].GetItem = info[0].ToInt();
                        shop.Sections[i].Items[z].GetValue = info[1].ToInt();
                        shop.Sections[i].Items[z].GiveItem = info[2].ToInt();
                        shop.Sections[i].Items[z].GiveValue = info[3].ToInt();
                    }
                }
            }
            return shop;
        }


        public static void SaveShop(Shop shop, int shopNum)
        {
            using (System.IO.StreamWriter Write = new System.IO.StreamWriter(IO.Paths.ShopsFolder + "shop" + shopNum + ".dat"))
            {
                Write.WriteLine(shop.Name + "|" + shop.JoinSay + "|" + shop.LeaveSay + "|" + shop.FixesItems + "|");
                for (int i = 1; i <= 7; i++)
                {
                    for (int z = 1; z <= 66; z++)
                    {
                        Write.WriteLine(shop.Sections[i].Items[z].GetItem + "|" + shop.Sections[i].Items[z].GetValue + "|" + shop.Sections[i].Items[z].GiveItem + "|" + shop.Sections[i].Items[z].GiveValue);
                    }
                }
            }
        }

        #endregion
    }
}
