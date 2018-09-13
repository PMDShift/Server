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

using Server.Network;

namespace Server.Tournaments
{
    public class TournamentMember
    {
        string tournament;
        Client client;
        bool active;
        bool admin;

        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }

        public Client Client
        {
            get { return client; }
        }

        public bool Active
        {
            get { return active; }
            set
            {
                active = value;
            }
        }

        public TournamentMember()
        {
            active = true;
            admin = false;
        }

        public Tournament GetTournamentInstance()
        {
            return TournamentManager.Tournaments[tournament];
        }

        public TournamentMember(Tournament tournament, Client client)
        {
            this.tournament = tournament.ID;
            this.client = client;
        }
    }
}
