using HtmlAgilityPack;
using VaccancyAnalizer.VacanciesRequesting.DataObjects;
using VaccancyAnalizer.Translation;
using System.Text.RegularExpressions;
using VaccancyAnalizer.VacanciesRequesting.HTML_Parsing;

namespace VaccancyAnalizer.VacanciesRequesting
{
    public class VacanciesDjinnyRequester : IVacanciesRequester
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private const float requestWaitTime = 5;

        public VacanciesDjinnyRequester()
        {
            httpClient.DefaultRequestHeaders.UserAgent.Clear();
            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT)");
        }

        public async Task<JobVacancy[]?> RecieveVacanies(string? searchQuery = null,
            int vacanciesLimit = -1, Action<float>? onProgressChange = null, 
            ILangugageTranslator? languageTranslator = null)
        {
            int pagesLimit = vacanciesLimit == -1 ? -1 : vacanciesLimit / 15 + 1;
            try
            {
                string link = $"https://djinni.co/jobs/";
                if (searchQuery != null)
                {
                    searchQuery = searchQuery.Replace(" ", "%20");
                    link = $"https://djinni.co/jobs/?all_keywords={searchQuery}";
                }

                var html = await httpClient.GetStringAsync(link);
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                int pagesCount = GetAvailableJobsPagesCount(doc);
                List<JobVacancy> awailableVacancies = new List<JobVacancy>();
                
                int pageNum = 0;
                while ((pageNum < pagesLimit || pagesLimit <= -1)
                    && pageNum <= pagesCount)
                {
                    JobVacancy[] vacancies = await RecieveVacaniesFromPage(pageNum,
                        vacanciesLimit == -1? 15 : vacanciesLimit - awailableVacancies.Count,
                        searchQuery, languageTranslator);
                    if(vacancies == null)
                    {
                        break;
                    }
                    awailableVacancies.AddRange(vacancies);
                    pageNum++;
                    onProgressChange?.Invoke((float)pageNum / (pagesCount < pagesLimit
                        || pagesLimit == -1 ? pagesCount : pagesLimit));
                    await Task.Delay(500);
                }
                return awailableVacancies.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"RecieveVacanies error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
}

        private async Task<JobVacancy[]> RecieveVacaniesFromPage(int page, int limit = 15,
            string? searchQuery = null, ILangugageTranslator? languageTranslator = null)
        {
            Random randomizer = new Random();
            try
            {
                string link = $"https://djinni.co/jobs/?page={page}";
                if (searchQuery != null)
                {
                    searchQuery = searchQuery.Replace(" ", "%20");
                    link = $"https://djinni.co/jobs/?all_keywords={searchQuery}&page={page}";
                }
                var html = await httpClient.GetStringAsync(link);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var ulNode = doc.DocumentNode.SelectSingleNode("//ul[@class='list-unstyled list-jobs mb-4']");

                if (ulNode == null)
                {
                    return null;
                }

                var liNodes = ulNode.SelectNodes("./li");
                if (liNodes == null || liNodes.Count == 0)
                {
                    return null;
                }

                JobVacancy[] jobVacancies = new JobVacancy[Math.Min(liNodes.Count, limit)];
                for (int i = 0; i < jobVacancies.Length; i++)
                {
                    var li = liNodes[i];
                    var jobTitleNode = li.SelectSingleNode(".//a[contains(@class,'job-item__title-link')]");
                    string? href = null;
                    string? jobTitle = null;
                    string? description = null;
                    if (jobTitleNode != null)
                    {
                        href = jobTitleNode.GetAttributeValue("href", "").Trim();
                        jobTitle = jobTitleNode.InnerText.Trim();
                        await Task.Delay((int)(requestWaitTime * ((float)randomizer.NextDouble() + 0.5f) * 1000));
                        description = await GetVacancyDescription(href);
                        description = DeepCleanText(description);
                        if (languageTranslator != null)
                        {
                            jobTitle = await languageTranslator.TranslateToEnglish(jobTitle);
                            description = await languageTranslator.TranslateToEnglish(description);
                        }
                    }

                    var companyNameNode = li.SelectSingleNode(".//a[contains(@data-analytics,'company_page')]");
                    string? companyName = null;
                    HtmlNodeCollection? vacancyStatsNodes = null;
                    if (companyNameNode != null)
                    {
                        companyName = companyNameNode.InnerText.Trim();
                        vacancyStatsNodes = companyNameNode.ParentNode.ParentNode
                        .SelectNodes(".//span[contains(@class,'text-nowrap')]");
                        if (languageTranslator != null)
                            companyName = await languageTranslator.TranslateToEnglish(companyName);
                    }
                    string? viewersCount = null;
                    string? appliersCount = null;
                    string? postTimeStatus = null;
                    if (vacancyStatsNodes != null && vacancyStatsNodes.Count >= 3)
                    {
                        viewersCount = vacancyStatsNodes[0].InnerText.Trim();
                        appliersCount = vacancyStatsNodes[1].InnerText.Trim();
                        postTimeStatus = vacancyStatsNodes[2].InnerText.Trim();
                    }

                    jobVacancies[i] = new JobVacancy()
                    {
                        pageLink = href,
                        jobTitle = jobTitle,
                        companyName = companyName,
                        viewersCount = viewersCount,
                        appliersCount = appliersCount,
                        postTimeStatus = postTimeStatus,
                        description = description
                    };
                }
                
                return jobVacancies;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private async Task<string?> GetVacancyDescription(string pageLink)
        {
            try
            {
                string link = $"https://djinni.co{pageLink}";
                var html = await httpClient.GetStringAsync(link);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                var descriptionNode = doc.DocumentNode
                    .SelectSingleNode("//div[@class='row']//div[@class='mb-4 job-post__description']");
                string? description = null;
                if (descriptionNode != null) description = descriptionNode.ToFormattedText();
                return description;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private int GetAvailableJobsPagesCount(HtmlAgilityPack.HtmlDocument page)
        {
            var ulNode = page.DocumentNode.SelectNodes("//ul[@class='pagination pagination_with_numbers']").Last();
            var liNodes = ulNode.SelectNodes("./li");
            var pageNum = liNodes[liNodes.Count - 2].SelectSingleNode(".//a[contains(@class,'page-link')]");
            string numText = pageNum.InnerText;
            return numText == "" ? 0 : int.Parse(numText);
        }

        private string DeepCleanText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return "";
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            text = text.Replace("\t", "    ");
            text = text.Replace('\u00A0', ' ');

            text = text
                .Replace("\u201C", "\"")
                .Replace("\u201D", "\"")
                .Replace("\u2018", "'")
                .Replace("\u2019", "'")
                .Replace("\u2013", "-")
                .Replace("\u2014", "-")
                .Replace("\u2026", "...");

            text = Regex.Replace(text, @"[^\p{L}\p{N}\p{P}\s]", "");
            text = Regex.Replace(text, @"[ ]{2,}", " ");
            text = Regex.Replace(text, @"\n{2,}", "\n");

            return text.Trim();
        }
    }
}
