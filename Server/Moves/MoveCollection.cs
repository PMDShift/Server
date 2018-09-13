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
using PMDCP.Core;

namespace Server.Moves
{
    public class MoveCollection
    {
        #region Fields

        ListPair<int, Move> moves;
        int maxMoves;

        #endregion Fields

        #region Constructors

        public MoveCollection(int maxMoves)
        {
            if (maxMoves == 0)
                maxMoves = 50;
            this.maxMoves = maxMoves;
            moves = new ListPair<int, Move>();
        }

        #endregion Constructors

        #region Properties

        public ListPair<int, Move> Moves
        {
            get { return moves; }
        }

        public int MaxMoves
        {
            get { return maxMoves; }
        }

        #endregion Properties

        #region Indexers

        public Move this[int index]
        {
            get { return moves[index]; }
            set { moves[index] = value; }
        }

        #endregion Indexers
    }
}
