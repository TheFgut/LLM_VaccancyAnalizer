using System.Text;
using System.Text.Json;

namespace VaccancyAnalizer.Translation
{
    public class LibreTranslatorRequestService : ILangugageTranslator
    {
        private string nativeLanguage;
        private string LLMLanguage;
        private string url;
        public LibreTranslatorRequestService(int port, Language nativeLanguage)
        {
            url = $"http://localhost:{port}/translate";
            LLMLanguage = getLangStr(Language.English);
            this.nativeLanguage = getLangStr(nativeLanguage);
        }

        public async Task<string> TranslateFromEnglish(string text)
        {
            string? translation = await SendTranslationRequest(text,
                LLMLanguage, nativeLanguage);
            return translation == null ? "Translation error" : translation;
        }

        public async Task<string> TranslateToEnglish(string text)
        {
            string? translation = await SendTranslationRequest(text,
                "auto", LLMLanguage);
            return translation == null ? "Translation error" : translation;
        }

        private async Task<string?> SendTranslationRequest(string text,
            string srcLang, string targetLang)
        {
            var requestData = new TranslateRequest
            {
                Q = text,
                Source = srcLang,
                Target = targetLang,
                Format = "text"
            };
            var json = JsonSerializer.Serialize(requestData);
            using var httpClient = new HttpClient();
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Sending POST-request
            HttpResponseMessage response = null;
            try
            {
                response = await httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                return null;
            }

            // Reading response
            var responseJson = await response.Content.ReadAsStringAsync();
            TranslateResponse? result;
            try
            {
                result = JsonSerializer.Deserialize<TranslateResponse>(responseJson);
            }
            catch (JsonException ex)
            {
                result = null;
            }
            return result == null ? null : result.TranslatedText;
        }

        public string getLangStr(Language language)
        {
            switch (language)
            {
                case Language.Auto : return "auto";
                case Language.English: return "en";
                case Language.Russian: return "ru";
                default: 
                    throw new NotImplementedException($"Language \"{language}\" is not supported");
            }
        }
    }
}
