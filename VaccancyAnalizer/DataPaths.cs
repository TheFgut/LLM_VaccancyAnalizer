
namespace VaccancyAnalizer
{
    public static class DataPaths
    {
        public static string VacanciesDataPath => $"{Application.LocalUserAppDataPath}\\vacancies\\";
        public static string ReportsDataPath => $"{Application.LocalUserAppDataPath}\\reports\\";
        public static string SavableConfigPath => $"{Application.LocalUserAppDataPath}\\";
    }
}
