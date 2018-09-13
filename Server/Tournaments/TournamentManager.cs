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
using Server.Maps;

namespace Server.Tournaments
{
    public class TournamentManager
    {
        static TournamentCollection tournaments;

        public static TournamentCollection Tournaments
        {
            get { return tournaments; }
        }

        public static void Initialize()
        {
            tournaments = new TournamentCollection();
        }

        public static Tournament CreateTournament(Client manager, string name, string hubMap, int hubStartX, int hubStartY)
        {
            Tournament tournament = new Tournament(GenerateUniqueID(), name, new WarpDestination(hubMap, hubStartX, hubStartY));
            // Add the manager
            TournamentMember member = new TournamentMember(tournament, manager);
            member.Admin = true;
            member.Active = true;
            tournament.RegisterPlayer(member);

            tournaments.AddTournament(tournament);

            return tournament;
        }

        public static void RemoveTournament(Tournament tournament)
        {
            tournaments.Remove(tournament);
        }

        private static string GenerateUniqueID()
        {
            string testID;
            while (true)
            {
                // Generate a new ID
                testID = Security.PasswordGen.Generate(16);
                // Check if the same ID is already in use
                if (!tournaments.IsIDInUse(testID))
                {
                    // If it isn't, our generated ID is useable!
                    return testID;
                }
                // If the same ID is in use, try to generate a new ID
            }
        }
    }
}
