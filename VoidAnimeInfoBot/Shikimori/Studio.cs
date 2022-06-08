using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VoidAnimeInfoBot.Shikimori
{
    public class Studio
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        [JsonPropertyName("filtered_name")]
        public string? FiltredName { get; set; }
        public bool Real { get; set; }
        public string? Image { get; set; }
    }
}
