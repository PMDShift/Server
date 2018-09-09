using Discord.Commands;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Discord.Commands
{
    [Group("my")]
    public class MyModule : ModuleBase<SocketCommandContext>
    {
        [Command("team")]
        [Summary("Displays your current ingame team.")]
        public async Task TeamAsync() {
            var client = ClientManager.FindClient(Context.User.Id);

            if (client == null) {
                await Context.Channel.SendMessageAsync("Unable to display team data. Make sure you are online and your account is connected.");
                return;
            }

            var infoBuilder = new StringBuilder();
            infoBuilder.AppendLine("Current Team:");
            infoBuilder.AppendLine();

            for (var i = 0; i < client.Player.Team.Length; i++) {
                var recruit = client.Player.Team[i];

                if (recruit.Loaded) {
                    var pokemon = Pokedex.Pokedex.GetPokemon(recruit.Species);

                    infoBuilder.AppendLine($"Slot {i + 1}: {pokemon.Name}, Level {recruit.Level}");
                } else {
                    infoBuilder.AppendLine($"Slot {i + 1}: Empty");
                }
            }

            await Context.Channel.SendMessageAsync(infoBuilder.ToString());
        }
    }
}
