namespace VaccancyAnalizer
{
    public class CustomSummary : Summary
    {
        public int skipped { get; private set; }
        public CustomSummary(List<JobVacancyAnalysis> vacancies) : base(vacancies)
        {
        }

        protected override void GenerateSummary(List<JobVacancyAnalysis> vaccancies)
        {
            foreach (JobVacancyAnalysis vaccancy in vaccancies)
            {
                if (vaccancy.analysisStatus == AnalysisStatus.Skipped)
                {
                    skipped++;
                    continue;
                }
            }
        }
    }
}
