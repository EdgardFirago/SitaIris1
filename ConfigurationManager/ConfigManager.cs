using System;
using System.IO;
using Newtonsoft.Json;

namespace ConfigurationManager
{
    public class ConfigManager
    {
        
        public ConfigManager()
        {
           

        }
        public  ConfigClass SetConfig(String path)
        {
            using (var sr = new StreamReader(path))
            {
                JsonSerializer se = new JsonSerializer();
                var reader = new JsonTextReader(sr);
                ConfigClass DeserializedObject = se.Deserialize<ConfigClass>(reader);
                Console.WriteLine("Config parsed");
                return DeserializedObject;
            }
            
        }
    }
    
}
