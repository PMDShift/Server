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

namespace Server.Discord.Commands
{
    [Group("zone")]
    [Alias("zones")]
    public class ZonesModule : ModuleBase<SocketCommandContext>
    {
        [Command]
        [Summary("View the details of a zone.")]
        public async Task DetailsAsync(int zoneID)
        {
            if (!ZoneManager.Zones.Zones.ContainsKey(zoneID))
            {
                await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                return;
            }

            var zone = ZoneManager.Zones[zoneID];

            var responseBuilder = new StringBuilder();
            responseBuilder.AppendLine("**Zone Details**");
            responseBuilder.AppendLine();
            responseBuilder.AppendLine($"Name: `{zone.Name}`");
            responseBuilder.AppendLine($"Open for Editing: {zone.IsOpen}");
            responseBuilder.AppendLine();

            responseBuilder.AppendLine("**Options:**");
            responseBuilder.AppendLine($"Visitors: {zone.AllowVisitors}");

            responseBuilder.AppendLine();

            using (var dbConnection = new DatabaseConnection(DatabaseID.Data))
            {
                var zoneResources = new List<ZoneResource>();
                zoneResources.AddRange(Dungeons.DungeonManager.LoadZoneResources(dbConnection.Database, zoneID));
                zoneResources.AddRange(Items.ItemManager.LoadZoneResources(dbConnection.Database, zoneID));
                zoneResources.AddRange(Maps.MapManager.LoadZoneResources(dbConnection.Database, zoneID));
                zoneResources.AddRange(Npcs.NpcManager.LoadZoneResources(dbConnection.Database, zoneID));
                zoneResources.AddRange(RDungeons.RDungeonManager.LoadZoneResources(dbConnection.Database, zoneID));
                zoneResources.AddRange(Shops.ShopManager.LoadZoneResources(dbConnection.Database, zoneID));
                zoneResources.AddRange(Stories.StoryManager.LoadZoneResources(dbConnection.Database, zoneID));

                responseBuilder.AppendLine("**Resources:**");
                responseBuilder.AppendLine();
                responseBuilder.Append(GenerateResourceListing(zoneResources));
            }

            await Context.Channel.SendMessageAsync(responseBuilder.ToString());
        }

        [Command("list")]
        [Summary("View a list of your assigned zones.")]
        public async Task ListAsync()
        {
            string characterID;
            using (var dbConnection = new DatabaseConnection(DatabaseID.Players))
            {
                characterID = PlayerDataManager.FindLinkedDiscordCharacter(dbConnection.Database, Context.User.Id);
            }

            if (string.IsNullOrEmpty(characterID))
            {
                await Context.Channel.SendMessageAsync("You have not linked your Discord account with your in-game account yet. Unable to list zones.");
                return;
            }

            var responseBuilder = new StringBuilder();
            responseBuilder.AppendLine("**Your zones:**");

            for (var i = 0; i < ZoneManager.Zones.Count; i++)
            {
                var zone = ZoneManager.Zones[i];

                var zoneMember = zone.Members.Where(x => x.CharacterID == characterID).FirstOrDefault();
                if (zoneMember != null)
                {
                    responseBuilder.AppendLine($"{i}. `{zone.Name}` - {zoneMember.Access}");
                }
            }

            await Context.Channel.SendMessageAsync(responseBuilder.ToString());
        }

