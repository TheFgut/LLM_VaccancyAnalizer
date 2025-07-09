using VaccancyAnalizer.VacanciesRequesting.DataObjects;

namespace VaccancyAnalizer
{
    public class JobVacancyAnalysis : JobVacancy
    {
        public string? analysis {  get; set; }
        public AnalysisStatus? analysisStatus { get; set; }
    }

    public enum AnalysisStatus
    {
        Success,
        Failed,
        Skipped
    }
}
