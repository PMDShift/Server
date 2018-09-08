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

using PMDCP.DatabaseConnector;
using PMDCP.DatabaseConnector.MySql;
using Server.Database;

namespace Server
{
    public class Bans
    {
    // Bans a player
        public static void BanPlayer(DatabaseConnection dbConnection, string ip, string bannedID, string bannedAccount,
            string bannedMac, string bannedUUID, string bannerID, string bannerIP, string unbanDate, Enums.BanType banType)
        {
            IDataColumn[] columns = new IDataColumn[] {
                dbConnection.Database.CreateColumn(true, "BannedPlayerID", bannedID),
                dbConnection.Database.CreateColumn(false, "BannedPlayerAccount", bannedAccount),
                dbConnection.Database.CreateColumn(false, "BannedPlayerIP", ip),
                dbConnection.Database.CreateColumn(false, "BannedPlayerMac", bannedMac),
                dbConnection.Database.CreateColumn(false, "BannerPlayerID", bannerID),
                dbConnection.Database.CreateColumn(false, "BannerPlayerIP",bannerIP),
                dbConnection.Database.CreateColumn(false, "BannedDate", DateTime.Now.ToString()),
                dbConnection.Database.CreateColumn(false, "UnbanDate", unbanDate),
                dbConnection.Database.CreateColumn(false, "BanType", ((int)banType).ToString())
            };
            dbConnection.Database.UpdateOrInsert("bans", columns);
        }

        public static Enums.BanType IsIPBanned(DatabaseConnection dbConnection, Client client) {
            string ipToTest = client.IP.ToString();
            return IsBanned(dbConnection, Enums.BanMethod.PlayerIP, ipToTest);
        }

        public static Enums.BanType IsMacBanned(DatabaseConnection dbConnection, Client client) {
            if (client.MacAddress == "") return Enums.BanType.None;
            return IsBanned(dbConnection, Enums.BanMethod.PlayerMAC, client.MacAddress);
        }

        public static Enums.BanType IsCharacterBanned(DatabaseConnection dbConnection, string id) {
            return IsBanned(dbConnection, Enums.BanMethod.PlayerID, id);
        }

        public static Enums.BanType IsBanned(DatabaseConnection dbConnection, Enums.BanMethod banMethod, string value) {
            IDataColumn[] columns = RetrieveField(dbConnection, "UnbanDate", banMethod, value);
            IDataColumn[] dataColumns = RetrieveField(dbConnection, "BanType", banMethod, value);
            if (columns != null) {
                string unbanDate = (string)columns[0].Value;
                if (unbanDate == "-----") {
                    // It's a permanent ban.
                    return (Enums.BanType)((int)dataColumns[0].Value);
                } else {
                    // It's a temp ban
                    DateTime dtUnbanDate = DateTime.Parse(unbanDate);
                    if (DateTime.Now > dtUnbanDate) {
                        RemoveBan(dbConnection, banMethod, value);
                        return Enums.BanType.None;
                    } else {
                        return (Enums.BanType)((int)dataColumns[0].Value);
                    }
                }
            } else {
                // columns was null, which means their entry was not found
                return Enums.BanType.None;
            }
        }

        private static string SelectColumnForBanMethod(Enums.BanMethod banMethod) {
            switch (banMethod) {
                case Enums.BanMethod.PlayerIP:
                    return "BannedPlayerIP";
                case Enums.BanMethod.PlayerMAC:
                    return "BannedPlayerMac";
                case Enums.BanMethod.PlayerID:
                    return "BannedPlayerID";
                default:
                    throw new NotSupportedException();
            }
        }

        public static void RemoveBan(DatabaseConnection dbConnection, Enums.BanMethod banMethod, string value) {
            var column = SelectColumnForBanMethod(banMethod);

            dbConnection.Database.DeleteRow("bans", $"{column} = @Value", new { Value = value });
        }

        private static IDataColumn[] RetrieveField(DatabaseConnection dbConnection, string fieldToRetrieve, Enums.BanMethod banMethod, string valueToSearch) {
            var column = SelectColumnForBanMethod(banMethod);

            IDataColumn[] columns = dbConnection.Database.RetrieveRow("bans", fieldToRetrieve, column + "=\"" + valueToSearch + "\"");
            if (columns != null) {
                return columns;
            } else {
                return null;
            }
        }


    }
}
