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

        public static async Task SyncUsersWithZoneRole(SocketCommandContext context, int zoneID, global::Discord.IRole role = null)
        {
            var roleName = $"zone-{zoneID}";

            await context.Guild.DownloadUsersAsync();

            if (role == null)
            {
                role = context.Guild.Roles.Where(x => x.Name == roleName).FirstOrDefault();
            }

            if (role == null)
            {
                return;
            }

            var zone = ZoneManager.Zones[zoneID];

            var validRoleUsers = new HashSet<ulong>();

            using (var dbConnection = new DatabaseConnection(DatabaseID.Players))
            {
                foreach (var member in zone.Members)
                {
                    var discordID = PlayerDataManager.FindLinkedCharacterDiscord(dbConnection.Database, member.CharacterID);

                    if (discordID > 0)
                    {
                        var user = context.Guild.Users.Where(x => x.Id == discordID).FirstOrDefault();

                        if (user != null)
                        {
                            var userRole = user.Roles.Where(x => x.Name == roleName).FirstOrDefault();

                            if (userRole == null)
                            {
                                await user.AddRoleAsync(role);
                            }

                            validRoleUsers.Add(discordID);
                        }
                    }
                }
            }

            foreach (var user in context.Guild.Users)
            {
                if (!validRoleUsers.Contains(user.Id))
                {
                    var userRole = user.Roles.Where(x => x.Name == roleName).FirstOrDefault();

                    if (userRole != null)
                    {
                        await userRole.DeleteAsync();
                    }
                }
            }
        }
    }
}
