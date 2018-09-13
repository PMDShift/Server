﻿using System;
using System.Collections.Generic;
using System.Text;
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


namespace Server.Exp
{
    public class ExpCollection
    {
        #region Fields

        ulong[] exp;
        int maxLevels;

        #endregion Fields

        #region Constructors

        public ExpCollection(int maxLevels)
        {
            if (maxLevels == 0)
                maxLevels = 100;
            exp = new ulong[maxLevels];
            this.maxLevels = maxLevels;
        }

        #endregion Constructors

        #region Properties

        public int MaxLevels
        {
            get { return maxLevels; }
        }

        #endregion Properties

        #region Indexers

        public ulong this[int index]
        {
            get { return exp[index]; }
            set { exp[index] = value; }
        }

        #endregion Indexers
    }
}