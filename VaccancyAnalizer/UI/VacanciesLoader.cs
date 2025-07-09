using CryptoAI_Upgraded.DataSaving;
using VaccancyAnalizer.VacanciesRequesting;
using VaccancyAnalizer.VacanciesRequesting.DataObjects;
using VaccancyAnalizer.JobAnalyticsMaking;
using VaccancyAnalizer.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VaccancyAnalizer
{
    public partial class VacanciesLoader : UserControl
    {
        private IVacanciesRequester requester;
        private VacanciesData? vacanciesData;
        private SaveManager<VacanciesData> saveManager;
        private VacanciesNetLoader? vacanciesNetLoader;
        private SavableConfig? config;
        public Action<VacanciesData> onVacanciesLoaded;

        private const string vacanciesExt = ".vacancies";
        public VacanciesLoader()
        {
            InitializeComponent();
            requester = new VacanciesDjinnyRequester();
            saveManager = new SaveManager<VacanciesData>();
            SaveBut.Enabled = vacanciesData != null;
            this.Disposed += (sender, args) => vacanciesNetLoader?.Close();
        }

        public void SetConfig(SavableConfig? config)
        {
            this.config = config;
        }

        private void LoadNewBut_Click(object sender, EventArgs e)
        {
            vacanciesNetLoader = new VacanciesNetLoader(requester, config : config);
            vacanciesNetLoader.onDataLoaded += (data) => 
            {
                AssignData(data);
    //            string fileName = vacanciesData.query.Take(vacanciesData.query.Length < 10 ?
    //vacanciesData.query.Length : 10).ToString() + (vacanciesData.query.Length < 10 ? "" : "...");
    //            saveManager.Save(vacanciesData, fileName);
                };
            vacanciesNetLoader.Show();
        }

        private void AssignData(VacanciesData? data)
        {
            vacanciesData = data;
            SaveBut.Enabled = data != null;
            DetailsTextBox.Text = data == null ? "not loaded" : $"VacanciesCount:{vacanciesData.vacancies.Length}" +
                $"\nQuery:{vacanciesData.query}";
            onVacanciesLoaded?.Invoke(data);
        }

        private async void LoadExistingBut_Click(object sender, EventArgs e)
        {
            // Создаём диалог открытия
            using var dlg = new OpenFileDialog
            {
                Title = "Choose vaccancies to load",
                InitialDirectory = DataPaths.VacanciesDataPath,
                RestoreDirectory = true
            };

            // Если пользователь выбрал файл и нажал OK
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string loadPath = dlg.FileName;

                try
                {
                    // Предполагаем, что saveManager.Load возвращает тот же тип, что и vacanciesData
                    var loadedData = (VacanciesData)(await saveManager.Load(loadPath));

                    if (loadedData != null)
                    {
                        AssignData(loadedData);
                    }
                    else
                    {
                        MessageBox.Show("Не удалось распознать формат файла.", "Ошибка загрузки",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке файла:\n{ex.Message}", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveBut_Click(object sender, EventArgs e)
        {
            bool queryNameIsTooBig = vacanciesData.query.Length < 10;
            string fileName = $"Vacancies: {vacanciesData.query.Take(queryNameIsTooBig ?
                vacanciesData.query.Length : 10).ToString() + (queryNameIsTooBig ? "" : "...")}{vacanciesExt}";

            using var dlg = new SaveFileDialog
            {
                Title = "Select save path",
                FileName = fileName,
                InitialDirectory = DataPaths.VacanciesDataPath,
                OverwritePrompt = true,
                RestoreDirectory = true
            };
            // Показываем диалог и ждём, что пользователь нажмёт OK
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string savePath = dlg.FileName;
                saveManager.Save(vacanciesData, savePath);
            }
        }
    }
}
