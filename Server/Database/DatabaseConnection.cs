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

using PMDCP.DatabaseConnector.MySql;
using Server.Network;

namespace Server.Database
{
    public class DatabaseConnection : IDisposable
    {
        PMDCP.DatabaseConnector.MySql.MySql database;
        Client client;
        DatabaseID databaseID;

        public PMDCP.DatabaseConnector.MySql.MySql Database {
            get {
                if (!disposed) {
                    return database;
                } else {
                    throw new ObjectDisposedException("database", "Database connection has been disposed.");
                }
            }
        }

        public DatabaseConnection(DatabaseID databaseID) {
            this.databaseID = databaseID;

            string databaseName = DetermineDatabaseName(databaseID);
            if (!string.IsNullOrEmpty(databaseName)) {
#if !DEBUG
                database = new PMDCP.DatabaseConnector.MySql.MySql(Settings.DatabaseIP, Settings.DatabasePort, databaseName, Settings.DatabaseUser, Settings.DatabasePassword);
                 
#else
                database = new PMDCP.DatabaseConnector.MySql.MySql("localhost", Settings.DatabasePort, databaseName, Settings.DatabaseUser, Settings.DatabasePassword);
#endif
            }

            database.OpenConnection();
        }

        private string DetermineDatabaseName(DatabaseID databaseID) {
            switch (databaseID) {
                case DatabaseID.Players: {
                        return "mdx_players";
                    }
                case DatabaseID.Data: {
                        return "mdx_data";
                    }
                default: {
                        return null;
                    }
            }
        }

        bool disposed;
        public void Dispose() {
            if (!disposed) {

                if (database != null) {
                    database.CloseConnection();
                }

                disposed = true;
            }
        }
    }
}
