using LLama;
using LLama.Common;
using LLama.Sampling;


namespace VaccancyAnalizer.LLM_models
{
    public class TranslationLLM
    {
        private string modelPath;

        private InteractiveExecutor executor;
        private ChatHistory chatHistory;
        private InferenceParams inferenceParams;
        public TranslationLLM(string originLanguage, string targetLanguage, string path)
        {
            modelPath = path;
            var parameters = new ModelParams(modelPath)
            {
                ContextSize = 1024,
                Threads = 6
            };
            var model = LLamaWeights.LoadFromFile(parameters);
            var context = model.CreateContext(parameters);
            executor = new InteractiveExecutor(context);
            inferenceParams = new InferenceParams()
            {
                MaxTokens = 256, // No more than 256 tokens should appear in answer. Remove it if antiprompt is enough for control.
                AntiPrompts = new List<string> { "\nUser:", "User: ", "Note: " }, // Stop generation once antiprompts appear.

                SamplingPipeline = new DefaultSamplingPipeline(),
            };

            chatHistory = new ChatHistory();
            chatHistory.AddMessage(AuthorRole.System, $"" +
                $"Translate the text that user tells you from {originLanguage} to {targetLanguage}." +
                $" Your reply should contain ONLY the translated text, nothing else." +
                $" Please use exactly the same formatting as the original text.");
        }

        public async Task Translate(string userInput, Action<string> onTokenAppeared)
        {
            ChatSession session = new(executor, chatHistory);

            await foreach ( // Generate the response streamingly.
                var text
                in session.ChatAsync(
                    new ChatHistory.Message(AuthorRole.User, $"User: {userInput}"),
                    inferenceParams))
            {
                onTokenAppeared?.Invoke(text);
            }
        }
    }
}
