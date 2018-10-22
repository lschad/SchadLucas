using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SchadLucas.Configuration
{
    public sealed class EzFileConfiguration : EzConfiguration
    {
        private readonly string _filePath;

        private JObject _configuration;

        public EzFileConfiguration(string filePath)
        {
            _filePath = filePath;
        }

        public override T Get<T>(string key)
        {
            var cfg = GetConfiguration();
            return cfg[key] != null ? cfg[key].ToObject<T>() : default;
        }

        public override void Set(string key, object value)
        {
            var cfg = GetConfiguration();
            if (!cfg.ContainsKey(key))
            {
                cfg.Add(new JProperty(key, value));
            }
            else
            {
                cfg[key] = new JValue(value);
            }

            Save();
        }

        private JObject GetConfiguration()
        {
            void CreateDefaultConfiguration()
            {
                var content = JsonConvert.SerializeObject(new { });
                File.WriteAllText(_filePath, content);
            }

            JObject LoadConfiguration()
            {
                var config = File.ReadAllText(_filePath);
                var obj = null as JObject;

                try
                {
                    obj = JObject.Parse(config);
                }
                catch (JsonReaderException)
                {
                    //todo: hmmm....
                }

                return obj;
            }

            JObject GetOrCreate()
            {
                if (!File.Exists(_filePath))
                {
                    CreateDefaultConfiguration();
                }

                var config = LoadConfiguration();
                if (config == null)
                {
                    CreateDefaultConfiguration();
                    config = LoadConfiguration();
                }

                return config;
            }

            return _configuration ?? (_configuration = GetOrCreate());
        }

        private void Save()
        {
            File.WriteAllText(_filePath, _configuration.ToString());
        }
    }
}