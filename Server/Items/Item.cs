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

namespace Server.Items
{
    public class Item
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Pic { get; set; }
        public Enums.ItemType Type { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int Price { get; set; }
        public int StackCap { get; set; }
        public bool Bound { get; set; }
        public bool Loseable { get; set; }
        public int Rarity { get; set; }
        public bool StaffOnly { get; set; }

        public int ReqData1 { get; set; }
        public int ReqData2 { get; set; }
        public int ReqData3 { get; set; }
        public int ReqData4 { get; set; }
        public int ReqData5 { get; set; }
        public int ScriptedReq { get; set; }

        public int AddHP { get; set; }
        public int AddPP { get; set; }
        public int AddAttack { get; set; }
        public int AddDefense { get; set; }
        public int AddSpAtk { get; set; }
        public int AddSpDef { get; set; }
        public int AddSpeed { get; set; }
        public int AddEXP { get; set; }
        public int AttackSpeed { get; set; }
        public int RecruitBonus { get; set; }
    }
}
