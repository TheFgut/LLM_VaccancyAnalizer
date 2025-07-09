using System.ComponentModel;

namespace VaccancyAnalizer.LLM_models.UI
{
    public partial class LLM_Loader : UserControl
    {
        private InstructLLM llm;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<InstructLLM> OnLMChanged {  get; set; }
        public LLM_Loader()
        {
            InitializeComponent();
        }

        private void SetupBut_Click(object sender, EventArgs e)
        {
            llm = new InstructLLM(@"F:\LLM_models\TheBloke\Llama-2-7B-32K-Instruct-GGUF/llama-2-7b-32k-instruct.Q4_K_S.gguf",
                antiPrompts: new List<string>() { "Finish"});
                OnLMChanged?.Invoke(llm);
                SetupBut.Enabled = false;
        }
    }
}
