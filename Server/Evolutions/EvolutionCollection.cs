using System;
using System.Collections.Generic;
using System.Text;
using PMDCP.Core;
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


namespace Server.Evolutions
{
    public class EvolutionCollection
    {
        #region Fields

        ListPair<int, Evolution> evolutions;
        int maxEvos;

        #endregion Fields

        #region Constructors

        public EvolutionCollection(int maxEvos)
        {
            if (maxEvos == 0)
                maxEvos = 50;
            this.maxEvos = maxEvos;
            evolutions = new ListPair<int, Evolution>();
        }

        #endregion Constructors

        #region Properties

        public ListPair<int, Evolution> Evolutions
        {
            get { return evolutions; }
        }

        public int MaxEvos
        {
            get { return maxEvos; }
        }

        #endregion Properties

        #region Indexers

        public Evolution this[int index]
        {
            get { return evolutions[index]; }
            set { evolutions[index] = value; }
        }

        #endregion Indexers
    }
}