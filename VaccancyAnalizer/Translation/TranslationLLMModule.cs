using VaccancyAnalizer.LLM_models;
using System.Text;

namespace VaccancyAnalizer.Translation
{
    public class TranslationLLMModule : ILangugageTranslator
    {
        private Dictionary<Language, TranslationLLM> translationNetworks;
        private TranslationLLM originTranslator;
        private Language originLanguage;
        public TranslationLLMModule(Dictionary<Language, TranslationLLM> translationNetworks,
            TranslationLLM originTranslator, Language originLanguage)
        {
            this.translationNetworks = translationNetworks;
            this.originTranslator = originTranslator;
            this.originLanguage = originLanguage;
        }

        public async Task<string> TranslateToEnglish(string text)
        {
            Language languageOfText = detectLanguage(text);
            if (languageOfText == Language.Unknown) throw new Exception("TranslationModule.TranslateToEnglish failed." +
                " Text language is unknown");
            else if(languageOfText == Language.English) return text;//do not translating from english to english
            TranslationLLM? translator;
            if(!translationNetworks.TryGetValue(languageOfText, out translator)) throw new Exception("TranslationModule.TranslateToEnglish failed." +
                $" Translator for language {languageOfText} not found");
            StringBuilder translation = new StringBuilder();
            await translator.Translate(text, (token) => translation.Append(token));

            string translationString = translation.ToString();
            return translationString;
        }

        public async Task TestTranslateToEnglish(string text, Action<string> onTokenRecieved)
        {
            Language languageOfText = detectLanguage(text);
            if (languageOfText == Language.Unknown) throw new Exception("TranslationModule.TranslateToEnglish failed." +
                " Text language is unknown");
            else if (languageOfText == Language.English)//do not translating from english to english
            {
                onTokenRecieved.Invoke(text);
                return;
            }
            TranslationLLM? translator;
            if (!translationNetworks.TryGetValue(languageOfText, out translator)) throw new Exception("TranslationModule.TranslateToEnglish failed." +
                $" Translator for language {languageOfText} not found");
            await translator.Translate(text, (token) => onTokenRecieved.Invoke(token));
        }

        public async Task<string> TranslateFromEnglish(string text)
        {
            StringBuilder translation = new StringBuilder();
            await originTranslator.Translate(text, (token) => translation.Append(token));
            string translationString = translation.ToString().Split(":")[1];
            return translationString;
        }

        private Language detectLanguage(string text)
        {
            double percentTreshold = 0.6;
            string checkString = Truncate(text, 100);

            if (text.Select(c => c >= '\u0400' && c <= '\u04FF' ? 1 : 0).Average() > percentTreshold)
                return Language.Russian;
            if (text.Select(c => c >= 'A' && c <= 'Z' || c >= 'a' && c <= 'z' ? 1 : 0).Average() > percentTreshold)
                return Language.English;
            return Language.Unknown;
        }

        private string Truncate(string s, int n)
        {
            return s.Length <= n ? s : s.Substring(0, n);
        }
    }
}
