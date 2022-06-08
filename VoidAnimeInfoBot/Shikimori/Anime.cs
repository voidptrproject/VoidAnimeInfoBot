using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VoidAnimeInfoBot.Shikimori
{
    public class Anime
    {
        public struct RateStats
        {
            public int Rate { get; set; }
            public int Value { get; set; }
        }
        public struct ScreenShot
        {
            public string? Original { get; set; }
            public string? Preview { get; set; }
        }

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Russian { get; set; }

        public ImageStructure Image { get; set; }

        public string? Url { get; set; }

        public string? Kind { get; set; }

        public float? Score { get; set; }

        public string? Status { get; set; }

        public int? Episodes { get; set; }

        [JsonProperty(PropertyName = ("episodes_aired"))]
        public int? EpisodesAired { get; set; }

        [JsonProperty(PropertyName = ("aired_on"))]
        public string? AiredOn { get; set; }

        [JsonProperty(PropertyName = ("released_on"))]
        public string? ReleasedOn { get; set; }
        public string? Rating { get; set; }
        public List<string?>? English { get; set; }
        public List<string?>? Japanese { get; set; }
        public List<string?>? Synonyms{ get; set; }

        [JsonProperty(PropertyName = ("license_name_ru"))]
        public string? LicenseNameRu { get; set; }
        public int Duration { get; set; }
        public string? Description { get; set; }

        [JsonProperty(PropertyName = ("description_html"))]
        public string? DescriptionHtml { get; set; }

        [JsonProperty(PropertyName = ("description_source"))]
        public string? DescriptionSource{ get; set; }
        public string? Franchise{ get; set; }
        public bool Favoured { get; set; }
        public bool Anons { get; set; }
        public bool Ongoing { get; set; }

        [JsonProperty(PropertyName = ("thread_id"))]
        public int ThreadId { get; set; }

        [JsonProperty(PropertyName = ("topic_id"))]
        public int TopicId { get; set; }

        [JsonProperty(PropertyName = ("myanimelist_id"))]
        public int MyAnimeListId { get; set; }

        [JsonProperty(PropertyName = ("rates_scores_stats"))]
        public List<RateStats?>? RateScores { get; set; }

        [JsonProperty(PropertyName = ("rates_statuses_stats"))]
        public List<RateStats?>? RateStatusesStats { get; set; }

        [JsonProperty(PropertyName = ("updated_at"))]
        public string? UpdatedAt { get; set; }

        [JsonProperty(PropertyName = ("next_episode_at"))]
        public string? NextEpisodeAt { get; set; }

        public List<string?>? Fansubbers { get; set; }

        public List<string?>? Fandubbers { get; set; }

        public List<string?>? Licensors { get; set; }

        public List<Genre?>? Genres { get; set; }

        public List<Studio?>? Studios { get; set; }

        public List<Video?>? Videos { get; set; }

        public List<ScreenShot?>? Screenshots { get; set; }

        public JsonObject? UserRate { get; set; }
    }
}
