using VaccancyAnalizer.LLM_models;
using VaccancyAnalizer.Translation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VaccancyAnalizer.JobAnalyticsMaking
{
    public partial class LLM_ChatPanel : UserControl
    {
        private InstructLLM llm;
        private ILangugageTranslator translator;
        public LLM_ChatPanel()
        {
            InitializeComponent();
            if(llm == null)
            {
                inputTextPanel.ReadOnly = true;
                SendMessageBut.Enabled = false;
            }
        }

        public void Init(InstructLLM llm, ILangugageTranslator translator)
        {
            this.llm = llm;
            this.translator = translator;
            inputTextPanel.ReadOnly = false;
            SendMessageBut.Enabled = true;
        }

        private async void SendMessageBut_Click(object sender, EventArgs e)
        {
            string message = inputTextPanel.Text;
            inputTextPanel.Text = "";
            await SendMessage(message);
        }

        public async Task SendMessage(string message, bool translate = true)
        {
            if (llm == null) throw new Exception("llm is not assigned");
            inputTextPanel.ReadOnly = true;
            SendMessageBut.Enabled = false;

            outputTextPanel.Text += $"\n\nUser: {message}\n\n";

            string msg = translate ? await translator.TranslateToEnglish(message) : message;
            string llmAnswer = "";
            await llm.Talk(msg, (token) =>
            {
                outputTextPanel.Text += token;
                llmAnswer += token;
            });
            string answer = translate ? await translator.TranslateFromEnglish(llmAnswer) : llmAnswer;
            outputTextPanel.Text = outputTextPanel.Text.Remove(outputTextPanel.Text.Length -
                llmAnswer.Length) + answer;

            inputTextPanel.ReadOnly = false;
            SendMessageBut.Enabled = true;
        }
    }
}
