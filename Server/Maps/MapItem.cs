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

namespace Server.Maps
{
    public class MapItem
    {
        DataManager.Maps.MapItem rawItem;

        public DataManager.Maps.MapItem RawItem
        {
            get
            {
                return rawItem;
            }
        }

        public MapItem(DataManager.Maps.MapItem rawItem)
        {
            this.rawItem = rawItem;
        }

        public int Num
        {
            get { return rawItem.Num; }
            set { rawItem.Num = value; }
        }
        public int Value
        {
            get { return rawItem.Value; }
            set { rawItem.Value = value; }
        }
        public bool Sticky
        {
            get { return rawItem.Sticky; }
            set { rawItem.Sticky = value; }
        }
        public bool Hidden
        {
            get { return rawItem.Hidden; }
            set { rawItem.Hidden = value; }
        }
        public string Tag
        {
            get { return rawItem.Tag; }
            set { rawItem.Tag = value; }
        }
        public int X
        {
            get { return rawItem.X; }
            set { rawItem.X = value; }
        }
        public int Y
        {
            get { return rawItem.Y; }
            set { rawItem.Y = value; }
        }
        public string PlayerFor
        {
            get { return rawItem.PlayerFor; }
            set { rawItem.PlayerFor = value; }
        }
        public TickCount TimeDropped { get; set; }
    }
}
