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
using System.IO;
using System.Text;

namespace Server.DataConverter.Items.V2
{
    class ItemManager
    {
        public static Item LoadItem(int itemNum)
        {
            Item item = new Item();
            string[] parse = null;
            using (System.IO.StreamReader read = new System.IO.StreamReader(Path.Combine(IO.Paths.ItemsFolder, "item" + itemNum + ".dat")))
            {
                while (!(read.EndOfStream))
                {
                    parse = read.ReadLine().Split('|');
                    switch (parse[0].ToLower())
                    {
                        case "itemdata":
                            if (parse[1].ToLower() != "v2")
                            {
                                read.Close();
                                return null;
                            }
                            break;
                        case "data":
                            item.Name = parse[1].Trim();
                            item.Desc = parse[2];
                            item.Pic = parse[3].ToInt();
                            item.Type = (Enums.ItemType)parse[4].ToInt();
                            item.Data1 = parse[5].ToInt();
                            item.Data2 = parse[6].ToInt();
                            item.Data3 = parse[7].ToInt();
                            

                            item.Price = parse[8].ToInt();
                            item.Stackable = parse[9].ToBool();
                            item.Bound = parse[10].ToBool();
                            item.Loseable = parse[11].ToBool();
                            item.Rarity = parse[12].ToInt();
                            break;
                        case "reqs":
                            {
                                item.AttackReq = parse[1].ToInt();
                                item.DefenseReq = parse[2].ToInt();
                                item.SpAtkReq = parse[3].ToInt();
                                item.SpDefReq = parse[4].ToInt();
                                item.SpeedReq = parse[5].ToInt();
                                item.ScriptedReq = parse[6].ToInt();
                            }
                            break;
                        case "stats":
                            {
                                item.AddHP = parse[1].ToInt();
                                item.AddPP = parse[2].ToInt();
                                item.AddAttack = parse[3].ToInt();
                                item.AddDefense = parse[4].ToInt();
                                item.AddSpAtk = parse[5].ToInt();
                                item.AddSpDef = parse[6].ToInt();
                                item.AddSpeed = parse[7].ToInt();
                                item.AddEXP = parse[8].ToInt();
                                item.AttackSpeed = parse[9].ToInt();

                                item.RecruitBonus = parse[10].ToInt();
                            }
                            break;
                    }
                }
            }
            return item;
        }

        public static void SaveItem(Item item, int itemNum)
        {
            string FileName = Path.Combine(IO.Paths.ItemsFolder, "item" + itemNum + ".dat");
            using (System.IO.StreamWriter Write = new System.IO.StreamWriter(FileName))
            {
                Write.WriteLine("ItemData|V2");
                Write.WriteLine("Data" + "|" + item.Name + "|" + item.Desc + "|" + item.Pic + "|" + (int)item.Type + "|" + item.Data1 + "|" + item.Data2 + "|" + item.Data3 + "|" + item.Price + "|" + item.Stackable + "|" + item.Bound + "|" + item.Loseable + "|" + item.Rarity + "|");
                Write.WriteLine("Reqs" + "|" + item.AttackReq + "|" + item.DefenseReq + "|" + item.SpAtkReq + "|" + item.SpDefReq + "|" + item.SpeedReq + "|" + item.ScriptedReq + "|");
                Write.WriteLine("Stats" + "|" + item.AddHP + "|" + item.AddPP + "|" + item.AddAttack + "|" + item.AddDefense + "|" + item.AddSpAtk + "|" + item.AddSpDef + "|" + item.AddSpeed + "|" + item.AddEXP + "|" + item.AttackSpeed + "|" + item.RecruitBonus + "|");
            }
        }
    }
}
