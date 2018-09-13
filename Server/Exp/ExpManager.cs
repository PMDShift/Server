using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using PMDCP.DatabaseConnector.MySql;
using PMDCP.DatabaseConnector;
using Server.Database;
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


namespace Server.Exp
{
    public class ExpManager
    {
        #region Fields

        static ExpCollection exp;

        #endregion Fields

        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        #region Properties

        public static ExpCollection Exp
        {
            get { return exp; }
        }

        #endregion Properties

        #region Methods


        public static void Initialize()
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                //method for getting count
                string query = "SELECT COUNT(level_num) FROM experience";
                DataColumnCollection row = dbConnection.Database.RetrieveRow(query);

                int count = row["COUNT(level_num)"].ValueString.ToInt();
                exp = new ExpCollection(count);
            }
        }

        public static void LoadExps(object object1)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                try
                {
                    var database = dbConnection.Database;

                    string query = "SELECT * " +
                        "FROM experience";

                    foreach (DataColumnCollection columnCollection in database.RetrieveRowsEnumerable(query))
                    {
                        int level = columnCollection["level_num"].ValueString.ToInt();

                        exp[level - 1] = columnCollection["med_slow"].ValueString.ToUlng();

                        if (LoadUpdate != null)
                            LoadUpdate(null, new LoadingUpdateEventArgs(level, exp.MaxLevels));
                    }

                    if (LoadComplete != null)
                        LoadComplete(null, null);
                }
                catch (Exception ex)
                {
                    Exceptions.ErrorLogger.WriteToErrorLog(ex);
                }
            }
        }

        #endregion Methods
    }
}