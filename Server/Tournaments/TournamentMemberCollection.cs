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

namespace Server.Tournaments
{
    public class TournamentMemberCollection : ICloneable
    {
        List<TournamentMember> members;

        internal TournamentMemberCollection(List<TournamentMember> members)
        {
            this.members = members;
        }

        public TournamentMemberCollection()
        {
            members = new List<TournamentMember>();
        }

        public void Add(TournamentMember member)
        {
            if (members.Contains(member) == false)
            {
                members.Add(member);
            }
        }

        public int Count
        {
            get { return members.Count; }
        }

        public int IndexOf(string playerID)
        {
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i].Client.Player.CharID == playerID)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool Contains(string playerID)
        {
            return (IndexOf(playerID) > -1);
        }

        public TournamentMember this[string playerID]
        {
            get
            {
                int memberIndex = IndexOf(playerID);
                if (memberIndex > -1)
                {
                    return members[memberIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        public TournamentMember this[Network.Client client]
        {
            get
            {
                return this[client.Player.CharID];
            }
        }

        public TournamentMember this[int index]
        {
            get { return members[index]; }
        }

        public void RemoveAt(int index)
        {
            if (index > -1)
            {
                members.RemoveAt(index);
            }
        }

        public void Remove(Network.Client client)
        {
            RemoveAt(IndexOf(client.Player.CharID));
        }

        public void Remove(TournamentMember member)
        {
            RemoveAt(IndexOf(member.Client.Player.CharID));
        }

        public object Clone()
        {
            TournamentMemberCollection memberCollection = new TournamentMemberCollection();
            for (int i = 0; i < members.Count; i++)
            {
                memberCollection.Add(members[i]);
            }
            return memberCollection;
        }
    }
}
