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
using System.Text;
using System.Xml;
using PMDCP.DatabaseConnector.MySql;
using PMDCP.DatabaseConnector;
using Server.Database;

namespace Server.Evolutions
{
    public class EvolutionManagerBase
    {
        static EvolutionCollection evolution;

        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        public static void Initialize() {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                //method for getting count
                string query = "SELECT COUNT(num) FROM evolution";
                DataColumnCollection row = dbConnection.Database.RetrieveRow(query);

                int count = row["COUNT(num)"].ValueString.ToInt();
                evolution = new EvolutionCollection(count);
            }
        }

        public static EvolutionCollection Evolutions {
            get { return evolution; }
        }

        #region Loading

        public static void LoadEvos(object object1) {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                for (int i = 0; i <= evolution.MaxEvos; i++)
                {
                    try
                    {
                        LoadEvo(i, dbConnection.Database);
                    }
                    catch (Exception ex)
                    {
                        Exceptions.ErrorLogger.WriteToErrorLog(ex, "Loading Evolution #" + i.ToString());
                    }
                    if (LoadUpdate != null)
                        LoadUpdate(null, new LoadingUpdateEventArgs(i, evolution.MaxEvos));
                }
                if (LoadComplete != null)
                    LoadComplete(null, null);
            }
        }

        public static void LoadEvo(int evoNum, PMDCP.DatabaseConnector.MySql.MySql database)
        {
            if (evolution.Evolutions.ContainsKey(evoNum) == false)
                evolution.Evolutions.Add(evoNum, new Evolution());

            string query = "SELECT name, " +
                "species " +
                "FROM evolution WHERE evolution.num = \'" + evoNum + "\'";

            DataColumnCollection row = database.RetrieveRow(query);
            if (row != null)
            {
                evolution[evoNum].Name = row["name"].ValueString;
                evolution[evoNum].Species = row["species"].ValueString.ToInt();
            }

            query = "SELECT branch, " +
                "name, " +
                "species, " +
                "req_script, " +
                "data1, " +
                "data2, " +
                "data3 " +
                "FROM evolution_branch WHERE evolution_branch.num = \'" + evoNum + "\'";

            foreach (DataColumnCollection columnCollection in database.RetrieveRowsEnumerable(query))
            {
                int tradeNum = columnCollection["branch"].ValueString.ToInt();

                EvolutionBranch newBranch = new EvolutionBranch();
                newBranch.Name = columnCollection["name"].ValueString;
                newBranch.NewSpecies = columnCollection["species"].ValueString.ToInt();
                newBranch.ReqScript = columnCollection["req_script"].ValueString.ToInt();
                newBranch.Data1 = columnCollection["data1"].ValueString.ToInt();
                newBranch.Data2 = columnCollection["data2"].ValueString.ToInt();
                newBranch.Data3 = columnCollection["data3"].ValueString.ToInt();
                evolution[evoNum].Branches.Add(newBranch);
            }
        }

        #endregion

        #region Saving

        public static void SaveEvo(int evoNum)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;

                database.BeginTransaction();

                database.ExecuteNonQuery("DELETE FROM evolution WHERE num = \'" + evoNum + "\'");
                database.ExecuteNonQuery("DELETE FROM evolution_branch WHERE num = \'" + evoNum + "\'");

                database.UpdateOrInsert("evolution", new IDataColumn[] {
                database.CreateColumn(false, "num", evoNum.ToString()),
                database.CreateColumn(false, "name", evolution[evoNum].Name),
                database.CreateColumn(false, "species", evolution[evoNum].Species.ToString())
            });

                for (int i = 0; i < evolution[evoNum].Branches.Count; i++)
                {
                    database.UpdateOrInsert("evolution_branch", new IDataColumn[] {
                    database.CreateColumn(false, "num", evoNum.ToString()),
                    database.CreateColumn(false, "branch", i.ToString()),
                    database.CreateColumn(false, "name", evolution[evoNum].Branches[i].Name),
                    database.CreateColumn(false, "species", evolution[evoNum].Branches[i].NewSpecies.ToString()),
                    database.CreateColumn(false, "req_script", evolution[evoNum].Branches[i].ReqScript.ToString()),
                    database.CreateColumn(false, "data1", evolution[evoNum].Branches[i].Data1.ToString()),
                    database.CreateColumn(false, "data2", evolution[evoNum].Branches[i].Data2.ToString()),
                    database.CreateColumn(false, "data3", evolution[evoNum].Branches[i].Data3.ToString())
                });
                }
                database.EndTransaction();
            }
        }

        #endregion
    }
}
