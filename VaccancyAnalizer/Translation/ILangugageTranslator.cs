namespace VaccancyAnalizer.Translation
{
    public interface ILangugageTranslator
    {
        public Task<string> TranslateToEnglish(string text);
        public Task<string> TranslateFromEnglish(string text);
    }
}
