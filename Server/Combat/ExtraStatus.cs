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

namespace Server.Combat
{
    public class ExtraStatus
    {
        DataManager.Characters.VolatileStatus rawVolatileStatus;

        public DataManager.Characters.VolatileStatus RawVolatileStatus
        {
            get
            {
                return rawVolatileStatus;
            }
        }

        public string Name
        {
            get { return rawVolatileStatus.Name; }
            set { rawVolatileStatus.Name = value; }
        }

        public int Emoticon
        {
            get { return rawVolatileStatus.Emoticon; }
            set { rawVolatileStatus.Emoticon = value; }
        }

        public int Counter
        {
            get { return rawVolatileStatus.Counter; }
            set { rawVolatileStatus.Counter = value; }
        }

        public string Tag
        {
            get { return rawVolatileStatus.Tag; }
            set { rawVolatileStatus.Tag = value; }
        }

        public ICharacter Target { get; set; }

        public ExtraStatus()
        {
            rawVolatileStatus = new DataManager.Characters.VolatileStatus();
        }

        public ExtraStatus(DataManager.Characters.VolatileStatus rawVolatileStatus)
        {
            this.rawVolatileStatus = rawVolatileStatus;
        }
    }
}
