namespace VaccancyAnalizer
{
    public class ParsedTechologiesSummary : Summary
    {
        public Dictionary<string, int> tehnologiesUsage { get; private set; }
        public int skipped { get; private set; }
        public int failedToAnalize { get; private set; }
        public ParsedTechologiesSummary(List<JobVacancyAnalysis> vacancies) : base(vacancies)
        {
            tehnologiesUsage = new Dictionary<string, int>();
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
                string[] parts = vaccancy.analysis.Split('~');
                if (!parts[0].Contains("Required Technologies:") &&
                    !parts[0].Contains("Requirements:"))
                {
                    vaccancy.analysisStatus = AnalysisStatus.Failed;
                    failedToAnalize++;
                    continue;
                }
                parts[parts.Length - 1] = parts.Last().Replace("Finish", "");
                for (int i = 1; i < parts.Length - 1; i++)
                {
                    string technologyPart = parts[i];
                    technologyPart = technologyPart.Trim(' ', '\n');
                    if (tehnologiesUsage.ContainsKey(technologyPart))
                    {
                        tehnologiesUsage[technologyPart]++;
                    }
                    else tehnologiesUsage.Add(technologyPart, 1);
                }
            }
        }
    }
}
