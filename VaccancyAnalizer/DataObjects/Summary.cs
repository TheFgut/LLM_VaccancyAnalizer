namespace VaccancyAnalizer
{
    public class Summary
    {
        public Dictionary<string, int> tehnologiesUsage { get; private set; }
        public int skipped {  get; private set; }
        public int failedToAnalize { get; private set; }
        public Summary()
        {
            tehnologiesUsage = new Dictionary<string, int>();
        }

        internal void AnalizeVaccancy(JobVacancyAnalysis vaccancy)
        {
            if (vaccancy.analysisStatus == AnalysisStatus.Skipped)
            {
                skipped++;
                return;
            }
            string[] parts = vaccancy.analysis.Split('~');
            if (!parts[0].Contains("Required Technologies:") &&
                !parts[0].Contains("Requirements:"))
            {
                vaccancy.analysisStatus = AnalysisStatus.Failed;
                failedToAnalize++;
                return;
            }
            parts[parts.Length - 1] = parts.Last().Replace("Finish","");
            for (int i = 1; i < parts.Length-1; i++)
            {
                string technologyPart = parts[i];
                technologyPart = technologyPart.Trim(' ','\n');
                if (tehnologiesUsage.ContainsKey(technologyPart))
                {
                    tehnologiesUsage[technologyPart]++;
                }
                else tehnologiesUsage.Add(technologyPart, 1);
            }
        }
    }
}
