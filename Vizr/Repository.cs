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
        private List<IResultProvider> Providers { get; set; }
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

            var providers = dlls
                .Select(x => Assembly.LoadFile(x.FullName))
                .SelectMany(x => x.GetTypes())
                .Where(t => typeof(IResultProvider).IsAssignableFrom(t));

            if (!providers.Any())
                return;

            var providerInstances = providers
                .Select(t => (IResultProvider)Activator.CreateInstance(t))
                .ToList();

            var providersWithPreferences = providerInstances
                .Where(x => x.GetType().GetInterfaces()
                    .Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHasPreferences<>)));

            foreach (var provider in providersWithPreferences)
            {
                PreferencesLoader.Load(provider);
            }

            Providers = new List<IResultProvider>(providerInstances);
        }

        public IEnumerable<ScoredResult> Query(string queryText)
        {
            Providers.ForEach(x => x.OnQueryChange(queryText));

            var results = Providers
                .SelectMany(x => x.Items);

            return Scorer.Score(queryText, results)
                .OrderByDescending(x => x.Score);
        }

        public void OnAppStart()
        {
            Providers.ForEach(x => x.OnAppStart());
        }

        public void OnAppHide()
        {
            Providers.ForEach(x => x.OnAppHide());
        }

        public void OnBackgroundStart()
        {
            Providers.ForEach(x => x.OnBackgroundStart());
        }
    }
}