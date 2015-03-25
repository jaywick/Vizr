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
                return;

            var jsonContent = File.ReadAllText(preferencesPath);
            var prefValue = JsonConvert.DeserializeObject(jsonContent, prefType);

            provider.GetType().GetProperty("Preferences").SetValue(provider, prefValue);
        }

        public static void Save(IResultProvider provider)
        {
            var prefValue = provider.GetType().GetProperty("Preferences").GetValue(provider);
            var jsonContent = JsonConvert.SerializeObject(prefValue, Formatting.Indented);
            var preferencesPath = GetProviderPrefPath(provider);

            File.WriteAllText(preferencesPath, jsonContent);
        }

        private static IEnumerable<System.Reflection.PropertyInfo> GetPreferenceProperties(IResultProvider provider)
        {
            return provider
                .GetType()
                .GetProperties()
                .Where(x => x
                    .GetCustomAttributes(true)
                    .Any(y => y.GetType() == typeof(ProviderPreferenceAttribute))); ;
        }
    }
}
