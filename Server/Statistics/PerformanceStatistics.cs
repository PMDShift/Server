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
using System.Diagnostics;
using System.Linq;
using System.Text;
using PMDCP.DatabaseConnector;
using Server.Database;
using Server.Network;

namespace Server.Statistics
{
    class PerformanceStatistics
    {
        public static void GatherStatistics()
        {
        }

        public static void DumpStatistics()
        {
            int playerCount = 0;
            int staffCount = 0;
            foreach (Client i in ClientManager.GetClients())
            {
                if (i.Player != null)
                {
                    if (i.IsPlaying())
                    {
                        playerCount++;
                        if (Players.Ranks.IsAllowed(i, Enums.Rank.Monitor))
                        {
                            staffCount++;
                        }
                    }
                }
            }
            int partyCount = Server.Players.Parties.PartyManager.CountActiveParties();
            int activeMapCount = Server.Maps.MapManager.CountActiveMaps();
            int activeNetworkClients = Server.Network.ClientManager.CountActiveClients();

            Process serverProcess = Process.GetCurrentProcess();
            long workingSet = serverProcess.WorkingSet64;
            long elapsedHours = (long)Globals.LiveTime.Elapsed.TotalHours;

            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                dbConnection.Database.AddRow("stats_performance", new IDataColumn[] {
                    dbConnection.Database.CreateColumn(false, "Time", DateTime.UtcNow.ToString()),
                    dbConnection.Database.CreateColumn(false, "PlayerCount", playerCount.ToString()),
                    dbConnection.Database.CreateColumn(false, "StaffCount", staffCount.ToString()),
                    dbConnection.Database.CreateColumn(false, "ActiveNetworkClients", activeNetworkClients.ToString()),
                    dbConnection.Database.CreateColumn(false, "ActiveMaps", activeMapCount.ToString()),
                    dbConnection.Database.CreateColumn(false, "ActiveParties", partyCount.ToString()),
                    dbConnection.Database.CreateColumn(false, "WorkingSet", workingSet.ToString()),
                    dbConnection.Database.CreateColumn(false, "ElapsedHours", elapsedHours.ToString())
                });
            }
        }
    }
}
