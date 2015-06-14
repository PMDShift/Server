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

namespace Server.DataConverter.Shops.V2
{
	/// <summary>
	/// Description of ShopManager.
	/// </summary>
	public class ShopManager
	{
		#region Methods

        

        public static Shop LoadShop(int shopNum) {
			Shop shop = new Shop();
            using (System.IO.StreamReader Read = new System.IO.StreamReader(IO.Paths.ShopsFolder + "shop" + shopNum + ".dat")) {
                string[] ShopInfo = Read.ReadLine().Split('|');
                if (ShopInfo[0] != "ShopData" || ShopInfo[1] != "V2") {
                		Read.Close();
                		return null;
                }
                
                string[] info;
                ShopInfo = Read.ReadLine().Split('|');
                shop.Name = ShopInfo[0];
                shop.JoinSay = ShopInfo[1];
                shop.LeaveSay = ShopInfo[2];
                //shop.FixesItems = ShopInfo[3].ToBool();
                //for (int i = 1; i <= 7; i++) {
                for (int z = 0; z < Constants.MAX_TRADES; z++) {
                    info = Read.ReadLine().Split('|');
                    shop.Items[z].GetItem = info[0].ToInt();
                    //shop.Items[z].GetValue = info[1].ToInt();
                    shop.Items[z].GiveItem = info[1].ToInt();
                    shop.Items[z].GiveValue = info[2].ToInt();
                }
                //}
            }
			return shop;
        }
        

        public static void SaveShop(Shop shop, int shopNum) {
            string FileName = IO.Paths.ShopsFolder + "shop" + shopNum + ".dat";
            using (System.IO.StreamWriter Write = new System.IO.StreamWriter(FileName)) {
				Write.WriteLine("ShopData|V2");
             Write.WriteLine(shop.Name + "|" + shop.JoinSay + "|" + shop.LeaveSay + "|");
             
                 for (int z = 0; z < Constants.MAX_TRADES; z++) {
                     Write.WriteLine(shop.Items[z].GetItem + "|" + shop.Items[z].GiveItem + "|" + shop.Items[z].GiveValue + "|");
                 }
             
            }
        }


        #endregion
	}
}
