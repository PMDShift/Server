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

namespace Server.DataConverter.Moves.V1
{
    public class Move
    {
        public string Name { get; set; }
        public int ClassReq { get; set; }
        public int ClassReq2 { get; set; }
        public int ClassReq3 { get; set; }
        public int LevelReq { get; set; }
        public int MPCost { get; set; }
        public int Sound { get; set; }
        public Enums.MoveType Type { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int Range { get; set; }
        public int SpellAnim { get; set; }
        public int SpellTime { get; set; }
        public int SpellDone { get; set; }
        public bool AE { get; set; }
        public bool Big { get; set; }
        public int Element { get; set; }

        public bool IsKey { get; set; }
        public int KeyItem { get; set; }
    }
}
