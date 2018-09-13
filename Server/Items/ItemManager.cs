using System;
using System.Collections.Generic;
using System.Text;
using PMDCP.DatabaseConnector.MySql;
using PMDCP.DatabaseConnector;
using Server.Database;
using Server.Zones;
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
    public class ItemManager
    {
        #region Fields

        static ItemCollection items;

        #endregion Fields

        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        #region Properties

        public static ItemCollection Items
        {
            get { return items; }
        }

        #endregion Properties

        #region Methods


        public static void Initialize()
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                //method for getting count
                string query = "SELECT COUNT(num) FROM item";
                DataColumnCollection row = dbConnection.Database.RetrieveRow(query);

                int count = row["COUNT(num)"].ValueString.ToInt();
                items = new ItemCollection(count);
            }
        }

        public static void LoadItem(int itemNum, PMDCP.DatabaseConnector.MySql.MySql database)
        {
            if (items.Items.ContainsKey(itemNum) == false)
                items.Items.Add(itemNum, new Item());
            string query = "SELECT name, " +
                "info, " +
                "pic, " +
                "item_type, " +
                "data1, " +
                "data2, " +
                "data3, " +
                "price, " +
                "stack_cap, " +
                "bound, " +
                "loseable, " +
                "rarity, " +
                "req_data1, " +
                "req_data2, " +
                "req_data3, " +
                "req_data4, " +
                "req_data5, " +
                "scripted_req, " +
                "add_hp, " +
                "add_pp, " +
                "add_atk, " +
                "add_def, " +
                "add_spatk, " +
                "add_spdef, " +
                "add_speed, " +
                "add_exp, " +
                "attack_speed, " +
                "recruit_bonus, " +
                "is_sandboxed, " +
                "zone_id " +
                "FROM item WHERE item.num = \'" + itemNum + "\'";

            DataColumnCollection row = database.RetrieveRow(query);
            if (row != null)
            {
                items[itemNum].Name = row["name"].ValueString;
                items[itemNum].Desc = row["info"].ValueString;
                items[itemNum].Pic = row["pic"].ValueString.ToInt();
                items[itemNum].Type = (Enums.ItemType)row["item_type"].ValueString.ToInt();
                items[itemNum].Data1 = row["data1"].ValueString.ToInt();
                items[itemNum].Data2 = row["data2"].ValueString.ToInt();
                items[itemNum].Data3 = row["data3"].ValueString.ToInt();
                items[itemNum].Price = row["price"].ValueString.ToInt();
                items[itemNum].StackCap = row["stack_cap"].ValueString.ToInt();
                items[itemNum].Bound = row["bound"].ValueString.ToBool();
                items[itemNum].Loseable = row["loseable"].ValueString.ToBool();
                items[itemNum].Rarity = row["rarity"].ValueString.ToInt();
                items[itemNum].ReqData1 = row["req_data1"].ValueString.ToInt();
                items[itemNum].ReqData2 = row["req_data2"].ValueString.ToInt();
                items[itemNum].ReqData3 = row["req_data3"].ValueString.ToInt();
                items[itemNum].ReqData4 = row["req_data4"].ValueString.ToInt();
                items[itemNum].ReqData5 = row["req_data5"].ValueString.ToInt();
                items[itemNum].ScriptedReq = row["scripted_req"].ValueString.ToInt();
                items[itemNum].AddHP = row["add_hp"].ValueString.ToInt();
                items[itemNum].AddPP = row["add_pp"].ValueString.ToInt();
                items[itemNum].AddAttack = row["add_atk"].ValueString.ToInt();
                items[itemNum].AddDefense = row["add_def"].ValueString.ToInt();
                items[itemNum].AddSpAtk = row["add_spatk"].ValueString.ToInt();
                items[itemNum].AddSpDef = row["add_spdef"].ValueString.ToInt();
                items[itemNum].AddSpeed = row["add_speed"].ValueString.ToInt();
                items[itemNum].AddEXP = row["add_exp"].ValueString.ToInt();
                items[itemNum].AttackSpeed = row["attack_speed"].ValueString.ToInt();
                items[itemNum].RecruitBonus = row["recruit_bonus"].ValueString.ToInt();
                items[itemNum].IsSandboxed = row["is_sandboxed"].ValueString.ToBool();
                items[itemNum].ZoneID = row["zone_id"].ValueString.ToInt();
            }
        }

        public static void LoadItems(object object1)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                try
                {
                    for (int i = 0; i < items.MaxItems; i++)
                    {
                        LoadItem(i, dbConnection.Database);
                        if (LoadUpdate != null)
                            LoadUpdate(null, new LoadingUpdateEventArgs(i, items.MaxItems));
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

        public static void SaveItem(int itemNum)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;
                database.BeginTransaction();
                database.ExecuteNonQuery("DELETE FROM item WHERE num = \'" + itemNum + "\'");

                database.UpdateOrInsert("item", new IDataColumn[] {
                database.CreateColumn(false, "num", itemNum.ToString()),
                database.CreateColumn(false, "name", items[itemNum].Name),
                database.CreateColumn(false, "info", items[itemNum].Desc),
                database.CreateColumn(false, "pic", items[itemNum].Pic.ToString()),
                database.CreateColumn(false, "item_type", ((int)items[itemNum].Type).ToString()),
                database.CreateColumn(false, "data1", items[itemNum].Data1.ToString()),
                database.CreateColumn(false, "data2", items[itemNum].Data2.ToString()),
                database.CreateColumn(false, "data3", items[itemNum].Data3.ToString()),
                database.CreateColumn(false, "price", items[itemNum].Price.ToString()),
                database.CreateColumn(false, "stack_cap", items[itemNum].StackCap.ToString()),
                database.CreateColumn(false, "bound", items[itemNum].Bound.ToIntString()),
                database.CreateColumn(false, "loseable", items[itemNum].Loseable.ToIntString()),
                database.CreateColumn(false, "rarity", items[itemNum].Rarity.ToString()),
                database.CreateColumn(false, "req_data1", items[itemNum].ReqData1.ToString()),
                database.CreateColumn(false, "req_data2", items[itemNum].ReqData2.ToString()),
                database.CreateColumn(false, "req_data3", items[itemNum].ReqData3.ToString()),
                database.CreateColumn(false, "req_data4", items[itemNum].ReqData4.ToString()),
                database.CreateColumn(false, "req_data5", items[itemNum].ReqData5.ToString()),
                database.CreateColumn(false, "scripted_req", items[itemNum].ScriptedReq.ToString()),
                database.CreateColumn(false, "add_hp", items[itemNum].AddHP.ToString()),
                database.CreateColumn(false, "add_pp", items[itemNum].AddPP.ToString()),
                database.CreateColumn(false, "add_atk", items[itemNum].AddAttack.ToString()),
                database.CreateColumn(false, "add_def", items[itemNum].AddDefense.ToString()),
                database.CreateColumn(false, "add_spatk", items[itemNum].AddSpAtk.ToString()),
                database.CreateColumn(false, "add_spdef", items[itemNum].AddSpDef.ToString()),
                database.CreateColumn(false, "add_speed", items[itemNum].AddSpeed.ToString()),
                database.CreateColumn(false, "add_exp", items[itemNum].AddEXP.ToString()),
                database.CreateColumn(false, "attack_speed", items[itemNum].AttackSpeed.ToString()),
                database.CreateColumn(false, "recruit_bonus", items[itemNum].RecruitBonus.ToString()),
                database.CreateColumn(false, "is_sandboxed", items[itemNum].IsSandboxed.ToIntString()),
                database.CreateColumn(false, "zone_id", items[itemNum].ZoneID.ToString())
            });

                database.EndTransaction();
            }
        }

        public static List<ZoneResource> LoadZoneResources(PMDCP.DatabaseConnector.MySql.MySql database, int zoneID)
        {
            var results = new List<ZoneResource>();

            var query = "SELECT num, name FROM item WHERE zone_id = " + zoneID;

            foreach (var row in database.RetrieveRowsEnumerable(query))
            {
                results.Add(new ZoneResource()
                {
                    Num = row["num"].ValueString.ToInt(),
                    Name = row["name"].ValueString,
                    Type = ZoneResourceType.Items
                });
            }

            return results;
        }

        #endregion Methods
    }
}