        [Command("help")]
        [Summary("View help information about zones.")]
        public async Task HelpAsync()
        {
            var responseBuilder = new StringBuilder();
            responseBuilder.AppendLine("**How to use zones:**");
            responseBuilder.AppendLine("/zone list - View a list of your zones.");
            responseBuilder.AppendLine("/zone [number] - View the details of the requested zone.");
            responseBuilder.AppendLine("/zone members [number] - View the members of the requested zone.");

            responseBuilder.AppendLine();

            responseBuilder.AppendLine("**Zone leader commands:**");
            responseBuilder.AppendLine("/zone members add [number] @name [Leader/Member/Viewer] - Add a user to your zone.");
            responseBuilder.AppendLine("/zone members remove [number] @name - Remove a user from your zone.");
            responseBuilder.AppendLine("/zone enable [number] [option] - Enable a zone option (see available options below).");
            responseBuilder.AppendLine("/zone disable [number] [option] - Enable a zone option (see available options below).");
            responseBuilder.AppendLine("/zone channel [number] [open/close] - Open/close a Discord channel for members of the zone.");

            responseBuilder.AppendLine();

            responseBuilder.AppendLine("**Zone options:**");
            responseBuilder.AppendLine("Visitors - Allows any players to view all resources in the zone, regardless if they are on the zone team.");

            responseBuilder.AppendLine();

            responseBuilder.AppendLine("**Types of zone members:**");
            responseBuilder.AppendLine("Leader - Able to add and remove members from zones.");
            responseBuilder.AppendLine("Member - Able to view and edit the resources in a zone.");
            responseBuilder.AppendLine("Viewer - Able to view the resources in a zone.");

            await Context.Channel.SendMessageAsync(responseBuilder.ToString());
        }

        [Command("channel")]
        [Summary("Open/close a Discord channel for members of the zone.")]
        public async Task ChannelAsync(int zoneID, string command)
        {
            if (!ZoneManager.Zones.Zones.ContainsKey(zoneID))
            {
                await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                return;
            }

            if (await ZoneSupport.IsUserLeader(Context, zoneID, Context.User) == false)
            {
                await Context.Channel.SendMessageAsync("You are not a leader of this zone.");
                return;
            }

            var zone = ZoneManager.Zones[zoneID];

            var channelName = $"{zoneID}-{zone.Name.Substring(0, System.Math.Min(zone.Name.Length, 17))}";

            channelName = channelName.ToLower().Replace(' ', '-');

            var roleName = $"zone-{zoneID}";

            switch (command.ToLower())
            {
                case "open":
                    {
                        var category = Context.Guild.CategoryChannels.Where(x => x.Name.ToLower() == "zones").FirstOrDefault();

                        if (category == null)
                        {
                            await Context.Channel.SendMessageAsync("Category not found.");
                            return;
                        }

                        var channel = await Context.Guild.CreateTextChannelAsync(channelName);

                        await channel.ModifyAsync(o =>
                        {
                            o.CategoryId = category.Id;
                            o.Topic = $"Discussion for Zone {zoneID} - {zone.Name}.";
                        });

                        global::Discord.IRole role = Context.Guild.Roles.Where(x => x.Name == roleName).FirstOrDefault();
                        if (role == null)
                        {
                            role = await Context.Guild.CreateRoleAsync(roleName);
                        }

                        var botRole = Context.Guild.Roles.Where(x => x.Name == "Bot").FirstOrDefault();
                        if (botRole != null)
                        {
                            await channel.AddPermissionOverwriteAsync(botRole, new global::Discord.OverwritePermissions(readMessages: global::Discord.PermValue.Allow, sendMessages: global::Discord.PermValue.Allow));
                        }

                        await channel.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, new global::Discord.OverwritePermissions(readMessages: global::Discord.PermValue.Deny, sendMessages: global::Discord.PermValue.Deny));
                        await channel.AddPermissionOverwriteAsync(role, new global::Discord.OverwritePermissions(readMessages: global::Discord.PermValue.Allow, sendMessages: global::Discord.PermValue.Allow));

                        await ZoneSupport.SyncUsersWithZoneRole(Context, zoneID, role);
                    }
                    break;
                case "close":
                    {
                        var channel = Context.Guild.Channels.Where(x => x.Name == channelName).FirstOrDefault();

                        if (channel != null)
                        {
                            await channel.DeleteAsync();

                            await Context.Channel.SendMessageAsync("Channel closed!");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("Channel not found.");
                        }

                        var role = Context.Guild.Roles.Where(x => x.Name == roleName).FirstOrDefault();
                        if (role != null)
                        {
                            await role.DeleteAsync();
                        }
                    }
                    break;
            }
        }

