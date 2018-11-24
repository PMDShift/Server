using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Server.Discord.Commands
{
    [Group("content")]
    public class ContentModule : ModuleBase<SocketCommandContext>
    {
        [Group("details")]
        public class DetailsModule : ModuleBase<SocketCommandContext>
        {
            [Command("move")]
            public async Task MoveAsync(int id)
            {
                if (id < 1 || id > Moves.MoveManager.Moves.MaxMoves)
                {
                    await Context.Channel.SendMessageAsync("Invalid move number.");
                    return;
                }

                var move = Moves.MoveManager.Moves[id];

                var detailsBuilder = new StringBuilder();
                detailsBuilder.AppendLine($"**Move Details (#{id})**");
                detailsBuilder.AppendLine($"Name: `{move.Name}`");
                detailsBuilder.AppendLine($"Sanboxed: {move.IsSandboxed}");
                detailsBuilder.AppendLine($"Under Review: {move.IsBeingReviewed}");
                detailsBuilder.AppendLine($"Max PP: {move.MaxPP}");
                detailsBuilder.AppendLine($"Effect Type: {move.EffectType}");
                detailsBuilder.AppendLine($"Element: {move.Element}");
                detailsBuilder.AppendLine($"Move Category: {move.MoveCategory}");
                detailsBuilder.AppendLine($"Target Type: {move.TargetType}");
                detailsBuilder.AppendLine($"Range Type: {move.MoveCategory}");
                detailsBuilder.AppendLine($"Range: {move.Range}");
                detailsBuilder.AppendLine($"Accuracy: {move.Range}");

                await Context.Channel.SendMessageAsync(detailsBuilder.ToString());
            }
        }

        [Group("edit")]
        public class EditModule : ModuleBase<SocketCommandContext>
        {
            [RequireOwner]
            [Command("move")]
            public async Task MoveAsync(int id)
            {
                if (id < 1 || id > Moves.MoveManager.Moves.MaxMoves)
                {
                    await Context.Channel.SendMessageAsync("Invalid move number.");
                    return;
                }

                Moves.MoveManager.Moves[id].IsSandboxed = true;
                Moves.MoveManager.SaveMove(id);

                await Context.Channel.SendMessageAsync($"\"{Moves.MoveManager.Moves[id].Name}\" (#{id}) has been sandboxed!");
            }

            [RequireOwner]
            [Command("item")]
            public async Task ItemAsync(int id)
            {
                if (id < 1 || id > Items.ItemManager.Items.MaxItems)
                {
                    await Context.Channel.SendMessageAsync("Invalid item number.");
                    return;
                }

                var content = Items.ItemManager.Items[id];

                //content.IsBeingReviewed = false;
                content.IsSandboxed = true;
                Items.ItemManager.SaveItem(id);

                await Context.Channel.SendMessageAsync($"\"{content.Name}\" (#{id}) has been sandboxed!");
            }
        }

        [Group("review")]
        public class ReviewModule : ModuleBase<SocketCommandContext>
        {
            [RequireOwner]
            [Command("move")]
            public async Task MoveAsync(int id)
            {
                if (id < 1 || id > Moves.MoveManager.Moves.MaxMoves)
                {
                    await Context.Channel.SendMessageAsync("Invalid move number.");
                    return;
                }

                if (!Moves.MoveManager.Moves[id].IsSandboxed)
                {
                    await Context.Channel.SendMessageAsync("This move is not currently being edited.");
                    return;
                }

                Moves.MoveManager.Moves[id].IsBeingReviewed = true;

                await Context.Channel.SendMessageAsync($"\"{Moves.MoveManager.Moves[id].Name}\" (#{id}) is now being reviewed.");
            }
        }

        [Group("approve")]
        public class ApproveModule : ModuleBase<SocketCommandContext>
        {
            [RequireOwner]
            [Command("move")]
            public async Task MoveAsync(int id)
            {
                if (id < 1 || id > Moves.MoveManager.Moves.MaxMoves)
                {
                    await Context.Channel.SendMessageAsync("Invalid move number.");
                    return;
                }

                if (!Moves.MoveManager.Moves[id].IsSandboxed)
                {
                    await Context.Channel.SendMessageAsync("This move is not currently being edited.");
                    return;
                }

                Moves.MoveManager.Moves[id].IsBeingReviewed = false;
                Moves.MoveManager.Moves[id].IsSandboxed = false;
                Moves.MoveManager.SaveMove(id);

                await Context.Channel.SendMessageAsync($"The changes to \"{Moves.MoveManager.Moves[id].Name}\" (#{id}) have been approved.");
            }

            [RequireOwner]
            [Command("item")]
            public async Task ItemAsync(int id)
            {
                if (id < 1 || id > Items.ItemManager.Items.MaxItems)
                {
                    await Context.Channel.SendMessageAsync("Invalid item number.");
                    return;
                }

                var content = Items.ItemManager.Items[id];

                if (!content.IsSandboxed)
                {
                    await Context.Channel.SendMessageAsync("This item is not currently being edited.");
                    return;
                }

                //content.IsBeingReviewed = false;
                content.IsSandboxed = false;
                Items.ItemManager.SaveItem(id);

                await Context.Channel.SendMessageAsync($"The changes to \"{content.Name}\" (#{id}) have been approved.");
            }
        }

        [Group("refuse")]
        public class RefuseModule : ModuleBase<SocketCommandContext>
        {
            [RequireOwner]
            [Command("move")]
            public async Task MoveAsync(int id)
            {
                if (id < 1 || id > Moves.MoveManager.Moves.MaxMoves)
                {
                    await Context.Channel.SendMessageAsync("Invalid move number.");
                    return;
                }

                if (!Moves.MoveManager.Moves[id].IsSandboxed)
                {
                    await Context.Channel.SendMessageAsync("This move is not currently being edited.");
                    return;
                }

                Moves.MoveManager.Moves[id].IsBeingReviewed = false;

                await Context.Channel.SendMessageAsync($"The changes to \"{Moves.MoveManager.Moves[id].Name}\" (#{id}) have been refused.");
            }

            [RequireOwner]
            [Command("item")]
            public async Task ItemAsync(int id)
            {
                if (id < 1 || id > Items.ItemManager.Items.MaxItems)
                {
                    await Context.Channel.SendMessageAsync("Invalid item number.");
                    return;
                }

                var content = Items.ItemManager.Items[id];

                if (!content.IsSandboxed)
                {
                    await Context.Channel.SendMessageAsync("This item is not currently being edited.");
                    return;
                }

                //content.IsBeingReviewed = false;

                await Context.Channel.SendMessageAsync($"The changes to \"{content.Name}\" (#{id}) have been refused.");
            }
        }
    }
}
