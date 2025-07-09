using VaccancyAnalizer.VacanciesRequesting;
using VaccancyAnalizer.VacanciesRequesting.DataObjects;
using VaccancyAnalizer.JobAnalyticsMaking;
using VaccancyAnalizer.LLM_models;
using VaccancyAnalizer.Translation;
using System.Net.Http;
using System.Text;

namespace VaccancyAnalizer
{
    public partial class Form1 : Form
    {
        private InstructLLM llm;
        private ILangugageTranslator languageTranslator; 

        private readonly Dictionary<string, string> paths = new Dictionary<string, string>() 
        { 
            {"3B 2bits" , @"F:\LLM_models\Llama-3.2-3B-Instruct-UD-Q2_K_XL.gguf"  },
            {"3B 8bits" , @"F:\LLM_models\Llama-3.2-3B-Instruct-UD-Q8_K_XL.gguf"  }
        };
        public Form1()
        {
            InitializeComponent();
            string networkToLoad = paths["3B 8bits"];

            //languageTranslator = new LibreTranslatorRequestService(5000, Language.Russian);
            //llm = new LLM(networkToLoad);

            new AnalyticsMakerForm().Show();
        }

        private async void SendMessageBut_Click(object sender, EventArgs e)
        {

        }
    }
}
