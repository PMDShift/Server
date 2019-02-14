using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using PMDCP.DatabaseConnector.MySql;
using PMDCP.DatabaseConnector;
using Server.Database;
using System.IO;
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



namespace Server
{
    public class Settings
    {
        public static string GameName { get; set; }
        public static string GameNameShort { get; set; }
        public static string MOTD { get; set; }

        public static List<string> News { get; set; }

        public static int StartMap { get; set; }
        public static int StartX { get; set; }
        public static int StartY { get; set; }
        public static int NewCharForm { get; set; }
        public static int NewCharSpecies { get; set; }
        public static int Crossroads { get; set; }

        public static int GamePort { get; set; }
        public static string DatabaseIP { get; set; }
        public static int DatabasePort { get; set; }
        public static string DatabaseUser { get; set; }
        public static string DatabasePassword { get; set; }

        public static string DiscordBotToken { get; set; }
        public static ulong DiscordGeneralChannel { get; set; }

        public static XmlWriterSettings XmlWriterSettings { get; set; }



        public static void Initialize()
        {
            XmlWriterSettings = new System.Xml.XmlWriterSettings();
            XmlWriterSettings.OmitXmlDeclaration = false;
            XmlWriterSettings.IndentChars = "  ";
            XmlWriterSettings.Indent = true;
            XmlWriterSettings.NewLineChars = Environment.NewLine;

            News = new List<string>();
        }

