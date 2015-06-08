using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Vizr.API;
using Vizr.Extensions;

namespace Vizr
{
    public class Repository
    {
        private List<IResultProvider> _providers;
        public IEnumerable<IResultProvider> Providers
        {
            get { return _providers; }
        }

        public IEnumerable<IResultProvider> ProvidersWithPrefs
        {
            get { return _providers.Where(HasPreferences); }
        }

        private IResultScorer Scorer { get; set; }

        public History History { get; private set; }

        public Repository()
        {
            Load();
        }

        public void Load()
        {
            LoadHistory();
            ImportProviders();

            Scorer = new GenericScorer();
        }

        private void LoadHistory()
        {
            History = new History();
        }

        private void ImportProviders()
        {
            var currentDirectory = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);

            var dlls = currentDirectory
                .EnumerateFiles("*.dll")
                .Where(x => x.Name != "Vizr.API.dll");

            if (!dlls.Any())
                return;

            var providerTypes = GetProviderClassesFromDLLs(dlls);

            if (!providerTypes.Any())
                return;

            var providerInstances = CreateProviderInstances(providerTypes);

            var providersWithPreferences = providerInstances.Where(HasPreferences);

            LoadPreferencesForProviders(providersWithPreferences);

            _providers = new List<IResultProvider>(providerInstances);
        }

        private static void LoadPreferencesForProviders(IEnumerable<IResultProvider> providers)
        {
            foreach (var provider in providers)
            {
                try
                {
                    PreferencesLoader.Load(provider);
                }
                catch (Exception ex)
                {
                    Logging.WriteLine("Failed loading provider '{0}' - {1}", provider.UniqueName, ex.Message);
                    continue;
                }
            }
        }

        private static List<Type> GetProviderClassesFromDLLs(IEnumerable<FileInfo> dlls)
        {
            var providerTypes = new List<Type>();

            foreach (var dll in dlls)
            {
                Assembly assembly;
                try
                {
                    assembly = Assembly.LoadFile(dll.FullName);
                }
                catch (Exception ex)
                {
                    Logging.WriteLine("Failed loading DLL '{0}' - {1}", dll.Name, ex.Message);
                    continue;
                }

                var types = assembly.GetTypes();
                providerTypes.AddRange(types.Where(t => typeof(IResultProvider).IsAssignableFrom(t)));
            }

            return providerTypes;
        }

        private static List<IResultProvider> CreateProviderInstances(IEnumerable<Type> providerTypes)
        {
            var providerInstances = new List<IResultProvider>();

            foreach (var providerType in providerTypes)
            {
                IResultProvider instance;
                try
                {
                    instance = (IResultProvider)Activator.CreateInstance(providerType);
                }
                catch (Exception ex)
                {
                    Logging.WriteLine("Failed instantiating provider class '{0}' - {1}", providerType.Name, ex.Message);
                    continue;
                }

                providerInstances.Add(instance);
            }

            return providerInstances;
        }

        private bool HasPreferences(IResultProvider provider)
        {
            return provider
                .GetType()
                .GetInterfaces()
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IHasPreferences<>));
        }

        public IEnumerable<ScoredResult> Query(string queryText)
        {
            _providers.ForEach(x => x.OnQueryChange(queryText));

            var results = _providers
                .SelectMany(x => x.Items);

            return Scorer.Score(queryText, results)
                .OrderByDescending(x => x.Score);
        }

        public void InvokeProviderStart()
        {
            foreach (var provider in _providers)
            {
                provider.OnStart();
            }
        }

        public void InvokeProviderExit()
        {
            foreach (var provider in _providers)
            {
                provider.OnExit();
            }
        }
    }
}