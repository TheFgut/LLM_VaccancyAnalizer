using VaccancyAnalizer.VacanciesRequesting.DataObjects;
using VaccancyAnalizer.Translation;

namespace VaccancyAnalizer.VacanciesRequesting
{
    public interface IVacanciesRequester
    {
        public Task<JobVacancy[]?> RecieveVacanies(string? searchQuery = null,
            int vacanciesLimit = -1, Action<float>? onProgressChange = null,
            ILangugageTranslator? languageTranslator = null);
    }
}
