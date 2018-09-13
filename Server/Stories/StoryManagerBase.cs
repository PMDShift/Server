using System;
using System.Collections.Generic;
using System.Text;
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


namespace Server.Stories
{
    public class StoryManagerBase
    {
        #region Fields

        static StoryCollection stories;

        #endregion Fields

        #region Events

        public static event EventHandler LoadComplete;

        public static event EventHandler<LoadingUpdateEventArgs> LoadUpdate;

        #endregion Events

        #region Properties

        public static StoryCollection Stories
        {
            get { return stories; }
        }

        #endregion Properties

        #region Methods


        public static void Initialize()
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                //method for getting count
                string query = "SELECT COUNT(num) FROM story";
                DataColumnCollection row = dbConnection.Database.RetrieveRow(query);

                int count = row["COUNT(num)"].ValueString.ToInt();

                stories = new StoryCollection(count);
            }
        }

        public static void LoadStories(object object1)
        {
            try
            {
                using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
                {
                    for (int i = 0; i <= stories.MaxStories; i++)
                    {
                        LoadStory(i, dbConnection.Database);
                        if (LoadUpdate != null)
                            LoadUpdate(null, new LoadingUpdateEventArgs(i, stories.MaxStories));
                    }
                    if (LoadComplete != null)
                        LoadComplete(null, null);
                }
            }
            catch (Exception ex)
            {
                Exceptions.ErrorLogger.WriteToErrorLog(ex);
            }
        }

        public static void LoadStory(int storyNum, PMDCP.DatabaseConnector.MySql.MySql database)
        {
            if (stories.Stories.ContainsKey(storyNum) == false)
                stories.Stories.Add(storyNum, new Story(storyNum.ToString()));
            Story story = new Story(storyNum.ToString());

            string query = "SELECT revision, " +
                "name, " +
                "story_start, " +
                "is_sandboxed, " +
                "zone_id " +
                "FROM story WHERE story.num = \'" + storyNum + "\'";

            DataColumnCollection row = database.RetrieveRow(query);
            if (row != null)
            {
                story.Revision = row["revision"].ValueString.ToInt();
                story.Name = row["name"].ValueString;
                story.StoryStart = row["story_start"].ValueString.ToInt();
                story.IsSandboxed = row["is_sandboxed"].ValueString.ToBool();
                story.ZoneID = row["zone_id"].ValueString.ToInt();
            }

            query = "SELECT segment, " +
                "action, " +
                "checkpoint " +
                "FROM story_segment WHERE story_segment.num = \'" + storyNum + "\'";

            List<DataColumnCollection> columnCollections = database.RetrieveRows(query);
            if (columnCollections == null) columnCollections = new List<DataColumnCollection>();
            foreach (DataColumnCollection columnCollection in columnCollections)
            {
                StorySegment segment = new StorySegment();

                int segmentNum = columnCollection["segment"].ValueString.ToInt();

                segment.Action = (Enums.StoryAction)columnCollection["action"].ValueString.ToInt();
                bool isCheckpoint = columnCollection["checkpoint"].ValueString.ToBool();

                string query2 = "SELECT param_key, " +
                    "param_val " +
                    "FROM story_param WHERE story_param.num = \'" + storyNum + "\' AND story_param.segment = \'" + segmentNum + "\'";

                List<DataColumnCollection> columnCollections2 = database.RetrieveRows(query2);
                if (columnCollections2 == null) columnCollections2 = new List<DataColumnCollection>();
                foreach (DataColumnCollection columnCollection2 in columnCollections2)
                {
                    string paramKey = columnCollection2["param_key"].ValueString;
                    string paramVal = columnCollection2["param_val"].ValueString;
                    segment.Parameters.Add(paramKey, paramVal);
                }
                story.Segments.Add(segment);

                if (isCheckpoint)
                {
                    story.ExitAndContinue.Add(segmentNum);
                }
            }

            stories.Stories[storyNum] = story;
        }


        public static void SaveStory(int storyNum)
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;

                database.BeginTransaction();

                database.ExecuteNonQuery("DELETE FROM story WHERE num = \'" + storyNum + "\'");
                database.ExecuteNonQuery("DELETE FROM story_segment WHERE num = \'" + storyNum + "\'");
                database.ExecuteNonQuery("DELETE FROM story_param WHERE num = \'" + storyNum + "\'");

                database.UpdateOrInsert("story", new IDataColumn[] {
                    database.CreateColumn(false, "num", storyNum.ToString()),
                    database.CreateColumn(false, "revision", stories[storyNum].Revision.ToString()),
                    database.CreateColumn(false, "name", stories[storyNum].Name),
                    database.CreateColumn(false, "story_start", stories[storyNum].StoryStart.ToString()),
                    database.CreateColumn(false, "is_sandboxed", stories[storyNum].IsSandboxed.ToIntString()),
                    database.CreateColumn(false, "zone_id", stories[storyNum].ZoneID.ToString())
                });

                for (int i = 0; i < stories[storyNum].Segments.Count; i++)
                {
                    bool isCheckPoint = false;
                    for (int j = 0; j < stories[storyNum].ExitAndContinue.Count; j++)
                    {
                        if (stories[storyNum].ExitAndContinue[j] == i)
                        {
                            isCheckPoint = true;
                            break;
                        }
                    }
                    database.UpdateOrInsert("story_segment", new IDataColumn[] {
                        database.CreateColumn(false, "num", storyNum.ToString()),
                        database.CreateColumn(false, "segment", i.ToString()),
                        database.CreateColumn(false, "action", ((int)stories[storyNum].Segments[i].Action).ToString()),
                        database.CreateColumn(false, "checkpoint", isCheckPoint.ToIntString())
                    });

                    for (int j = 0; j < stories[storyNum].Segments[i].Parameters.Count; j++)
                    {
                        database.UpdateOrInsert("story_param", new IDataColumn[] {
                        database.CreateColumn(false, "num", storyNum.ToString()),
                        database.CreateColumn(false, "segment", i.ToString()),
                        database.CreateColumn(false, "param_key", stories[storyNum].Segments[i].Parameters.KeyByIndex(j)),
                        database.CreateColumn(false, "param_val", stories[storyNum].Segments[i].Parameters.ValueByIndex(j))
                    });
                    }
                }
                database.EndTransaction();
            }
        }

        #endregion Methods
    }
}