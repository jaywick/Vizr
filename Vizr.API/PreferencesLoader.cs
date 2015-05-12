using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API
{
    public class PreferencesLoader
    {
        private static JsonConverter[] Converters = new JsonConverter[]
        {
            new JsonConverters.DirectoryInfoConverter(),
            new JsonConverters.FileInfoConverter(),
        };

        public static string GetProviderPrefPath(IResultProvider provider)
        {
            return Path.Combine(Workspace.GetProviderFolder(provider).FullName, "preferences.json");
        }

        public static void Load(IResultProvider provider)
        {
            var prefType = provider.GetType().GetInterfaces()
                .Single(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHasPreferences<>))
                .GetGenericArguments().Single();

            var preferencesPath = GetProviderPrefPath(provider);

            if (!File.Exists(preferencesPath))
            {
                var newPrefValue = Activator.CreateInstance(prefType);
                GetPrefProperty(provider).SetValue(provider, newPrefValue);
                return;
            }

            var jsonContent = File.ReadAllText(preferencesPath);
            var prefValue = JsonConvert.DeserializeObject(jsonContent, prefType, Converters);

            GetPrefProperty(provider).SetValue(provider, prefValue);
        }

        public static void Save(IResultProvider provider)
        {
            var prefValue = GetPrefProperty(provider).GetValue(provider);

            var jsonContent = JsonConvert.SerializeObject(prefValue, Formatting.Indented, Converters);
            var preferencesPath = GetProviderPrefPath(provider);

            File.WriteAllText(preferencesPath, jsonContent);
        }

        private static System.Reflection.PropertyInfo GetPrefProperty(IResultProvider provider)
        {
            return provider.GetType().GetProperty("Preferences");
        }
    }
}
