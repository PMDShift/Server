using System;
using System.Collections.Generic;
using System.Text;
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


namespace Server.DataConverter.Npcs.V6
{
    public class NpcManager
    {
        #region Methods

        public static Npc LoadNpc(int npcNum)
        {
            Npc npc = new Npc();
            string FileName = IO.Paths.NpcsFolder + "npc" + npcNum + ".dat";
            string s;
            string[] parse;
            using (System.IO.StreamReader read = new System.IO.StreamReader(FileName))
            {
                while (!(read.EndOfStream))
                {
                    s = read.ReadLine();
                    parse = s.Split('|');
                    switch (parse[0].ToLower())
                    {
                        case "npcdata":
                            if (parse[1].ToLower() != "v6")
                            {
                                read.Close();
                                return null;
                            }
                            break;
                        case "data":
                            {
                                npc.Name = parse[1];
                                npc.AttackSay = parse[2];
                                npc.Sprite = parse[3].ToInt();
                                npc.Behavior = (Enums.NpcBehavior)parse[4].ToInt();
                                npc.Range = parse[5].ToInt();
                                npc.Species = parse[6].ToInt();
                                npc.SpawnsAtDay = parse[7].ToBool();
                                npc.SpawnsAtNight = parse[8].ToBool();
                                npc.SpawnsAtDawn = parse[9].ToBool();
                                npc.SpawnsAtDusk = parse[10].ToBool();
                                npc.AIScript = parse[11];
                                npc.RecruitRate = parse[12].ToInt();
                            }
                            break;
                        case "moves":
                            {
                                int n = 1;
                                for (int i = 0; i < npc.Moves.Length; i++)
                                {
                                    npc.Moves[i] = parse[n].ToInt();

                                    n += 1;
                                }
                            }
                            break;
                        case "drop":
                            {
                                int dropNum = parse[1].ToInt();
                                if (dropNum < npc.Drops.Length)
                                {
                                    npc.Drops[dropNum].ItemNum = parse[2].ToInt();
                                    npc.Drops[dropNum].ItemValue = parse[3].ToInt();
                                    npc.Drops[dropNum].Tag = parse[4];
                                    npc.Drops[dropNum].Chance = parse[5].ToInt();
                                }
                            }
                            break;
                    }
                }
            }
            return npc;
        }

        public static void SaveNpc(Npc npc, int npcNum)
        {
            string FileName = IO.Paths.NpcsFolder + "npc" + npcNum.ToString() + ".dat";
            using (System.IO.StreamWriter Write = new System.IO.StreamWriter(FileName))
            {
                Write.WriteLine("NpcData|V6");
                Write.WriteLine("Data" + "|" + npc.Name + "|" + npc.AttackSay + "|" + npc.Sprite + "|" + (int)npc.Behavior + "|" + npc.Range + "|" + npc.Species + "|" + npc.SpawnsAtDay.ToIntString() + "|" + npc.SpawnsAtNight.ToIntString() + "|" + npc.SpawnsAtDawn.ToIntString() + "|" + npc.SpawnsAtDusk.ToIntString() + "|" + npc.AIScript + "|" + npc.RecruitRate + "|");
                Write.Write("Moves|");
                for (int i = 0; i < npc.Moves.Length; i++)
                {
                    Write.Write(npc.Moves[i].ToString() + "|");
                }
                Write.WriteLine();
                for (int z = 0; z < Constants.MAX_NPC_DROPS; z++)
                {
                    Write.WriteLine("Drop" + "|" + z + "|" + npc.Drops[z].ItemNum + "|" + npc.Drops[z].ItemValue + "|" + npc.Drops[z].Tag + "|" + npc.Drops[z].Chance + "|");
                }
            }
        }

        #endregion Methods
    }
}
