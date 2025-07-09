namespace VaccancyAnalizer.VacanciesRequesting.DataObjects
{
    public class JobVacancy
    {
        public string? pageLink { get; set; }
        public string? companyName { get; set; }
        public string? jobTitle { get; set; }
        public string? viewersCount { get; set; }
        public string? appliersCount { get; set; }
        public string? postTimeStatus { get; set; }
        public string? description { get; set; }
        public override string ToString()
        {
            return $"Company:{companyName}\nJob title:{jobTitle}\nLink:{pageLink}\n" +
                $"Viewers:{viewersCount}\nAppliers:{appliersCount}\nPostTimeStatus:{postTimeStatus}\n" +
                $"Description:{description}";
        }

        public JobVacancy Clone()
        {
            return new JobVacancy()
            {
                pageLink = pageLink,
                companyName = companyName,
                jobTitle = jobTitle,
                viewersCount = viewersCount,
                appliersCount = appliersCount,
                postTimeStatus = postTimeStatus,
                description = description
            };
        }

        public JobVacancyAnalysis MakeAnalysisObj()
        {
            return new JobVacancyAnalysis()
            {
                pageLink = pageLink,
                companyName = companyName,
                jobTitle = jobTitle,
                viewersCount = viewersCount,
                appliersCount = appliersCount,
                postTimeStatus = postTimeStatus,
                description = description
            };
        }
    }
}
