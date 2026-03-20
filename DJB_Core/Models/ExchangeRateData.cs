using System.Text.Json.Serialization;

namespace DJB_Core.Models
{
    public class ExchangeRateData
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("setup")]
        public string Setup { get; set; }

        [JsonPropertyName("punchline")]
        public string Punchline { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}