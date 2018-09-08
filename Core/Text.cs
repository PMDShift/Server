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


namespace Server
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    public class Text
    {
        #region Fields
        #region OldColors
        static Color black = System.Drawing.Color.Black; // 01
        static Color blue = System.Drawing.Color.Blue; // 02
        static Color brightBlue = System.Drawing.Color.LightBlue; // 12
        static Color brightCyan = System.Drawing.Color.LightCyan; // 11
        static Color brightGreen = System.Drawing.Color.LightGreen; // 09
        static Color brightRed = System.Drawing.Color.Red; // 04
        static Color brown = System.Drawing.Color.Brown; // 05
        static Color orange = System.Drawing.Color.Orange; // 07
        static Color cyan = System.Drawing.Color.Cyan; // 10
        static Color darkGrey = System.Drawing.Color.DarkGray; // 15
        static Color green = System.Drawing.Color.Green; // 03
        static Color grey = System.Drawing.Color.Gray; // 14
        static Color magenta = System.Drawing.Color.Magenta; // 13
        static Color pink = System.Drawing.Color.Pink; // 06
        static Color red = System.Drawing.Color.Red; // 04
        static Color white = System.Drawing.Color.White; // default
        static Color whiteSmoke = System.Drawing.Color.WhiteSmoke; // 00
        static Color yellow = System.Drawing.Color.Yellow; // 08
        #endregion OldColors
            
        #region NewColors
        public static Color BoxReward { get; } = Color.FromArgb(0xffcece);		// same as Item
		public static Color Unselectable { get; } = Color.FromArgb(0xff0000);	// also used in rank S missions
		public static Color UnknownC { get; } = Color.FromArgb(0xffff00);		// same as Teammate
		public static Color Default { get; } = Color.FromArgb(0xffffff);		// also used in rank E missions
		public static Color Emphasis { get; } = Color.FromArgb(0x00ffff);		// same as NPC
		public static Color Teammate { get; } = Color.FromArgb(0xffff00);		// also used in rank *1-*9 missions
		public static Color Money { get; } = Color.FromArgb(0x00ffff);			// same as NPC
		public static Color Trap { get; } = Color.FromArgb(0xff7b7b);
		public static Color Item { get; } = Color.FromArgb(0xffcece);           // also used in rank D missions
		public static Color MoveEffect { get; } = Color.FromArgb(0xffffa5);
		public static Color SpeciesName { get; } = Color.FromArgb(0x00ff00);
		public static Color UnknownL { get; } = Color.FromArgb(0x00e763);
		public static Color Move { get; } = Color.FromArgb(0x00ff00);           // also used in rank C missions
		public static Color NPC { get; } = Color.FromArgb(0x00ffff);            // also used in rank A missions
		public static Color UnknownO { get; } = Color.FromArgb(0x009c00);
		public static Color Location { get; } = Color.FromArgb(0xffc663);       // also used in rank B missions
		public static Color UnknownQ { get; } = Color.FromArgb(0x0084ff);
		public static Color ShopPrice { get; } = Color.FromArgb(0x42ff42);
		public static Color MultiSelect { get; } = Color.FromArgb(0x6384e7);    // should be used as bg/highlight color
		public static Color Transparent { get; } = Color.FromArgb(0x000000);    // could be used for drop shadows?
		public static Color TreasureBox { get; } = Color.FromArgb(0xffff00);    // same as Teammate
		public static Color Number { get; } = Color.FromArgb(0x00ffff);         // same as NPC
		public static Color UnknownW { get; } = Color.FromArgb(0xff0000);       // same as Unselectable
		public static Color Team { get; } = Color.FromArgb(0xffa5ff);
		public static Color Player { get; } = Color.FromArgb(0x009cff);
		public static Color ExclusiveItem { get; } = Color.FromArgb(0x8484ff);
        #endregion NewColors
        #endregion Fields

        #region Properties

        public static Color Black
        {
            get { return black; }
        }

        public static Color Blue
        {
            get { return blue; }
        }

        public static Color BrightBlue
        {
            get { return brightBlue; }
        }

        public static Color BrightCyan
        {
            get { return brightCyan; }
        }

        public static Color BrightGreen
        {
            get { return brightGreen; }
        }

        public static Color BrightRed
        {
            get { return brightRed; }
        }

        public static Color Brown
        {
            get { return brown; }
        }

        public static Color Orange
        {
            get { return orange; }
        }

        public static Color Cyan
        {
            get { return cyan; }
        }

        public static Color DarkGrey
        {
            get { return darkGrey; }
        }

        public static Color Green
        {
            get { return green; }
        }

        public static Color Grey
        {
            get { return grey; }
        }

        public static Color Magenta
        {
            get { return magenta; }
        }

        public static Color Pink
        {
            get { return pink; }
        }

        public static Color Red
        {
            get { return red; }
        }

        public static Color White
        {
            get { return white; }
        }
        
        public static Color WhiteSmoke
        {
            get { return whiteSmoke; }
        }

        public static Color Yellow
        {
            get { return yellow; }
        }

        #endregion Properties
    }
}
