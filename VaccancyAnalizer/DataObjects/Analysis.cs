namespace VaccancyAnalizer
{
    public class Analysis
    {
        public List<JobVacancyAnalysis> vacancies {  get; private set; }
        public string query { get; private set; }
        public Summary summary { get; private set; }
        public Analysis(List<JobVacancyAnalysis> vacancies, string query, 
            AnalysisType analysisType)
        {
            this.vacancies = vacancies;
            this.query = query;
            switch (analysisType)
            {
                case AnalysisType.ParseTechnologies:
                    this.summary = new ParsedTechologiesSummary(vacancies);
                    break;
                case AnalysisType.Custom:
                    this.summary = new CustomSummary(vacancies);
                    break;
            }
        }
    }
}
