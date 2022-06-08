using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VoidAnimeInfoBot.Shikimori
{
    public class Video
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        [JsonPropertyName("player_url")]
        public string? PlayerUrl { get; set; }

        public string? Kind { get; set; }
        public string? Hosting { get; set; }
    }
}
