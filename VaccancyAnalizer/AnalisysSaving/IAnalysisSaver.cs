
namespace VaccancyAnalizer.AnalisysSaving
{
    internal interface IAnalysisSaver
    {
        public void SaveAnalysis(Analysis analysis, bool overwrite = true);
    }
}
