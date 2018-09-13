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
using System.Linq;
using System.Text;

namespace Server.DataConverter.Moves.V4
{
    public class MoveManager
    {
        public static Move LoadMove(int moveNum)
        {
            Move move = new Move();
            string[] parse = null;
            using (System.IO.StreamReader read = new System.IO.StreamReader(IO.Paths.MovesFolder + "move" + moveNum + ".dat"))
            {
                while (!(read.EndOfStream))
                {
                    parse = read.ReadLine().Split('|');
                    switch (parse[0].ToLower())
                    {
                        case "movedata":
                            if (parse[1].ToLower() != "v4")
                            {
                                read.Close();
                                return null;
                            }
                            break;
                        case "data":
                            move.Name = parse[1];
                            move.MaxPP = parse[2].ToInt();
                            move.EffectType = (Enums.MoveType)parse[3].ToInt();
                            move.Element = (Enums.PokemonType)parse[4].ToInt();
                            move.MoveCategory = (Enums.MoveCategory)parse[5].ToInt();
                            move.RangeType = (Enums.MoveRange)parse[6].ToInt();
                            move.Range = parse[7].ToInt();
                            move.TargetType = (Enums.MoveTarget)parse[8].ToInt();

                            move.Data1 = parse[9].ToInt();
                            move.Data2 = parse[10].ToInt();
                            move.Data3 = parse[11].ToInt();
                            move.Accuracy = parse[12].ToInt();
                            move.HitTime = parse[13].ToInt();
                            move.AdditionalEffectData1 = parse[14].ToInt();
                            move.AdditionalEffectData2 = parse[15].ToInt();
                            move.AdditionalEffectData3 = parse[16].ToInt();
                            move.PerPlayer = parse[17].ToBool();

                            move.KeyItem = parse[18].ToInt();

                            move.Sound = parse[19].ToInt();

                            move.AttackerAnim.AnimationType = (Enums.MoveAnimationType)parse[20].ToInt();
                            move.AttackerAnim.AnimationIndex = parse[21].ToInt();
                            move.AttackerAnim.FrameSpeed = parse[22].ToInt();
                            move.AttackerAnim.Repetitions = parse[23].ToInt();

                            move.TravelingAnim.AnimationType = (Enums.MoveAnimationType)parse[24].ToInt();
                            move.TravelingAnim.AnimationIndex = parse[25].ToInt();
                            move.TravelingAnim.FrameSpeed = parse[26].ToInt();
                            move.TravelingAnim.Repetitions = parse[27].ToInt();

                            move.DefenderAnim.AnimationType = (Enums.MoveAnimationType)parse[28].ToInt();
                            move.DefenderAnim.AnimationIndex = parse[29].ToInt();
                            move.DefenderAnim.FrameSpeed = parse[30].ToInt();
                            move.DefenderAnim.Repetitions = parse[31].ToInt();

                            break;
                    }
                }
            }

            return move;
        }


        public static void SaveMove(Move move, int moveNum)
        {
            string FileName = IO.Paths.MovesFolder + "move" + moveNum + ".dat";
            using (System.IO.StreamWriter write = new System.IO.StreamWriter(FileName))
            {
                write.WriteLine("MoveData|V4");
                write.WriteLine("Data|" + move.Name + "|" + move.MaxPP + "|" + ((int)move.EffectType).ToString() + "|" + ((int)move.Element).ToString() + "|" + ((int)move.MoveCategory).ToString() + "|"
                    + ((int)move.RangeType).ToString() + "|" + move.Range + "|" + ((int)move.TargetType).ToString()
                    + "|" + move.Data1 + "|" + move.Data2 + "|" + move.Data3 + "|" + move.Accuracy + "|" + move.HitTime + "|"
                    + move.AdditionalEffectData1 + "|" + move.AdditionalEffectData2 + "|" + move.AdditionalEffectData3 + "|"
                    + move.PerPlayer.ToIntString() + "|" + move.KeyItem + "|" + move.Sound + "|"
                    + ((int)move.AttackerAnim.AnimationType).ToString() + "|" + move.AttackerAnim.AnimationIndex.ToString() + "|" + move.AttackerAnim.FrameSpeed.ToString() + "|" + move.AttackerAnim.Repetitions.ToString() + "|"
                    + ((int)move.TravelingAnim.AnimationType).ToString() + "|" + move.TravelingAnim.AnimationIndex.ToString() + "|" + move.TravelingAnim.FrameSpeed.ToString() + "|" + move.TravelingAnim.Repetitions.ToString() + "|"
                    + ((int)move.DefenderAnim.AnimationType).ToString() + "|" + move.DefenderAnim.AnimationIndex.ToString() + "|" + move.DefenderAnim.FrameSpeed.ToString() + "|" + move.DefenderAnim.Repetitions.ToString() + "|");
            }
        }
    }
}
