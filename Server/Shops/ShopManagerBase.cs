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
using PMDCP.DatabaseConnector.MySql;
using PMDCP.DatabaseConnector;
using Server.Database;
using Server.Zones;

namespace Server.Shops
{
    public class ShopManagerBase
    {
        static ShopCollection shops;

        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        public static ShopCollection Shops
        {
            get { return shops; }
        }

        public static void Initialize()
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                //method for getting count
                string query = "SELECT COUNT(num) FROM shop";
                DataColumnCollection row = dbConnection.Database.RetrieveRow(query);

                int count = row["COUNT(num)"].ValueString.ToInt();
                shops = new ShopCollection(count);
            }
        }

        #region Loading

        public static void LoadShops(object object1)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                try
                {
                    for (int i = 1; i <= shops.MaxShops; i++)
                    {
                        LoadShop(i, dbConnection.Database);
                        if (LoadUpdate != null)
                            LoadUpdate(null, new LoadingUpdateEventArgs(i, shops.MaxShops));
                    }
                    if (LoadComplete != null)
                        LoadComplete(null, null);
                }
                catch (Exception ex)
                {
                    Exceptions.ErrorLogger.WriteToErrorLog(ex);
                }
            }
        }

        public static void LoadShop(int shopNum, PMDCP.DatabaseConnector.MySql.MySql database)
        {
            if (shops.Shops.ContainsKey(shopNum) == false)
                shops.Shops.Add(shopNum, new Shop());

            string query = "SELECT name, " +
                "greeting, " +
                "farewell, " +
                "is_sandboxed, " +
                "zone_id " +
                "FROM shop WHERE shop.num = \'" + shopNum + "\'";

            DataColumnCollection row = database.RetrieveRow(query);
            if (row != null)
            {
                shops[shopNum].Name = row["name"].ValueString;
                shops[shopNum].JoinSay = row["greeting"].ValueString;
                shops[shopNum].LeaveSay = row["farewell"].ValueString;
                shops[shopNum].IsSandboxed = row["is_sandboxed"].ValueString.ToBool();
                shops[shopNum].ZoneID = row["zone_id"].ValueString.ToInt();
            }

            query = "SELECT trade_num, " +
                "item, " +
                "cost_num, " +
                "cost_val " +
                "FROM shop_trade WHERE shop_trade.num = \'" + shopNum + "\'";

            foreach (DataColumnCollection columnCollection in database.RetrieveRowsEnumerable(query))
            {
                int tradeNum = columnCollection["trade_num"].ValueString.ToInt();

                shops[shopNum].Items[tradeNum].GetItem = columnCollection["item"].ValueString.ToInt();
                shops[shopNum].Items[tradeNum].GiveItem = columnCollection["cost_num"].ValueString.ToInt();
                shops[shopNum].Items[tradeNum].GiveValue = columnCollection["cost_val"].ValueString.ToInt();
            }
        }


        #endregion

        #region Saving


        public static void SaveShop(int shopNum)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;
                database.BeginTransaction();

                database.ExecuteNonQuery("DELETE FROM shop WHERE num = \'" + shopNum + "\'");
                database.ExecuteNonQuery("DELETE FROM shop_trade WHERE num = \'" + shopNum + "\'");

                database.UpdateOrInsert("shop", new IDataColumn[] {
                    database.CreateColumn(false, "num", shopNum.ToString()),
                    database.CreateColumn(false, "name", shops[shopNum].Name),
                    database.CreateColumn(false, "greeting", shops[shopNum].JoinSay),
                    database.CreateColumn(false, "farewell", shops[shopNum].LeaveSay),
                    database.CreateColumn(false, "is_sandboxed", shops[shopNum].IsSandboxed.ToIntString()),
                    database.CreateColumn(false, "zone_id", shops[shopNum].ZoneID.ToString()),
                });

                for (int i = 0; i < Constants.MAX_TRADES; i++)
                {
                    database.UpdateOrInsert("shop_trade", new IDataColumn[] {
                    database.CreateColumn(false, "num", shopNum.ToString()),
                    database.CreateColumn(false, "trade_num", i.ToString()),
                    database.CreateColumn(false, "item", shops[shopNum].Items[i].GetItem.ToString()),
                    database.CreateColumn(false, "cost_num", shops[shopNum].Items[i].GiveItem.ToString()),
                    database.CreateColumn(false, "cost_val", shops[shopNum].Items[i].GiveValue.ToString())
                });
                }
                database.EndTransaction();
            }
        }

        #endregion

        public static List<ZoneResource> LoadZoneResources(PMDCP.DatabaseConnector.MySql.MySql database, int zoneID)
        {
            var results = new List<ZoneResource>();

            var query = "SELECT num, name FROM shop WHERE zone_id = " + zoneID;

            foreach (var row in database.RetrieveRowsEnumerable(query))
            {
                results.Add(new ZoneResource()
                {
                    Num = row["num"].ValueString.ToInt(),
                    Name = row["name"].ValueString,
                    Type = ZoneResourceType.Shops
                });
            }

            return results;
        }
    }
}
