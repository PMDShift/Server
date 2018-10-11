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


namespace Server.Maps
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data = DataManager.Maps;

    public class Tile
    {
        Data.Tile tile;

        public Data.Tile RawTile
        {
            get { return tile; }
        }

        #region Constructors

        public Tile(Data.Tile tile)
        {
            this.tile = tile;
        }

        #endregion Constructors

        #region Properties

        public int Anim
        {
            get { return tile.Anim; }
            set { tile.Anim = value; }
        }

        public int AnimSet
        {
            get { return tile.AnimSet; }
            set { tile.AnimSet = value; }
        }

        public int Data1
        {
            get { return tile.Data1; }
            set { tile.Data1 = value; }
        }

        public int Data2
        {
            get { return tile.Data2; }
            set { tile.Data2 = value; }
        }

        public int Data3
        {
            get { return tile.Data3; }
            set { tile.Data3 = value; }
        }

        public bool DoorOpen
        {
            get; set;
        }

        public TickCount DoorTimer
        {
            get; set;
        }

        public int F2Anim
        {
            get { return tile.F2Anim; }
            set { tile.F2Anim = value; }
        }

        public int F2AnimSet
        {
            get { return tile.F2AnimSet; }
            set { tile.F2AnimSet = value; }
        }

        public int FAnim
        {
            get { return tile.FAnim; }
            set { tile.FAnim = value; }
        }

        public int FAnimSet
        {
            get { return tile.FAnimSet; }
            set
            {
                tile.FAnimSet = value;
            }
        }

        public int Fringe
        {
            get { return tile.Fringe; }
            set { tile.Fringe = value; }
        }

        public int Fringe2
        {
            get { return tile.Fringe2; }
            set { tile.Fringe2 = value; }
        }

        public int Fringe2Set
        {
            get { return tile.Fringe2Set; }
            set { tile.Fringe2Set = value; }
        }

        public int FringeSet
        {
            get { return tile.FringeSet; }
            set { tile.FringeSet = value; }
        }

        public int Ground
        {
            get { return tile.Ground; }
            set { tile.Ground = value; }
        }

        public int GroundSet
        {
            get { return tile.GroundSet; }
            set { tile.GroundSet = value; }
        }

        public int GroundAnim
        {
            get { return tile.GroundAnim; }
            set { tile.GroundAnim = value; }
        }

        public int GroundAnimSet
        {
            get { return tile.GroundAnimSet; }
            set { tile.GroundAnimSet = value; }
        }
        public int RDungeonMapValue
        {
            get { return tile.RDungeonMapValue; }
            set { tile.RDungeonMapValue = value; }
        }

        public int M2Anim
        {
            get { return tile.M2Anim; }
            set { tile.M2Anim = value; }
        }

        public int M2AnimSet
        {
            get { return tile.M2AnimSet; }
            set { tile.M2AnimSet = value; }
        }

        public int Mask
        {
            get { return tile.Mask; }
            set { tile.Mask = value; }
        }

        public int Mask2
        {
            get { return tile.Mask2; }
            set { tile.Mask2 = value; }
        }

        public int Mask2Set
        {
            get { return tile.Mask2Set; }
            set { tile.Mask2Set = value; }
        }

        public int MaskSet
        {
            get { return tile.MaskSet; }
            set { tile.MaskSet = value; }
        }

        public string String1
        {
            get { return tile.String1; }
            set { tile.String1 = value; }
        }

        public string String2
        {
            get { return tile.String2; }
            set { tile.String2 = value; }
        }

        public string String3
        {
            get { return tile.String3; }
            set { tile.String3 = value; }
        }

        public Enums.TileType Type
        {
            get { return (Enums.TileType)tile.Type; }
            set { tile.Type = (int)value; }
        }

        public int F3Anim
        {
            get { return tile.F3Anim; }
            set { tile.F3Anim = value; }
        }

        public int F3AnimSet
        {
            get { return tile.F3AnimSet; }
            set { tile.F3AnimSet = value; }
        }

        public int Fringe3
        {
            get { return tile.Fringe3; }
            set { tile.Fringe3 = value; }
        }

        public int Fringe3Set
        {
            get { return tile.Fringe3Set; }
            set { tile.Fringe3Set = value; }
        }

        public int F4Anim
        {
            get { return tile.F4Anim; }
            set { tile.F4Anim = value; }
        }

        public int F4AnimSet
        {
            get { return tile.F4AnimSet; }
            set { tile.F4AnimSet = value; }
        }

        public int Fringe4
        {
            get { return tile.Fringe4; }
            set { tile.Fringe4 = value; }
        }

        public int Fringe4Set
        {
            get { return tile.Fringe4Set; }
            set { tile.Fringe4Set = value; }
        }

        public int F5Anim
        {
            get { return tile.F5Anim; }
            set { tile.F5Anim = value; }
        }

        public int F5AnimSet
        {
            get { return tile.F5AnimSet; }
            set { tile.F5AnimSet = value; }
        }

        public int Fringe5
        {
            get { return tile.Fringe5; }
            set { tile.Fringe5 = value; }
        }

        public int Fringe5Set
        {
            get { return tile.Fringe5Set; }
            set { tile.Fringe5Set = value; }
        }

        public int M3Anim
        {
            get { return tile.M3Anim; }
            set { tile.M3Anim = value; }
        }

        public int M3AnimSet
        {
            get { return tile.F5AnimSet; }
            set { tile.F5AnimSet = value; }
        }

        public int Mask3
        {
            get { return tile.Mask3; }
            set { tile.Mask3 = value; }
        }

        public int Mask3Set
        {
            get { return tile.Mask3Set; }
            set { tile.Mask3Set = value; }
        }

        public int M4Anim
        {
            get { return tile.M4Anim; }
            set { tile.M4Anim = value; }
        }

        public int M4AnimSet
        {
            get { return tile.F5AnimSet; }
            set { tile.F5AnimSet = value; }
        }

        public int Mask4
        {
            get { return tile.Mask4; }
            set { tile.Mask4 = value; }
        }

        public int Mask4Set
        {
            get { return tile.Mask4Set; }
            set { tile.Mask4Set = value; }
        }

        public int M5Anim
        {
            get { return tile.M5Anim; }
            set { tile.M5Anim = value; }
        }

        public int M5AnimSet
        {
            get { return tile.F5AnimSet; }
            set { tile.F5AnimSet = value; }
        }

        public int Mask5
        {
            get { return tile.Mask5; }
            set { tile.Mask5 = value; }
        }

        public int Mask5Set
        {
            get { return tile.Mask5Set; }
            set { tile.Mask5Set = value; }
        }

        #endregion Properties
    }
}
