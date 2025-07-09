using Newtonsoft.Json.Bson;
using Newtonsoft.Json;

namespace VaccancyAnalizer.Utils
{
    internal class LocalLoaderAndSaverBSON<T> where T : class
    {
        public string fullPath { get; private set; }
        public LocalLoaderAndSaverBSON(string path, string fileName)
        {
            fullPath = $"{path}\\{fileName}.bson";
        }

        public LocalLoaderAndSaverBSON(string path)
        {
            fullPath = path;
        }


        public void Save(T dataToSerialize)
        {
            using (var fileStream = File.OpenWrite(fullPath))
            using (var bsonWriter = new BsonDataWriter(fileStream))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(bsonWriter, dataToSerialize);
            }
        }

        public T? Load()
        {
            if(!File.Exists(fullPath)) return null;
            using (var fileStream = File.OpenRead(fullPath))
            using (var bsonReader = new BsonDataReader(fileStream))
            {
                try
                {
                    var serializer = new JsonSerializer();

                    T? data = serializer.Deserialize<T>(bsonReader);
                    return data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Data loading failed {ex.Message}\n {ex.StackTrace}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
        }
    }
}
