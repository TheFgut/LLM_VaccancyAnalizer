using LLama;
using LLama.Common;
using LLama.Sampling;


namespace VaccancyAnalizer.LLM_models
{
    public class InstructLLM
    {
        public uint ContextSize { get; private set; } = 4096;
        public int MaxAnswerLen { get; private set; } = 512;
        public int InstructionsSize {  get; private set; }

        private string modelPath;

        private InteractiveExecutor executor;
        private LLMInstructions instructions;
        private InferenceParams inferenceParams;
        private LLamaContext context;
        private ModelParams parameters;
        private LLamaWeights model;

        public InstructLLM(string path, List<string>? antiPrompts = null, LLMInstructions? instructions = null)
        {
            if (antiPrompts == null) antiPrompts = ["Finish", "###", "[INST]"];
            modelPath = path;
            parameters = new ModelParams(modelPath)
            {
                ContextSize = ContextSize,
                GpuLayerCount = 28,
            };

            model = LLamaWeights.LoadFromFile(parameters);
            context = model.CreateContext(parameters);
            executor = new InteractiveExecutor(context);
            inferenceParams = new InferenceParams()
            {
                MaxTokens = MaxAnswerLen, // No more than MaxAnswerLen tokens should appear in answer.     
                AntiPrompts = antiPrompts,
                SamplingPipeline = new DefaultSamplingPipeline()
                {
                    Temperature = 0f,//creativity
                    TopK = 20,
                    TopP = 1f,
                    MinP = 0.99f,
                    FrequencyPenalty = 0,
                    PresencePenalty = 0,
                    TypicalP = 1,
                    RepeatPenalty = 1.2f,
                    Seed = 1337
                }
            };

            // Add chat histories as prompt to tell AI how to act.
            if(instructions == null) instructions = new LLMInstructions();
            SetLLMInstructions(instructions);
        }

        public void SetLLMInstructions(LLMInstructions instructions)
        {
            if (instructions == null) throw new Exception("LLM.SetLLMInstructions(). instructions cant be null");
            this.instructions = instructions;
            InstructionsSize = CalculateTokensCount(instructions.instructionPromt);
            if(InstructionsSize + MaxAnswerLen > ContextSize * 0.5f)
            {
                MessageBox.Show($"Model context is too low: instSize({InstructionsSize}) + maxAnswerLen({MaxAnswerLen}) " +
                    $"> ContextSize({ContextSize}) * 0.5f","Warning",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //session = new(executor, instructions.generateChatHistory());
            //initialState = session.GetSessionState();
        }

        public async Task<AnalysisStatus> Talk(string userInput, Action<string> onTokenAppeared)
        {
            if (!SimpleCheckFitInContext(userInput))
            {
                onTokenAppeared?.Invoke("Skipped.");
                return AnalysisStatus.Skipped;
            }
            string input = $"{instructions.instructionPromt}\n\n[INST]{userInput}[/INST]\n\n";
            //string input = $"<s>[INST] <<SYS>>{instructions.instructionPromt}\n<</SYS>>\n{userInput}\n\n[/INST]";
            //string input = instructions.instructionPromt + "[/INST]";
            await foreach (var text in executor.InferAsync(input, inferenceParams))
            {
                onTokenAppeared?.Invoke(text);
            }
            return AnalysisStatus.Success;
        }

        public void ResetLLM()
        {
            //session.LoadSession(initialState);
            context?.Dispose();
            context = model.CreateContext(parameters);
            executor = new InteractiveExecutor(context);
            //session = new(executor, instructions.generateChatHistory());
        }

        public int CalculateTokensCount(string? text)
        {
            if(string.IsNullOrEmpty(text)) return 0;
            var ids = context.Tokenize(text, addBos: true);
            int tokenCount = ids.Length;
            return tokenCount;
        }

        public bool SimpleCheckFitInContext(string text)
        {
            if (string.IsNullOrEmpty(text)) return false;
            InstructionsSize = CalculateTokensCount(instructions.instructionPromt);
            return InstructionsSize + MaxAnswerLen < ContextSize;
        }
    }

    public class LLMInstructions
    {
        public string? instructionPromt {  get; private set; }
        public LLMInstructions(string? instructionPromt = null)
        {
            this.instructionPromt = instructionPromt;
        }

        public ChatHistory generateChatHistory()
        {
            var history = new ChatHistory();
            if(instructionPromt != null) history.AddMessage(AuthorRole.System, instructionPromt); 
            return history;
        }
    }
}