        public static void LoadConfig()
        {
            using (XmlReader reader = XmlReader.Create(Path.Combine(IO.Paths.DataFolder, "config.xml")))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "GamePort":
                                if (reader.Read())
                                {
                                    GamePort = reader.ReadString().ToInt();
                                }
                                break;
                            case "DatabaseIP":
                                if (reader.Read())
                                {
                                    DatabaseIP = reader.ReadString();
                                }
                                break;
                            case "DatabasePort":
                                if (reader.Read())
                                {
                                    DatabasePort = reader.ReadString().ToInt();
                                }
                                break;
                            case "DatabaseUser":
                                if (reader.Read())
                                {
                                    DatabaseUser = reader.ReadString();
                                }
                                break;
                            case "DatabasePassword":
                                if (reader.Read())
                                {
                                    DatabasePassword = reader.ReadString();
                                }
                                break;
                            case "DiscordBotToken":
                                {
                                    if (reader.Read())
                                    {
                                        DiscordBotToken = reader.ReadString();
                                    }
                                }
                                break;
                            case "DiscordGeneralChannel":
                                {
                                    if (reader.Read())
                                    {
                                        DiscordGeneralChannel = reader.ReadString().ToUlng();
                                    }
                                }
                                break;
                        }
                    }
                }
            }

            var gamePortEnvironmentVariable = Environment.GetEnvironmentVariable("PMDCP_GAME_PORT");
            if (!string.IsNullOrEmpty(gamePortEnvironmentVariable))
            {
                GamePort = gamePortEnvironmentVariable.ToInt();
            }

            var databaseIPEnvironmentVariable = Environment.GetEnvironmentVariable("PMDCP_DATABASE_IP");
            if (!string.IsNullOrEmpty(databaseIPEnvironmentVariable))
            {
                DatabaseIP = databaseIPEnvironmentVariable;
            }

            var databasePortEnvironmentVariable = Environment.GetEnvironmentVariable("PMDCP_DATABASE_PORT");
            if (!string.IsNullOrEmpty(databasePortEnvironmentVariable))
            {
                DatabasePort = databasePortEnvironmentVariable.ToInt();
            }

            var databaseUserEnvironmentVariable = Environment.GetEnvironmentVariable("PMDCP_DATABASE_USER");
            if (!string.IsNullOrEmpty(databaseUserEnvironmentVariable))
            {
                DatabaseUser = databaseUserEnvironmentVariable;
            }

            var databasePasswordEnvironmentVariable = Environment.GetEnvironmentVariable("PMDCP_DATABASE_PASSWORD");
            if (!string.IsNullOrEmpty(databasePasswordEnvironmentVariable))
            {
                DatabasePassword = databasePasswordEnvironmentVariable;
            }

            var discordBotTokenEnvironmentVariable = Environment.GetEnvironmentVariable("PMDCP_DISCORD_BOT_TOKEN");
            if (!string.IsNullOrEmpty(discordBotTokenEnvironmentVariable))
            {
                DiscordBotToken = discordBotTokenEnvironmentVariable;
            }

            var discordBotGeneralChannelEnvironmentVariable = Environment.GetEnvironmentVariable("PMDCP_DISCORD_GENERAL_CHANNEL");
            if (!string.IsNullOrEmpty(discordBotGeneralChannelEnvironmentVariable))
            {
                DiscordGeneralChannel = discordBotGeneralChannelEnvironmentVariable.ToUlng();
            }

            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;
                //load most recent news
                string query = "SELECT id, message " +
                    "FROM title WHERE title.id = 'GameName' OR title.id = 'MOTD' OR title.id = 'GameNameShort'";

                foreach (DataColumnCollection columnCollection in database.RetrieveRowsEnumerable(query))
                {
                    switch (columnCollection["id"].ValueString)
                    {
                        case "GameName":
                            {
                                GameName = columnCollection["message"].ValueString;
                            }
                            break;
                        case "GameNameShort":
                            {
                                GameNameShort = columnCollection["message"].ValueString;
                            }
                            break;
                        case "MOTD":
                            {
                                MOTD = columnCollection["message"].ValueString;
                            }
                            break;
                    }
                }

                query = "SELECT id, val " +
                   "FROM start_value " +
                   "WHERE start_value.id = 'Crossroads' " +
                   "OR start_value.id = 'NewCharForm' " +
                   "OR start_value.id = 'NewCharSpecies' " +
                   "OR start_value.id = 'StartMap' " +
                   "OR start_value.id = 'StartX' " +
                   "OR start_value.id = 'StartY'";

                foreach (DataColumnCollection columnCollection in database.RetrieveRowsEnumerable(query))
                {
                    switch (columnCollection["id"].ValueString)
                    {
                        case "Crossroads":
                            {
                                Crossroads = columnCollection["val"].ValueString.ToInt();
                            }
                            break;
                        case "NewCharForm":
                            {
                                NewCharForm = columnCollection["val"].ValueString.ToInt();
                            }
                            break;
                        case "NewCharSpecies":
                            {
                                NewCharSpecies = columnCollection["val"].ValueString.ToInt();
                            }
                            break;
                        case "StartMap":
                            {
                                StartMap = columnCollection["val"].ValueString.ToInt();
                            }
                            break;
                        case "StartX":
                            {
                                StartX = columnCollection["val"].ValueString.ToInt();
                            }
                            break;
                        case "StartY":
                            {
                                StartY = columnCollection["val"].ValueString.ToInt();
                            }
                            break;
                    }
                }
            }
        }

        public static void LoadNews()
        {
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;
                //load most recent news
                string query = "SELECT num, news_time, message " +
                    "FROM news WHERE news.num > (SELECT COUNT(num) FROM news) - 10";

                foreach (DataColumnCollection columnCollection in database.RetrieveRowsEnumerable(query))
                {
                    int num = columnCollection["num"].ValueString.ToInt();

                    News.Add("[" + columnCollection["news_time"].ValueString + "] " + columnCollection["message"].ValueString);
                }
            }
        }

        public static void SaveMOTD()
        {
            //save motd
            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;

                database.UpdateOrInsert("title", new IDataColumn[] {
                    database.CreateColumn(true, "id", "MOTD"),
                    database.CreateColumn(false, "message", MOTD)
                });
            }
        }

        public static void AddNews(string newNews)
        {
            string date = DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss");

            News.Add("[" + date + "] " + newNews);

            using (DatabaseConnection dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var database = dbConnection.Database;

                database.AddRow("news", new IDataColumn[] {
                    database.CreateColumn(false, "message", newNews)
                });
            }
        }
    }
}