        [Command("rename")]
        [Summary("Rename a zone.")]
        public async Task RenameAsync(int zoneID, string zoneName)
        {
            if (!ZoneManager.Zones.Zones.ContainsKey(zoneID))
            {
                await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                return;
            }

            if (await ZoneSupport.IsUserLeader(Context, zoneID, Context.User) == false)
            {
                await Context.Channel.SendMessageAsync("You are not a leader of this zone.");
                return;
            }

            var zone = ZoneManager.Zones[zoneID];

            zone.Name = zoneName;

            ZoneManager.SaveZone(zoneID);

            await Context.Channel.SendMessageAsync($"The zone has been renamed to `{zone.Name}`!");
        }

        [Command("enable")]
        [Summary("Enable a zone option.")]
        public async Task EnableAsync(int zoneID, ZoneOption zoneOption)
        {
            if (!ZoneManager.Zones.Zones.ContainsKey(zoneID))
            {
                await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                return;
            }

            if (await ZoneSupport.IsUserLeader(Context, zoneID, Context.User) == false)
            {
                await Context.Channel.SendMessageAsync("You are not a leader of this zone.");
                return;
            }

            var zone = ZoneManager.Zones[zoneID];

            switch (zoneOption)
            {
                case ZoneOption.Visitors:
                    {
                        zone.AllowVisitors = true;
                    }
                    break;
            }

            ZoneManager.SaveZone(zoneID);

            await Context.Channel.SendMessageAsync($"The `{zoneOption}` option is now enabled on `{zone.Name}`!");
        }

        [Command("disable")]
        [Summary("Disable a zone option.")]
        public async Task DisableAsync(int zoneID, ZoneOption zoneOption)
        {
            if (!ZoneManager.Zones.Zones.ContainsKey(zoneID))
            {
                await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                return;
            }

            if (await ZoneSupport.IsUserLeader(Context, zoneID, Context.User) == false)
            {
                await Context.Channel.SendMessageAsync("You are not a leader of this zone.");
                return;
            }

            var zone = ZoneManager.Zones[zoneID];

            switch (zoneOption)
            {
                case ZoneOption.Visitors:
                    {
                        zone.AllowVisitors = false;
                    }
                    break;
            }

            ZoneManager.SaveZone(zoneID);

            await Context.Channel.SendMessageAsync($"The `{zoneOption}` option is now disabled on `{zone.Name}`!");
        }

        [Command("create")]
        [Summary("Create a new zone.")]
        [RequireOwner]
        public async Task CreateAsync(string name)
        {
            var zone = new Zone()
            {
                Name = name,
                IsOpen = true
            };

            ZoneManager.CreateZone(zone);

            await Context.Channel.SendMessageAsync($"Zone #{zone.Num}, `{zone.Name}`, has been created!");
        }

        [Command("open")]
        [Summary("Opens a zone for editing.")]
        [RequireOwner]
        public async Task OpenAsync(int id)
        {
            if (!ZoneManager.Zones.Zones.ContainsKey(id))
            {
                await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                return;
            }

            var zone = ZoneManager.Zones[id];

            zone.IsOpen = true;

            ZoneManager.SaveZone(id);

            await Context.Channel.SendMessageAsync($"Zone #{id} has been opened!");
        }

        [Command("close")]
        [Summary("Closes a zone for editing.")]
        [RequireOwner]
        public async Task CloseAsync(int id)
        {
            if (!ZoneManager.Zones.Zones.ContainsKey(id))
            {
                await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                return;
            }

            var zone = ZoneManager.Zones[id];

            zone.IsOpen = false;

            ZoneManager.SaveZone(id);

            await Context.Channel.SendMessageAsync($"Zone #{id} has been closed!");
        }

