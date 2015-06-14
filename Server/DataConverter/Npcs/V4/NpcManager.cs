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

namespace Server.DataConverter.Npcs.V4
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class NpcManager
    {
        #region Methods

        public static Npc LoadNpc(int npcNum) {
            Npc npc = new Npc();
            string FileName = IO.Paths.NpcsFolder + "npc" + npcNum + ".dat";
            string s;
            string[] parse;
            using (System.IO.StreamReader read = new System.IO.StreamReader(FileName)) {
                while (!(read.EndOfStream)) {
                    s = read.ReadLine();
                    parse = s.Split('|');
                    switch (parse[0].ToLower()) {
                        case "npcdata":
                            if (parse[1].ToLower() != "v4") {
                                read.Close();
                                return null;
                            }
                            break;
                        case "data": {
                                npc.Name = parse[1];
                                npc.AttackSay = parse[2];
                                npc.Sprite = parse[3].ToInt();
                                npc.SpawnSecs = parse[4].ToInt();
                                npc.Behavior = (Enums.NpcBehavior)parse[5].ToInt();
                                npc.Range = parse[6].ToInt();
                                npc.Species = parse[7].ToInt();
                                npc.Big = parse[8].ToBool();
                                npc.SpawnTime = parse[9].ToInt();
                                if (parse.Length > 11) {
                                    npc.Spell = parse[10].ToInt();
                                }
                                if (parse.Length > 12) {
                                    npc.Frequency = parse[11].ToInt();
                                }
                                if (parse.Length > 13) {
                                    npc.AIScript = parse[12];
                                }
                            }
                            break;
                        case "recruit":
                            npc.RecruitRate = parse[1].ToInt();
                            npc.RecruitLevel = parse[2].ToInt();
                            break;
                        case "items": {
                                if (parse[1].ToInt() < npc.Drops.Length) {
                                    npc.Drops[parse[1].ToInt()].ItemNum = parse[2].ToInt();
                                    npc.Drops[parse[1].ToInt()].ItemValue = parse[3].ToInt();
                                    npc.Drops[parse[1].ToInt()].Chance = parse[4].ToInt();
                                }
                            }
                            break;
                    }
                }
            }
            return npc;
        }

        public static void SaveNpc(Npc npc, int npcNum) {
            string FileName = IO.Paths.NpcsFolder + "npc" + npcNum.ToString() + ".dat";
            using (System.IO.StreamWriter Write = new System.IO.StreamWriter(FileName)) {
                Write.WriteLine("NpcData|V4");
                Write.WriteLine("Data" + "|" + npc.Name + "|" + npc.AttackSay + "|" + npc.Sprite + "|" + npc.SpawnSecs + "|" + (int)npc.Behavior + "|" + npc.Range + "|" + npc.Species + "|" + npc.Big + "|" + npc.SpawnTime + "|" + npc.Spell + "|" + npc.Frequency + "|" + npc.AIScript + "|");
                Write.WriteLine("recruit" + "|" + npc.RecruitRate + "|" + npc.RecruitLevel + "|");
                for (int z = 0; z < Constants.MAX_NPC_DROPS; z++) {
                    Write.WriteLine("items" + "|" + z + "|" + npc.Drops[z].ItemNum + "|" + npc.Drops[z].ItemValue + "|" + npc.Drops[z].Chance + "|");
                }
            }
        }

        #endregion Methods
    }
}