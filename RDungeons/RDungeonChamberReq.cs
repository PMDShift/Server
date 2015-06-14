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
using System.Linq;
using System.Text;

namespace Server.RDungeons {
    public class RDungeonChamberReq {
        //passed to the random dungeon generator to know what the chamber requires

        public int MinX { get; set; }
        public int MinY { get; set; }

        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public int TopPassage { get; set; }
        public int BottomPassage { get; set; }
        public int LeftPassage { get; set; }
        public int RightPassage { get; set; }

        public Enums.Acceptance TopAcceptance { get; set; }
        public Enums.Acceptance BottomAcceptance { get; set; }
        public Enums.Acceptance LeftAcceptance { get; set; }
        public Enums.Acceptance RightAcceptance { get; set; }

        public Enums.Acceptance Start { get; set; }
        public Enums.Acceptance End { get; set; }

        public RDungeonChamberReq() {
            MinX = 2;
            MaxX = 2;
            MinY = 2;
            MaxY = 2;
            TopPassage = -1;
            TopAcceptance = Enums.Acceptance.Maybe;
            BottomPassage = -1;
            BottomAcceptance = Enums.Acceptance.Maybe;
            LeftPassage = -1;
            LeftAcceptance = Enums.Acceptance.Maybe;
            RightPassage = -1;
            RightAcceptance = Enums.Acceptance.Maybe;
        }
    }
}
