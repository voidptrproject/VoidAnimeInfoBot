using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using VoidAnimeInfoBot.Properties;
using VoidAnimeInfoBot.Shikimori;

namespace VoidAnimeInfoBot
{
    public class AnimeInfoModule : BaseCommandModule
    {
        public static async Task SendError(CommandContext ctx, string error)
        {
            await ctx.RespondAsync(new DiscordEmbedBuilder().WithTitle(Resources.Error).WithDescription(error).WithColor(DiscordColor.Rose));
        }

        public static async Task SendError(CommandContext ctx, DiscordMessage msg, string error)
        {
            await msg.ModifyAsync(new DiscordEmbedBuilder().WithTitle(Resources.Error).WithDescription(error)
                .WithColor(DiscordColor.Rose).Build());
        }

        [Command("ai")]
        [Description("Show info of anime")]
        public async Task AnimeInfoCommand(CommandContext ctx,
            [RemainingText, Description("Anime name")] string? animeName)
        {
            var message = await ctx.RespondAsync(new DiscordEmbedBuilder().WithTitle("Wait...").WithColor(DiscordColor.Azure));

            if (string.IsNullOrEmpty(animeName))
            {
                await SendError(ctx, message, Resources.AnimeNameIsReq);
                return;
            }

            List<Anime?> anims;

            try
            {
                anims = await AnimeInfo.FindAnimesByName(animeName, 1);
            }
            catch (Exception e)
            {
                await SendError(ctx, message, e.ToString());
                return;
            }

            if (anims.Count > 0)
                await message.ModifyAsync(await AnimeInfo.AnimeToEmbed(anims[0] ?? throw new InvalidOperationException()));
            else
                await SendError(ctx, message, Resources.NotFound);
        }

        [Command("aid")]
        [Description("Show info of anime by id")]
        public async Task AnimeInfoCommand(CommandContext ctx,
            [Description("Anime id")] int animeId)
        {
            var message = await ctx.RespondAsync(new DiscordEmbedBuilder().WithTitle("Wait...").WithColor(DiscordColor.Azure));

            if (animeId <= 0)
            {
                await SendError(ctx, message, Resources.AnimeIdIsReq);
                return;
            }

            Anime? anime;

            try
            {
                anime = await AnimeInfo.GetAnime(animeId);
            }
            catch (Exception e)
            {
                await SendError(ctx, message, e.ToString());
                return;
            }

            if (anime != null)
                await message.ModifyAsync(await AnimeInfo.AnimeToEmbed(anime));
            else
                await SendError(ctx, message, Resources.NotFound);
        }

        [Command("ais")]
        [Description("Show info of anime list")]
        public async Task AnimeInfoCommand(CommandContext ctx,
            [Description("Anime count to output")] int num,
            [RemainingText, Description("Anime name")] string? animeName)
        {
            var message = await ctx.RespondAsync(new DiscordEmbedBuilder().WithTitle("Wait...").WithColor(DiscordColor.Azure));
            if (message == null) return;

            if (string.IsNullOrEmpty(animeName))
            {
                await SendError(ctx, message, Resources.AnimeNameIsReq);
                return;
            }

            var anims = new List<Anime?>();

            try
            {
                anims = await AnimeInfo.FindAnimesByName(animeName, num);
            }
            catch (Exception e)
            {
                await SendError(ctx, message, e.ToString());
                return;
            }

            if (anims.Count > 0)
                await message.ModifyAsync(await AnimeInfo.AnimeListToEmbed(anims));
            else
                await SendError(ctx, message, Resources.NotFound);
        }

        [Command("eval"), RequireOwner]
        public async Task EvalCommand(CommandContext ctx, [RemainingText] string? cmd)
        {
            if (string.IsNullOrEmpty(cmd))
            {
                await ctx.RespondAsync("cmd is required");
                return;
            }

            object? result = null;

            try
            {
                result = await CSharpScript.EvaluateAsync(cmd);
            }
            catch (Exception e)
            {
                await SendError(ctx, e.ToString());
                return;
            }

            await ctx.RespondAsync(new DiscordEmbedBuilder().WithTitle("Execution Result").WithDescription($"{result}"));
        }
    }
}
