using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VoidAnimeInfoBot.Shikimori
{
    partial class AnimesResult
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Russian { get; set; }

        public ImageStructure Image { get; set; }

        public string? Url { get; set; }

        public string? Kind { get; set; }

        public float? Score { get; set; }

        public string? Status { get; set; }

        public int? Episodes { get; set; }

        [JsonPropertyName("episodes_aired")]
        public int? EpisodesAired{ get; set; }

        [JsonPropertyName("aired_on")]
        public string? AiredOn { get; set; }

        [JsonPropertyName("released_on")]
        public string? ReleasedOn { get; set; }
    }
}
