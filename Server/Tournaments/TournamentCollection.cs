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

using PMDCP.Core;

namespace Server.Tournaments
{
    public class TournamentCollection
    {
        ListPair<string, Tournament> tournaments;

        public TournamentCollection()
        {
            tournaments = new ListPair<string, Tournament>();
        }

        public void AddTournament(Tournament tournament)
        {
            if (tournaments.Keys.Contains(tournament.ID) == false)
            {
                tournaments.Add(tournament.ID, tournament);
            }
        }

        public void Remove(string tournamentID)
        {
            tournaments.RemoveAtKey(tournamentID);
        }

        public void Remove(Tournament tournament)
        {
            tournaments.RemoveAtValue(tournament);
        }

        public bool IsIDInUse(string idToTest)
        {
            return (tournaments.Keys.Contains(idToTest));
        }

        public Tournament this[string tournamentID]
        {
            get
            {
                if (tournaments.Keys.Contains(tournamentID))
                {
                    return tournaments[tournamentID];
                }
                else
                {
                    return null;
                }
            }
        }

        public Tournament this[int index]
        {
            get { return tournaments.ValueByIndex(index); }
        }

        public int Count
        {
            get { return tournaments.Values.Count; }
        }
    }
}
