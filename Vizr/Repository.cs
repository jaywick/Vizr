using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Vizr.API;

namespace Vizr
{
    public class Repository
    {
        private List<IResultProvider> Providers { get; set; }
        private IResultScorer Scorer { get; set; }

        public Repository()
        {
            //todo: auto load IResultProvider
            Providers = new List<IResultProvider>();
            Providers.Add(new StandardProviders.StartMenuProvider());

            Scorer = new GenericScorer();
        }

        public IEnumerable<ScoredResult> Process(string text)
        {
            var results = Providers
                .SelectMany(x => x.Query(text));

            return Scorer.Score(results)
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