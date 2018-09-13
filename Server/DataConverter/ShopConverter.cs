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
using System;

namespace Server.DataConverter
{
    /// <summary>
    /// Description of ShopConverter.
    /// </summary>
    public class ShopConverter
    {
        public static void ConvertV1ToV2(int num)
        {
            DataConverter.Shops.V2.Shop shopV2 = new Server.DataConverter.Shops.V2.Shop();

            DataConverter.Shops.V1.Shop shopV1 = Server.DataConverter.Shops.V1.ShopManager.LoadShop(num);

            shopV2.Name = shopV1.Name;
            shopV2.JoinSay = shopV1.JoinSay;
            shopV2.LeaveSay = shopV1.LeaveSay;
            int n = 0;
            for (int i = 1; i <= 7; i++)
            {
                for (int z = 1; z <= 66; z++)
                {
                    if (shopV1.Sections[i].Items[z].GetItem > 0 && n < 100)
                    {
                        shopV2.Items[n].GetItem = shopV1.Sections[i].Items[z].GetItem;
                        shopV2.Items[n].GiveItem = shopV1.Sections[i].Items[z].GiveItem;
                        shopV2.Items[n].GiveValue = shopV1.Sections[i].Items[z].GiveValue;
                        n++;
                    }
                }
            }

            Shops.V2.ShopManager.SaveShop(shopV2, num);
        }
    }
}
