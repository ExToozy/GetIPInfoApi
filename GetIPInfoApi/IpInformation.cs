using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GetIPInfoApi
{
    public class IPInformation
    {
        public int? Id { get; set; }
        [JsonPropertyName("ip")]
        public string? Ip { get; set; }
        [JsonPropertyName("bogon")]
        public bool Bogon { get; set; }
        [JsonPropertyName("hostname")]
        public string? Hostname { get; set; }
        [JsonPropertyName("city")]
        public string? City { get; set; }
        [JsonPropertyName("region")]
        public string? Region { get; set; }
        [JsonPropertyName("loc")]
        public string? Loc { get; set; }
        [JsonPropertyName("org")]
        public string? Org { get; set; }
        [JsonPropertyName("postal")]
        public string? Postal { get; set; }
        [JsonPropertyName("timezone")]
        public string? Timezone { get; set; }
        [JsonPropertyName("readme")]
        public string? Readme { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }


    }
}
