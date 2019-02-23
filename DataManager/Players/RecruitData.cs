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

namespace DataManager.Players
{
    public class RecruitData
    {
        public bool UsingTempStats { get; set; }
        public string Name { get; set; }
        public bool Nickname { get; set; }
        public int NpcBase { get; set; }
        public int Species { get; set; }
        public int Form { get; set; }
        public int Costume { get; set; }
        public int Shiny { get; set; }
        public byte Sex { get; set; }
        public int HeldItemSlot { get; set; }
        public int Level { get; set; }
        public ulong Exp { get; set; }
        public int HP { get; set; }
        public int StatusAilment { get; set; }
        public int StatusAilmentCounter { get; set; }
        public int IQ { get; set; }
        public int Belly { get; set; }
        public int MaxBelly { get; set; }
        public int AtkBonus { get; set; }
        public int DefBonus { get; set; }
        public int SpeedBonus { get; set; }
        public int SpclAtkBonus { get; set; }
        public int SpclDefBonus { get; set; }

        public Characters.Move[] Moves { get; set; }
        public List<Characters.VolatileStatus> VolatileStatus { get; set; }


        public RecruitData() {
            HeldItemSlot = -1;
            Costume = -1;
            Moves = new Characters.Move[4];
            for (int i = 0; i < Moves.Length; i++) {
                Moves[i] = new Characters.Move();
            }
            VolatileStatus = new List<Characters.VolatileStatus>();
        }

    }
}
