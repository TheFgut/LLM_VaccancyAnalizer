using CryptoAI_Upgraded.DataSaving;
using System.Data;
using System.Text;
using VaccancyAnalizer.AnalisysSaving;
using VaccancyAnalizer.LLM_models;
using VaccancyAnalizer.VacanciesRequesting.DataObjects;

namespace VaccancyAnalizer.JobAnalyticsMaking
{
    public partial class AnalyticsMakerForm : Form
    {
        private SavableConfig savableConfig;
        private AnalyticsMaker analyticsMaker;
        private VacanciesData? vacanciesData;
        private IAnalysisSaver saver;
        private string promt;
        private AnalysisType analysisType;
        public AnalyticsMakerForm()
        {
            savableConfig = new SavableConfig(DataPaths.SavableConfigPath, "AppConfig");
            InitializeComponent();
            llM_Loader.OnLMChanged += (lm) =>
            {
                analyticsMaker = new AnalyticsMaker(lm);
                analyticsMaker.SetPromt(promt);
                CheckAwailabilityToStartAnalize();
            };

            //Llama-2-7b-instruct-tuning-Q5_K_M.gguf
            //Llama-3.2-3B-Instruct-UD-Q2_K_XL.gguf

            analysisType = savableConfig.GetEnumOrDefault("analysisType", AnalysisType.ParseTechnologies);
            analysisTypeSelector.DataSource = Enum.GetValues(typeof(AnalysisType));
            analysisTypeSelector.SelectedIndex = (int)analysisType;
            LoadSavedPromt();
            StartAnalizeBut.Enabled = false;
            vacanciesLoader1.SetConfig(savableConfig);
            vacanciesLoader1.onVacanciesLoaded += (v) =>
            {
                vacanciesData = v;
                CheckAwailabilityToStartAnalize();
            };

            saver = new WordFormatSaver(DataPaths.VacanciesDataPath);
        }

        private async void StartAnalizeBut_Click(object sender, EventArgs e)
        {
            StartAnalizeBut.Enabled = false;

            Analysis analysis = await analyticsMaker.ProcessVacanciesData(vacanciesData,
                (progress) => progressBar1.Value = (int)(progress * 100));

            progressBar1.Value = 0;
            StartAnalizeBut.Enabled = true;
            while (true)
            {
                try
                {
                    saver.SaveAnalysis(analysis);
                    break;
                }
                catch (IOException)
                {
                    DialogResult result = MessageBox.Show("Orerwriting analysis file is currently open.\n\n" +
                        "Please close file and press OK.", "Saving fail",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (result == DialogResult.Cancel) break;
                }
            }
            MessageBox.Show("Analysis completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //private void Save(List<JobVacancyAnalysis> vacancies)
        //{
        //    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        //    {
        //        NewLine = "\r\n",
        //        // Символ кавычки и символ экранирования — оба "
        //        Quote = '"',
        //        Escape = '"',
        //        // Режим экранирования по RFC 4180
        //        Mode = CsvMode.Escape
        //    };
        //    name = name.Replace('\n', '_');
        //    using (var writer = new StreamWriter($"{DataPaths.VacanciesDataPath}{name}.csv"))
        //    using (var csv = new CsvWriter(writer, config))
        //    {
        //        csv.WriteHeader<JobVacancyAnalysis>();
        //        csv.NextRecord();
        //        foreach (var vacancie in vacancies)
        //        {
        //            csv.WriteRecord(vacancie);
        //            csv.NextRecord();
        //        }
        //    }
        //}

        private void AnalyticsMakerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            savableConfig.Save();
        }

        private void PromtTextBox_Validated(object sender, EventArgs e) => SavePromt(PromtTextBox.Text);

        private void SavePromt(string promt)
        {
            this.promt = promt;
            savableConfig.SetString($"AnalysisPromt{analysisTypeSelector.SelectedItem.ToString()}", promt);
            analyticsMaker?.SetPromt(promt);
        }

        private void CheckAwailabilityToStartAnalize()
        {
            if (vacanciesData != null && analyticsMaker != null)
                StartAnalizeBut.Enabled = true;
        }

        private void analysisType_SelectedIndexChanged(object sender, EventArgs e)
        {
            analysisType = (AnalysisType)analysisTypeSelector.SelectedItem;
            savableConfig.SetEnum("analysisType", analysisType);
            LoadSavedPromt();
        }

        private void LoadSavedPromt()
        {
            promt = savableConfig.GetStringOrDefault($"AnalysisPromt{analysisType.ToString()}",
                Prompts.defaultGenVacansionsSummaryPromt);
            PromtTextBox.Text = promt;
        }
    }

    class AnalyticsMaker
    {
        private InstructLLM llm;
        private SavableConfig? config;

        public AnalyticsMaker(InstructLLM llm, SavableConfig? config = null)
        {
            this.llm = llm;
        }

        public async Task<Analysis> ProcessVacanciesData(VacanciesData data,
            Action<float>? onProgressChanged = null)
        {
            List<JobVacancyAnalysis> vacancies = data.vacancies.Select(v => v.MakeAnalysisObj()).ToList();
            int analizedVacanciesCount = 0;
            foreach (var vaccancy in vacancies)
            {
                StringBuilder result = new StringBuilder();
                //limit vacancy description string to context length
                AnalysisStatus status = await llm.Talk(vaccancy.description, (token) => result.Append(token));
                analizedVacanciesCount++;
                onProgressChanged?.Invoke((float)analizedVacanciesCount / vacancies.Count);
                vaccancy.analysis = result.ToString();
                vaccancy.analysisStatus = status;
                llm.ResetLLM();
            }
            return new Analysis(vacancies, data.query);
        }

        public void SetPromt(string? promt)
        {
            llm.SetLLMInstructions(new LLMInstructions(promt));
        }

        //public async Task TalkFittedInContext(string text, Action<string> onTokenAppears)
        //{
        //    int maxSubstringIterations = 5;
        //    while (maxSubstringIterations > 0)
        //    {
        //        int textTokenLen = llm.CalculateTokensCount(text);
        //        float fitCoef = textTokenLen / llm.ContextSize - llm.MaxAnswerLen - llm.InstructionsSize;
        //        if (fitCoef > 1)
        //        {
        //            text.Substring(0, (int)(text.Length / (fitCoef + 0.05f)));
        //        }
        //        else break;
        //        maxSubstringIterations--;
        //    }
        //    await llm.Talk(text, onTokenAppears);
        //}
    }
}
