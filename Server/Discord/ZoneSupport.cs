using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.Players;
using Discord.Commands;
using Discord.WebSocket;
using Server.Database;
using Server.Zones;

namespace Server.Discord
{
    public class ZoneSupport
    {
        public static async Task<bool> IsUserLeader(SocketCommandContext context, int zoneID, SocketUser user)
        {
            // Allow application owner to always manage everything
            var application = await context.Client.GetApplicationInfoAsync();
            if (application.Owner.Id == user.Id)
            {
                return true;
            }

            string characterID;
            using (var dbConnection = new DatabaseConnection(DatabaseID.Players))
            {
                characterID = PlayerDataManager.FindLinkedDiscordCharacter(dbConnection.Database, user.Id);
            }

            if (string.IsNullOrEmpty(characterID))
            {
                return false;
            }

            var zone = ZoneManager.Zones[zoneID];

            var member = zone.Members.Where(x => x.CharacterID == characterID).FirstOrDefault();
            if (member == null)
            {
                return false;
            }

            return (member.Access == Enums.ZoneAccess.Leader);
        }
    }
}
