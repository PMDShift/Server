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
    public class TargetCollection
    {
        public List<ICharacter> Foes { get; set; }

        public List<ICharacter> Friends { get; set; }

        public List<ICharacter> Self { get; set; }

        public TargetCollection()
        {
            Foes = new List<ICharacter>();
            Friends = new List<ICharacter>();
            Self = new List<ICharacter>();
        }

        public void Add(ICharacter character, Enums.CharacterMatchup matchup)
        {
            switch (matchup)
            {
                case Enums.CharacterMatchup.Foe:
                    {
                        Foes.Add(character);
                    }
                    break;
                case Enums.CharacterMatchup.Friend:
                    {
                        Friends.Add(character);
                    }
                    break;
                case Enums.CharacterMatchup.Self:
                    {
                        Self.Add(character);
                    }
                    break;
            }
        }

        public List<ICharacter> GetTargets(Enums.CharacterMatchup matchup)
        {
            switch (matchup)
            {
                case Enums.CharacterMatchup.Foe:
                    {
                        return Foes;
                    }
                    break;
                case Enums.CharacterMatchup.Friend:
                    {
                        return Friends;
                    }
                    break;
                case Enums.CharacterMatchup.Self:
                    {
                        return Self;
                    }
                    break;
            }

            return null;
        }

        public int Count { get { return Foes.Count + Friends.Count + Self.Count; } }

        public ICharacter this[int index]
        {
            get
            {
                if (index >= Foes.Count + Friends.Count)
                {
                    return Self[index - Friends.Count - Foes.Count];
                }
                else if (index >= Foes.Count)
                {
                    return Friends[index - Foes.Count];
                }
                else
                {
                    return Foes[index];
                }
            }
        }
    }
}