        [Command("add")]
        [Summary("Adds a resource to a zone.")]
        [RequireOwner]
        public async Task AddAsync(int id, ZoneResourceType resource, int amount)
        {
            if (!ZoneManager.Zones.Zones.ContainsKey(id))
            {
                await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                return;
            }

            var addedResources = ZoneManager.AddResources(id, resource, amount);

            var responseBuilder = new StringBuilder();
            responseBuilder.AppendLine("New resources have been added:");
            responseBuilder.AppendLine();
            var resourceListing = GenerateResourceListing(addedResources);

            responseBuilder.Append(resourceListing);

            await Context.Channel.SendMessageAsync(responseBuilder.ToString());
        }

        [Command("preset")]
        [Summary("Add a preset of resources to a zone.")]
        [RequireOwner]
        public async Task AddPresetAsync(int id, ZonePresetType presetType)
        {
            var addedResources = new List<ZoneResource>();

            switch (presetType)
            {
                case ZonePresetType.RDungeon:
                    {
                        addedResources.AddRange(ZoneManager.AddResources(id, ZoneResourceType.Dungeons, 1));
                        addedResources.AddRange(ZoneManager.AddResources(id, ZoneResourceType.RDungeons, 1));
                        addedResources.AddRange(ZoneManager.AddResources(id, ZoneResourceType.Maps, 5));
                        addedResources.AddRange(ZoneManager.AddResources(id, ZoneResourceType.NPCs, 25));
                        addedResources.AddRange(ZoneManager.AddResources(id, ZoneResourceType.Stories, 5));
                    }
                    break;
            }

            var responseBuilder = new StringBuilder();
            responseBuilder.AppendLine("New resources have been added:");
            responseBuilder.AppendLine();
            var resourceListing = GenerateResourceListing(addedResources);

            responseBuilder.Append(resourceListing);

            await Context.Channel.SendMessageAsync(responseBuilder.ToString());
        }

        private string GenerateResourceListing(IReadOnlyCollection<ZoneResource> zoneResources)
        {
            var responseBuilder = new StringBuilder();
            foreach (var zoneResourceGroup in zoneResources.GroupBy(x => x.Type))
            {
                responseBuilder.AppendLine($"**{zoneResourceGroup.Key}**");

                foreach (var zoneResource in zoneResourceGroup.OrderBy(x => x.Num))
                {
                    var realResourceNumber = zoneResource.Num;

                    switch (zoneResource.Type)
                    {
                        case ZoneResourceType.Stories:
                        case ZoneResourceType.RDungeons:
                        case ZoneResourceType.Dungeons:
                            realResourceNumber++;
                            break;
                    }

                    responseBuilder.AppendLine($"{realResourceNumber} - {zoneResource.Name}");
                }
            }

            return responseBuilder.ToString();
        }

        [Group("member")]
        [Alias("members")]
        public class Members : ModuleBase<SocketCommandContext>
        {
            [Command]
            public async Task ListAsync(int zoneID)
            {
                if (!ZoneManager.Zones.Zones.ContainsKey(zoneID))
                {
                    await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                    return;
                }

                var zone = ZoneManager.Zones[zoneID];

                var responseBuilder = new StringBuilder();
                responseBuilder.AppendLine("**Zone Members**");
                responseBuilder.AppendLine();
                if (zone.Members.Count == 0)
                {
                    responseBuilder.AppendLine("No members have been added yet.");
                }
                else
                {
                    using (var dbConnection = new DatabaseConnection(DatabaseID.Players))
                    {
                        for (var i = 0; i < zone.Members.Count; i++)
                        {
                            var member = zone.Members[i];

                            // TODO: This is **REALLY** inefficient. Improve this later
                            var name = PlayerDataManager.GetCharacterName(dbConnection.Database, member.CharacterID);

                            responseBuilder.AppendLine($"{i + 1}. `{name}` - {member.Access}");
                        }
                    }
                }

                await Context.Channel.SendMessageAsync(responseBuilder.ToString());
            }

