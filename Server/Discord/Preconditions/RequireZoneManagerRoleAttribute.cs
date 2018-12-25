using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Server.Discord.Preconditions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class RequireZoneManagerRoleAttribute : PreconditionAttribute
    {
        public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
        {
            var user = (SocketGuildUser)context.User;

            var hasRole = user.Roles.Where(x => x.Name == "Zone Manager").Any();

            if (hasRole)
            {
                return Task.FromResult(PreconditionResult.FromSuccess());
            } else
            {
                return Task.FromResult(PreconditionResult.FromError("You must be a zone manager to use this command."));
            }
        }
    }
}
