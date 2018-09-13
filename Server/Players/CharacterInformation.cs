using System;
using System.Collections.Generic;
using System.Linq;
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


namespace Server.Players
{
    public class CharacterInformation
    {
        #region Fields

        string account;
        string id;
        string name;
        int slot;

        #endregion Fields

        #region Constructors

        public CharacterInformation(string name, string account, int slot, string id)
        {
            this.name = name;
            this.account = account;
            this.slot = slot;
            this.id = id;
        }

        #endregion Constructors

        #region Properties

        public string Account
        {
            get { return account; }
        }

        public string ID
        {
            get { return id; }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }

        public int Slot
        {
            get { return slot; }
        }

        #endregion Properties
    }
}