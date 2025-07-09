using System.Text.Json.Serialization;

namespace VaccancyAnalizer.Translation
{
    public class TranslateRequest
    {
        [JsonPropertyName("q")]
        public string Q { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        [JsonPropertyName("target")]
        public string Target { get; set; }

        [JsonPropertyName("format")]
        public string Format { get; set; } = "text";
    }
}
