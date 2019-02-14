using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Discord
{
    public class DiscordManager
    {
        static DiscordManager instance;
        public static DiscordManager Instance
        {
            get {
                if (instance == null)
                {
                    instance = new DiscordManager();
                }

                return instance;
            }
        }

        CommandService commands;
        public DiscordSocketClient Client { get; private set; }
        IServiceProvider services;

        public void Run(string token)
        {
            RunAsync(token).GetAwaiter().GetResult();
        }

        public async Task RunAsync(string token)
        {
            Client = new DiscordSocketClient();
            commands = new CommandService();

            services = new ServiceCollection().AddSingleton(Client)
                                               .AddSingleton(commands)
                                               .BuildServiceProvider();

            Client.MessageReceived += HandleCommandAsync;
            await commands.AddModulesAsync(typeof(DiscordManager).Assembly);

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private Task HandleCommandAsync(SocketMessage messageParam)
        {
            // NOTE: Run on a background thread to prevent blocking the main Discord event loop
            Task.Run(async () =>
            {
                // Don't process the command if it was a System Message
                var message = messageParam as SocketUserMessage;
                if (message == null) return;
                // Create a number to track where the prefix ends and the command begins
                int argPos = 0;
                // Determine if the message is a command, based on if it starts with '!' or a mention prefix
                if (!(message.HasCharPrefix('/', ref argPos) || message.HasMentionPrefix(Client.CurrentUser, ref argPos))) return;
                // Create a Command Context
                var context = new SocketCommandContext(Client, message);
                // Execute the command. (result does not indicate a return value, 
                // rather an object stating if the command executed successfully)
                var result = await commands.ExecuteAsync(context, argPos, services);
                if (!result.IsSuccess)
                {
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                }
            });

            return Task.CompletedTask;
        }
    }
}
