using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizr.API;
using Vizr.StandardProviders.Extensions;

namespace Vizr.StandardProviders
{
    public class GeneratePasswordResult : IResult
    {
        public GeneratePasswordResult(IResultProvider provider, GeneratePasswordProviderPrefs prefs)
        {
            _preferences = prefs;

            ID = Hash.CreateFrom(provider.UniqueName);
            Title = "Generate Password";
            Description = "Generates a random password";
            Provider = provider;
        }

        private GeneratePasswordProviderPrefs _preferences;

        public Hash ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public IResultProvider Provider { get; set; }

        public IEnumerable<SearchableText> SearchableText
        {
            get
            {
                yield return new SearchableText(1, Title);
            }
        }

        public bool Launch()
        {
            return true;
        }

        public IPreview Preview
        {
            get { return new GeneratePasswordPreview(this, _preferences); }
            set { }
        }

        public void Edit()
        {
        }

        public void Delete()
        {
        }
    }
}
