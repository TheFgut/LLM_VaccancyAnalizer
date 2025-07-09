using System.Text.Json;

namespace VaccancyAnalizer.Utils
{
    public class SaveManager<T>
    {
        private readonly JsonSerializerOptions _jsonOptions;

        public SaveManager(JsonSerializerOptions? jsonOptions = null)
        {
            _jsonOptions = jsonOptions ?? new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public string Save(T item, string fullPath)
        {
            string json = JsonSerializer.Serialize(item, _jsonOptions);

            File.WriteAllText(fullPath, json);

            return fullPath;
        }

        public async Task<string> SaveAsync(T item, string fullPath)
        {
            using (var stream = File.Create(fullPath))
            {
                await JsonSerializer.SerializeAsync(stream, item, _jsonOptions);
            }

            return fullPath;
        }

        public async Task<T> Load(string path)
        {
            string json = await File.ReadAllTextAsync(path);

            T vacanciesData = JsonSerializer.Deserialize<T>(json)
                            ?? throw new InvalidOperationException("Serialization failed");
            return vacanciesData;
        }
    }
}

