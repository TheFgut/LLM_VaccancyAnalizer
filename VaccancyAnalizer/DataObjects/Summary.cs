namespace VaccancyAnalizer
{
    public abstract class Summary
    {
        public Summary(List<JobVacancyAnalysis> vacancies)
        {
            GenerateSummary(vacancies);
        }
        protected abstract void GenerateSummary(List<JobVacancyAnalysis> vaccancies);
    }
}