            [Command("add")]
            [Summary("Add a user to a zone")]
            public async Task AddAsync(int zoneID, SocketGuildUser user, string access = "")
            {
                if (!ZoneManager.Zones.Zones.ContainsKey(zoneID))
                {
                    await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                    return;
                }

                if (await ZoneSupport.IsUserLeader(Context, zoneID, Context.User) == false)
                {
                    await Context.Channel.SendMessageAsync("You are not a leader of this zone.");
                    return;
                }

                var zone = ZoneManager.Zones[zoneID];

                Enums.ZoneAccess accessValue;
                switch (access.ToLower())
                {
                    case "viewer":
                        {
                            accessValue = Enums.ZoneAccess.Viewer;
                        }
                        break;
                    case "member":
                        {
                            accessValue = Enums.ZoneAccess.Member;
                        }
                        break;
                    case "leader":
                        {
                            accessValue = Enums.ZoneAccess.Leader;
                        }
                        break;
                    default:
                        {
                            accessValue = Enums.ZoneAccess.Viewer;
                        }
                        break;
                }

                string characterID;
                using (var dbConnection = new DatabaseConnection(DatabaseID.Players))
                {
                    characterID = PlayerDataManager.FindLinkedDiscordCharacter(dbConnection.Database, user.Id);
                }

                if (string.IsNullOrEmpty(characterID))
                {
                    await Context.Channel.SendMessageAsync("That user has not linked their Discord account with their in-game account yet. Unable to add to the zone.");
                    return;
                }

                bool foundMember = false;

                var zoneMember = zone.Members.Where(x => x.CharacterID == characterID).FirstOrDefault();
                if (zoneMember == null)
                {
                    zoneMember = new ZoneMember()
                    {
                        Access = accessValue,
                        ZoneID = zoneID,
                        CharacterID = characterID
                    };

                    zone.Members.Add(zoneMember);
                }
                else
                {
                    zoneMember.Access = accessValue;
                    foundMember = true;
                }

                ZoneManager.SaveZone(zoneID);

                await ZoneSupport.SyncUsersWithZoneRole(Context, zoneID);

                if (!foundMember)
                {
                    await Context.Channel.SendMessageAsync($"User added as a `{accessValue}` to `{zone.Name}`!");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"User updated to be a `{accessValue}` in `{zone.Name}`!");
                }
            }

            [Command("remove")]
            [Summary("Remove a user from a zone.")]
            public async Task RemoveAsync(int zoneID, SocketGuildUser user)
            {
                if (!ZoneManager.Zones.Zones.ContainsKey(zoneID))
                {
                    await Context.Channel.SendMessageAsync("Invalid zone id specified.");
                    return;
                }

                if (await ZoneSupport.IsUserLeader(Context, zoneID, Context.User) == false)
                {
                    await Context.Channel.SendMessageAsync("You are not a leader of this zone.");
                    return;
                }

                string characterID;
                using (var dbConnection = new DatabaseConnection(DatabaseID.Players))
                {
                    characterID = PlayerDataManager.FindLinkedDiscordCharacter(dbConnection.Database, user.Id);
                }

                if (string.IsNullOrEmpty(characterID))
                {
                    await Context.Channel.SendMessageAsync("That user has not linked their Discord account with their in-game account yet. Unable to remove from the zone.");
                    return;
                }

                var zone = ZoneManager.Zones[zoneID];

                var zoneMember = zone.Members.Where(x => x.CharacterID == characterID).FirstOrDefault();

                if (zoneMember == null)
                {
                    await Context.Channel.SendMessageAsync("That user is not part of this zone.");
                    return;
                }

                zone.Members.Remove(zoneMember);
                ZoneManager.SaveZone(zoneID);

                await ZoneSupport.SyncUsersWithZoneRole(Context, zoneID);

                await Context.Channel.SendMessageAsync($"User removed from `{zone.Name}`!");
            }
        }
    }
}
