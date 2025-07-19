using CryptoAI_Upgraded.DataSaving;
using VaccancyAnalizer.VacanciesRequesting;
using VaccancyAnalizer.VacanciesRequesting.DataObjects;
using VaccancyAnalizer.Translation;
using VaccancyAnalizer.Utils;

namespace VaccancyAnalizer.JobAnalyticsMaking
{
    public partial class VacanciesNetLoader : Form
    {
        private IVacanciesRequester parser;
        private int vacanciesLimit;
        public Action onWindowUnlocks;
        public Action onWindowLocks;
        public Action<VacanciesData> onDataLoaded;
        private SavableConfig? config;
        public VacanciesNetLoader(IVacanciesRequester parser, SaveManager<VacanciesData>? saveManager = null,
            SavableConfig? config = null)
        {
            this.parser = parser;
            this.config = config;
            InitializeComponent();
            if (config != null)
            {
                TranslateToEnglishCheckBox.Checked = config.GetBool("VacanciesNetLoader.Translation");
                int? VL = config.GetInt("VacanciesNetLoader.VacanciesLimit");
                vacanciesLimit = VL == null ? 10 : VL.Value;
                QueryTextBox.Text = config.GetStringOrDefault("VacanciesNetLoader.Query", "");
            }
            VacanciesLimitTextBox.Text = vacanciesLimit.ToString();
        }

        private async void RequestVacanciesBut_Click(object sender, EventArgs e)
        {
            LockWindow();
            string query = QueryTextBox.Text;

            ILangugageTranslator? translator = null;
            if (TranslateToEnglishCheckBox.Checked)
            {
                translator = new LibreTranslatorRequestService(5000,
                Language.English);
            }

            JobVacancy[]? vacancies = await parser.RecieveVacanies(query, vacanciesLimit: vacanciesLimit,
                onProgressChange: (progress) =>
            {
                progressBar1.Value = int.Clamp((int)(progress * 100), 0, 100);
            },
                languageTranslator: translator);
            if (vacancies == null)
            {
                MessageBox.Show("Failed to load vacancies", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show($"Loaded {vacancies.Length}", "Vacancies loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (vacancies.Length > 0)
                {
                    //close window and return vacancies
                    onDataLoaded?.Invoke(new VacanciesData()
                    {
                        query = query,
                        vacancies = vacancies
                    });
                    Close();
                }
            }
            UnlockWindow();
        }

        private void LockWindow()
        {
            QueryTextBox.ReadOnly = true;
            RequestVacanciesBut.Enabled = false;
            VacanciesLimitTextBox.Enabled = false;
            onWindowLocks?.Invoke();
        }

        private void UnlockWindow()
        {
            QueryTextBox.ReadOnly = false;
            RequestVacanciesBut.Enabled = true;
            VacanciesLimitTextBox.Enabled = true;
            onWindowUnlocks?.Invoke();
        }

        private void VacanciesLimitTextBox_Validated(object sender, EventArgs e)
        {
            if (int.TryParse(VacanciesLimitTextBox.Text, out int i))
            {
                vacanciesLimit = i;
                config?.SetInt("VacanciesNetLoader.VacanciesLimit", vacanciesLimit);
            }
            else
            {
                MessageBox.Show("Write an integer value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                VacanciesLimitTextBox.Text = vacanciesLimit.ToString();
            }
        }

        private void TranslateToEnglishCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            config?.SetBool("VacanciesNetLoader.Translation", TranslateToEnglishCheckBox.Checked);
        }

        private void VacanciesNetLoader_FormClosing(object sender, FormClosingEventArgs e)
        {
            config?.Save();
        }

        private void QueryTextBox_Validated(object sender, EventArgs e)
        {
            config?.SetString("VacanciesNetLoader.Query", QueryTextBox.Text);
        }
    }
}
