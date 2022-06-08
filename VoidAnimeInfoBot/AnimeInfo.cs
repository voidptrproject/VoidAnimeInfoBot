using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using Newtonsoft.Json;
using VoidAnimeInfoBot.Properties;
using VoidAnimeInfoBot.Shikimori;

namespace VoidAnimeInfoBot
{
    public class AnimeInfo
    {
        private static readonly Uri ShikimoriApiUri = new Uri("https://shikimori.one/api/");
        private static readonly Uri ShikimoriUri = new Uri("https://shikimori.one/");
        private static readonly HttpClient Web = new() { Timeout = TimeSpan.FromSeconds(5) };

        public static async Task<Anime?> GetAnime(int id)
        {
            var animeResponse = await Web.GetAsync(ShikimoriApiUri + $"animes/{id}");
            if (!animeResponse.IsSuccessStatusCode)
                throw new HttpRequestException($"Cannot get valid response for anime {id} (Code: {animeResponse.StatusCode})");
            return JsonConvert.DeserializeObject<Anime>(await animeResponse.Content.ReadAsStringAsync());
        }

        public static async Task<List<Anime?>> FindAnimesByName(string name, int numToFind = 10)
        {
            var animes = new List<Anime?>();

            var response = await Web.GetAsync(ShikimoriApiUri + "animes" + $"?search={name}&limit={numToFind}");
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Cannot get valid response");

            var result = JsonConvert.DeserializeObject<List<AnimesResult>>(await response.Content.ReadAsStringAsync());
            Debug.Assert(result != null, nameof(result) + " != null");
            foreach (var res in result)
            {
                animes.Add(await GetAnime(res.Id));
                await Task.Delay(200);
            }

            return animes;
        }

        public static async Task<DiscordEmbed> AnimeListToEmbed(List<Anime?> animes)
        {
            var builder = new DiscordEmbedBuilder();

            builder.WithColor(DiscordColor.Cyan);
            builder.WithTitle(Resources.SearchResults);

            var stringBuilder = new StringBuilder();
            animes.ForEach(anime =>
            {
                var name = string.IsNullOrEmpty(anime?.Russian) ? anime?.Russian : anime.Name;
                stringBuilder.AppendLine($"<{anime!.Id}> - [{name}]({ShikimoriUri + anime!.Url})");
            });

            builder.WithDescription(stringBuilder.ToString());

            return builder.Build();
        }

        public static async Task<DiscordEmbed> AnimeToEmbed(Anime anime)
        {
            var builder = new DiscordEmbedBuilder();

            builder.WithColor(DiscordColor.Cyan);
            builder.WithTitle(!string.IsNullOrEmpty(anime?.Russian) ? anime?.Russian : anime.Name);

            if (!string.IsNullOrEmpty(anime?.Image.Preview))
                builder.WithThumbnail(ShikimoriUri + anime.Image.Preview);

            var descriptionBuilder = new StringBuilder();
            descriptionBuilder.AppendLine($"[{ShikimoriUri + anime?.Url}]({ShikimoriUri + anime?.Url})");

            if (!string.IsNullOrEmpty(anime?.DescriptionHtml))
                descriptionBuilder.Append(new ReverseMarkdown.Converter().Convert(anime?.DescriptionHtml));
            else if (!string.IsNullOrEmpty(anime?.Description))
                descriptionBuilder.Append(anime?.Description);

            builder.WithDescription(descriptionBuilder.ToString());

            builder.AddField(Resources.Score, anime?.Score + "/10", true);
            builder.AddField(Resources.Status, anime?.Status, true);
            if (anime?.Genres != null && anime?.Genres.Count != 0)
            {
                var genres = new List<string?>();
                anime?.Genres.ForEach(g => genres.Add(string.IsNullOrEmpty(g?.Russian) ? g?.Name : g?.Russian));
                builder.AddField(Resources.Genres, string.Join(", ", genres), true);
            }
            
            return builder.Build();
        }
    }
}
