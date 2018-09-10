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
using System.Text;

namespace Server.RDungeons
{
    public class RDungeon
    {
        public string DungeonName { get; set; }
        public Enums.Direction Direction { get; set; }
        public bool Recruitment { get; set; }
        public bool Exp { get; set; }
        public int WindTimer { get; set;}
        public int DungeonIndex { get; set; }
        public bool IsSandboxed { get; set; }
        public int ZoneID { get; set; }

        public List<RDungeonFloor> Floors { get; set; }

        

        public int RDungeonIndex;


        public RDungeon(int rDungeonIndex) {
            RDungeonIndex = rDungeonIndex;
            DungeonName = "";
            Floors = new List<RDungeonFloor>();
            
        }
    }
}
