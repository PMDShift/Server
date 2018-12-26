using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Server.Discord
{
    public static class SocketMessageChannelExtensions
    {
        public static async Task SendSplitMessageAsync(this ISocketMessageChannel channel, string message)
        {
            foreach (var chunk in ChunksUpto(message, 50))
            {
                await channel.SendMessageAsync(chunk);
            }
        }

        private static IEnumerable<string> ChunksUpto(string str, int maxChunkSize)
        {
            var lines = str.Split(Environment.NewLine);

            var responseBuilder = new StringBuilder();

            var currentChunkSize = 0;
            foreach (var line in lines)
            {
                if (currentChunkSize + line.Length > maxChunkSize)
                {
                    yield return responseBuilder.ToString();
                    responseBuilder.Clear();
                }

                responseBuilder.AppendLine(line);

                currentChunkSize += line.Length;
            }

            var leftoverResponse = responseBuilder.ToString();

            if (!string.IsNullOrEmpty(leftoverResponse) && leftoverResponse != Environment.NewLine)
            {
                yield return responseBuilder.ToString();
            }
        }
    }
}
