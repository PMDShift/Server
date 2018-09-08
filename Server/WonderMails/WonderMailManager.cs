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
using PMDCP.DatabaseConnector;
using PMDCP.DatabaseConnector.MySql;
using Server.Database;

namespace Server.WonderMails {
    public class WonderMailManager {

        static MissionPoolCollection missionPools;


        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        public static void Initialize() {
            missionPools = new MissionPoolCollection();
        }

        public static MissionPoolCollection Missions {
            get { return missionPools; }
        }

        public static void LoadMissionPools(object object1) {
            int count = Enum.GetValues(typeof(Enums.JobDifficulty)).Length;
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data)) {
                for (int i = 0; i < count; i++) {
                    try {
                        LoadMissionPool(dbConnection, i);
                        if (LoadUpdate != null)
                            LoadUpdate(null, new LoadingUpdateEventArgs(i, count - 1));
                    } catch (Exception ex) {
                        Exceptions.ErrorLogger.WriteToErrorLog(ex, "Loading MissionPool #" + i.ToString());
                    }
                }
            }
            if (LoadComplete != null)
                LoadComplete(null, null);
        }

        public static void LoadMissionPool(DatabaseConnection dbConnection, int difficulty) {
            MissionPool missionPool = new MissionPool();
            var database = dbConnection.Database;

            string query = "SELECT mission_client.DexNum, mission_client.FormNum " +
                "FROM mission_client " +
                "WHERE mission_client.Rank = \'" + difficulty + "\'  " +
                "ORDER BY mission_client.ClientIndex";

            foreach (DataColumnCollection column in database.RetrieveRowsEnumerable(query))
            {
                MissionClientData data = new MissionClientData();
                data.Species = column["DexNum"].ValueString.ToInt();
                data.Form = column["FormNum"].ValueString.ToInt();
                missionPool.MissionClients.Add(data);
            }

            query = "SELECT mission_enemy.NpcNum " +
                "FROM mission_enemy " +
                "WHERE mission_enemy.Rank = \'" + difficulty + "\'  " +
                "ORDER BY mission_enemy.EnemyIndex";

            foreach (DataColumnCollection column in database.RetrieveRowsEnumerable(query))
            {
                MissionEnemyData data = new MissionEnemyData();
                data.NpcNum = column["NpcNum"].ValueString.ToInt();
                missionPool.Enemies.Add(data);
            }
            
            query = "SELECT mission_reward.ItemNum, mission_reward.ItemAmount, mission_reward.ItemTag " +
                "FROM mission_reward " +
                "WHERE mission_reward.Rank = \'" + difficulty + "\'  " +
                "ORDER BY mission_reward.RewardIndex";

            foreach (DataColumnCollection column in database.RetrieveRowsEnumerable(query))
            {
                MissionRewardData data = new MissionRewardData();
                data.ItemNum = column["ItemNum"].ValueString.ToInt();
                data.Amount = column["ItemAmount"].ValueString.ToInt();
                data.Tag = column["ItemTag"].ValueString;
                missionPool.Rewards.Add(data);
            }


            missionPools.MissionPools.Add(missionPool);
        }

        public static void SaveMissionPool(DatabaseConnection dbConnection, int difficulty)
        {
            var database = dbConnection.Database;

            database.ExecuteNonQuery("DELETE FROM mission_client WHERE Rank = \'" + difficulty + "\'");
            
            for (int i = 0; i < missionPools.MissionPools[difficulty].MissionClients.Count; i++) {
                MissionClientData data = missionPools.MissionPools[difficulty].MissionClients[i];
                
            }

            database.ExecuteNonQuery("DELETE FROM mission_enemy WHERE Rank = \'" + difficulty + "\'");
            for (int i = 0; i < missionPools.MissionPools[difficulty].Enemies.Count; i++) {
                MissionEnemyData data = missionPools.MissionPools[difficulty].Enemies[i];

                database.AddRow("mission_enemy", new IDataColumn[] {
                    database.CreateColumn(false, "Rank", difficulty.ToString()),
                    database.CreateColumn(false, "EnemyIndex", i.ToString()),
                    database.CreateColumn(false, "NpcNum", data.NpcNum.ToString())
                });
            }

            database.ExecuteNonQuery("DELETE FROM mission_reward WHERE Rank = \'" + difficulty + "\'");
            for (int i = 0; i < missionPools.MissionPools[difficulty].Rewards.Count; i++) {
                MissionRewardData data = missionPools.MissionPools[difficulty].Rewards[i];


                database.AddRow("mission_reward", new IDataColumn[] {
                    database.CreateColumn(false, "Rank", difficulty.ToString()),
                    database.CreateColumn(false, "RewardIndex", i.ToString()),
                    database.CreateColumn(false, "ItemNum", data.ItemNum.ToString()),
                    database.CreateColumn(false, "ItemAmount", data.Amount.ToString()),
                    database.CreateColumn(false, "ItemTag", data.Tag)
                });
            }
        }
    }
}
