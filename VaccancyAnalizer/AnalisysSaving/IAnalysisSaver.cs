
namespace VaccancyAnalizer.AnalisysSaving
{
    internal interface IAnalysisSaver
    {
        /// <summary>
        /// saves analysis
        /// </summary>
        /// <param name="analysis"></param>
        /// <param name="overwrite"></param>
        /// <returns>save path</returns>
        public string SaveAnalysis(Analysis analysis, bool overwrite = true);
    }
}
