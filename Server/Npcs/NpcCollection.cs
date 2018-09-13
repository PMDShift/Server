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

namespace Server.Npcs
{
    public class NpcCollection
    {
        #region Fields

        ListPair<int, Npc> npcs;
        int maxNpcs;

        #endregion Fields

        #region Constructors

        public NpcCollection(int maxNpcs)
        {
            if (maxNpcs == 0)
                maxNpcs = 50;
            this.maxNpcs = maxNpcs;
            npcs = new ListPair<int, Npc>();
        }

        #endregion Constructors

        #region Properties

        public ListPair<int, Npc> Npcs
        {
            get { return npcs; }
        }

        public int MaxNpcs
        {
            get { return maxNpcs; }
        }

        #endregion Properties

        #region Indexers

        public Npc this[int index]
        {
            get { return npcs[index]; }
            set { npcs[index] = value; }
        }

        #endregion Indexers
    }
}
