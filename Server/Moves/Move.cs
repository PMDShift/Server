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

namespace Server.Moves
{
    public class Move
    {
        #region Properties

        public bool IsSandboxed { get; set; }
        public bool IsBeingReviewed { get; set; }

        public string Name { get; set; }

        public int MaxPP { get; set; }


        public Enums.MoveType EffectType { get; set; }


        public Enums.PokemonType Element { get; set; }

        public Enums.MoveCategory MoveCategory { get; set; }


        public Enums.MoveRange RangeType { get; set; }

        public int Range { get; set; }

        public Enums.MoveTarget TargetType { get; set; }



        public int Data1 { get; set; }

        public int Data2 { get; set; }

        public int Data3 { get; set; }

        public int Accuracy { get; set; }

        public int HitTime { get; set; }

        public bool HitFreeze { get; set; }

        public int AdditionalEffectData1 { get; set; }

        public int AdditionalEffectData2 { get; set; }

        public int AdditionalEffectData3 { get; set; }

        public bool PerPlayer { get; set; }

        public int KeyItem { get; set; }


        public int Sound { get; set; }

        public MoveAnimation AttackerAnim { get; set; }

        public MoveAnimation TravelingAnim { get; set; }

        public MoveAnimation DefenderAnim { get; set; }



        #endregion Properties

        public Move()
        {
            AttackerAnim = new MoveAnimation();
            TravelingAnim = new MoveAnimation();
            DefenderAnim = new MoveAnimation();
        }
    }
}
