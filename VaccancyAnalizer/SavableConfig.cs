using VaccancyAnalizer.Utils;
using Newtonsoft.Json.Linq;

namespace CryptoAI_Upgraded.DataSaving
{
    public class SavableConfig
    {
        private LocalLoaderAndSaverBSON<SavableConfigData> loaderAndSaver;
        private SavableConfigData configData;
        public SavableConfig(string path, string fileName)
        {
            loaderAndSaver = new LocalLoaderAndSaverBSON<SavableConfigData>(path, fileName);
            SavableConfigData? savedConfig = loaderAndSaver.Load();
            if(savedConfig == null) savedConfig = new SavableConfigData();
            configData = savedConfig;
        }

        #region getting values
        public bool GetBool(string key)
        {       
            return configData.dataBooleans.GetValueOrDefault(key);
        }

        public string? GetString(string key)
        {
            return configData.dataStrings.GetValueOrDefault(key);
        }

        public string GetStrinOrDefault(string key, string defaultValue)
        {
            if (configData.dataStrings.TryGetValue(key, out var value)) return value;
            return defaultValue;
        }

        public double? GetDouble(string key)
        {
            if (configData.dataNumbers.TryGetValue(key, out var value)) return value;
            return null;
        }

        public int? GetInt(string key)
        {
            if (configData.dataNumbers.TryGetValue(key, out var value)) return (int)value;
            return null;
        }

        public T? GetObject<T>(string key) where T : class
        {
            if (configData.dataObjects.TryGetValue(key, out var value))
            {
                try
                {
                    if (value is JToken token)
                    {
                        return token.ToObject<T>();
                    }
                    return value as T;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }


        public T GetObjectOrDefault<T>(string key, T defaultValue) where T : class
        {
            if (configData.dataObjects.TryGetValue(key, out var value))
            {
                try
                {
                    if (value is JToken token)
                    {
                        return token.ToObject<T>() ?? defaultValue;
                    }
                    return value as T ?? defaultValue;
                }
                catch (Exception)
                {
                    return defaultValue;
                }
            }
            return defaultValue;
        }
        #endregion

        #region setting values
        public void SetBool(string key, bool value)
        {
            if (configData.dataBooleans.ContainsKey(key))
            {
                configData.dataBooleans[key] = value;
            }
            else
            {
                configData.dataBooleans.Add(key, value);
            }
        }

        public void SetString(string key, string value)
        {
            if (configData.dataStrings.ContainsKey(key))
            {
                configData.dataStrings[key] = value;
            }
            else
            {
                configData.dataStrings.Add(key, value);
            }
        }

        public void SetDouble(string key, double value)
        {
            if (configData.dataNumbers.ContainsKey(key))
            {
                configData.dataNumbers[key] = value;
            }
            else
            {
                configData.dataNumbers.Add(key, value);
            }
        }

        public void SetInt(string key, int value)
        {
            if (configData.dataNumbers.ContainsKey(key))
            {
                configData.dataNumbers[key] = value;
            }
            else
            {
                configData.dataNumbers.Add(key, value);
            }
        }
        public void SetObject(string key, object value)
        {
            if (configData.dataObjects.ContainsKey(key))
            {
                configData.dataObjects[key] = value;
            }
            else
            {
                configData.dataObjects.Add(key, value);
            }
        }
        #endregion

        public void Save()
        {
            loaderAndSaver.Save(configData);
        }
    }

    internal class SavableConfigData
    {
        public Dictionary<string, string> dataStrings { get; set; }
        public Dictionary<string, double> dataNumbers { get; set; }
        public Dictionary<string, bool> dataBooleans { get; set; }
        public Dictionary<string, object> dataObjects { get; set; }

        public SavableConfigData()
        {
            if (dataStrings == null) dataStrings = new Dictionary<string, string>();
            if (dataNumbers == null) dataNumbers = new Dictionary<string, double>();
            if (dataBooleans == null) dataBooleans = new Dictionary<string, bool>();
            if (dataObjects == null) dataObjects = new Dictionary<string, object>();
        }
    }
}
