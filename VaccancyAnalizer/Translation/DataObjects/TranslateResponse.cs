using System.Text.Json.Serialization;

namespace VaccancyAnalizer.Translation
{
    public class TranslateResponse
    {
        [JsonPropertyName("translatedText")]
        public string TranslatedText { get; set; }
    }
}
