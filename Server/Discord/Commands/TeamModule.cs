using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Discord.Commands
{
    [Group("team")]
    public class TeamModule : ModuleBase<SocketCommandContext>
    {
        List<string> teams = new List<string>()
        {
            // Global teams
            "Graphics",
            "Dev",
            "Moves",
            "Coding",
            "Dungeons",

            "Spoilers",

            // Subteams
            "Maps",
            "NPCs",
            "Stories",
            "Sprites",
            "Tiles",
            "Tester"
        };

        [Command("join")]
        [Summary("Join a development team.")]
        public async Task JoinAsync(string teamName)
        {
            foreach (var team in teams)
            {
                if (team.ToLower() == teamName.ToLower())
                {
                    var role = Context.Guild.Roles.Where(x => x.Name == team).FirstOrDefault();

                    if (role != null)
                    {
                        await (Context.User as IGuildUser).AddRoleAsync(role);

                        await Context.Channel.SendMessageAsync("You have been added to the requested team.");
                    }
                }
            }
        }

        [Command("leave")]
        [Summary("Leave a development team.")]
        public async Task LeaveAsync(string teamName)
        {
            foreach (var team in teams)
            {
                if (team.ToLower() == teamName.ToLower())
                {
                    var role = Context.Guild.Roles.Where(x => x.Name == team).FirstOrDefault();

                    if (role != null)
                    {
                        await (Context.User as IGuildUser).RemoveRoleAsync(role);

                        await Context.Channel.SendMessageAsync("You have been removed from the requested team.");
                    }
                }
            }
        }

        [Command("list")]
        [Summary("List all of the available teams.")]
        public async Task ListAsync()
        {
            var responseBuilder = new StringBuilder();

            responseBuilder.AppendLine("**Available Teams:**");
            responseBuilder.AppendLine();
            foreach (var team in teams)
            {
                responseBuilder.AppendLine(team);
            }

            await Context.Channel.SendMessageAsync(responseBuilder.ToString());
        }
    }
}
