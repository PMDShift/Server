using Discord.Commands;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Discord.Commands
{
    [Group("account")]
    public class AccountModule : ModuleBase<SocketCommandContext>
    {
        [Command("connect")]
        [Summary("Connect your Discord account to your ingame account.")]
        [RequireContext(ContextType.DM)]
        public async Task ConnectAsync([Remainder] string characterName)
        {
            var client = ClientManager.FindClient(characterName);

            if (client == null)
            {
                await Context.Channel.SendMessageAsync("That player is not online.");
                return;
            }

            if (!client.Player.PlayerData.CanLinkDiscord)
            {
                await Context.Channel.SendMessageAsync("Unable to connect to ingame account.");
                return;
            }

            if (client.Player.PendingDiscordId > 0)
            {
                await Context.Channel.SendMessageAsync("An active connect is pending. Try again later, or reset with \"/resetdiscord\" ingame.");
                return;
            }

            client.Player.PendingDiscordId = Context.User.Id;
            await Context.Channel.SendMessageAsync("Please confirm ingame.");
            Messenger.AskQuestion(client, "LinkDiscord", $"Discord user \"{Context.User.Username}#{Context.User.Discriminator}\" would like to connect with your account. Allow?", -1);
        }
    }
}
