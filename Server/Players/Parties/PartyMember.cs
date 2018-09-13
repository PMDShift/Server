using System;
using System.Collections.Generic;
using System.Text;

using Server.Network;
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


namespace Server.Players.Parties
{
    public class PartyMember
    {
        #region Fields

        string playerID;
        bool expShared;

        #endregion Fields

        #region Constructors

        public PartyMember(string playerID)
        {
            this.playerID = playerID;
            expShared = true;
        }

        #endregion Constructors

        #region Properties

        public string PlayerID
        {
            get { return playerID; }
        }

        public bool ExpShared
        {
            get { return expShared; }
            set { expShared = value; }
        }

        #endregion Properties
    }
}