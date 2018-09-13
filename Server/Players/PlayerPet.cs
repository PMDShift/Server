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
using System.Drawing;

namespace Server.Players
{
    class PlayerPet
    {
        public int Sprite
        {
            get;
            set;
        }

        public Enums.Direction Direction
        {
            get;
            set;
        }

        public bool Attacking
        {
            get;
            set;
        }

        public System.Drawing.Point Offset
        {
            get;
            set;
        }

        public System.Drawing.Point Location
        {
            get;
            set;
        }

        public int AttackTimer
        {
            get;
            set;
        }

        public int X
        {
            get { return Location.X; }
            set { Location = new Point(value, Location.Y); }
        }

        public int Y
        {
            get { return Location.Y; }
            set { Location = new Point(Location.X, value); }
        }

        public bool Confused
        {
            get;
            set;
        }

        public Enums.StatusAilment StatusAilment
        {
            get;
            set;
        }
    }
}
