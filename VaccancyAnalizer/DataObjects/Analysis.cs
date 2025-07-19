namespace VaccancyAnalizer
{
    public class Analysis
    {
        public List<JobVacancyAnalysis> vacancies {  get; private set; }
        public string query { get; private set; }
        public Summary summary { get; private set; }
        public Analysis(List<JobVacancyAnalysis> vacancies, string query)
        {
            this.vacancies = vacancies;
            this.query = query;
            this.summary = new ParsedTechologiesSummary(vacancies);
        }
    }
}
