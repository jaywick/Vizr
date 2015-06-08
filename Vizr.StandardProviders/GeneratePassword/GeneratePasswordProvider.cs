using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vizr.API;
using Vizr.StandardProviders.Extensions;

namespace Vizr.StandardProviders
{
    public class GeneratePasswordProvider : API.IResultProvider, API.IHasPreferences<GeneratePasswordProviderPrefs>
    {
        public GeneratePasswordProvider()
        {
            Items = Enumerable.Empty<IResult>();
        }

        public string UniqueName
        {
            get { return "vizr.standard.generatepassword"; }
        }

        public string Name
        {
            get { return "Password Generator"; }
        }

        public string Icon
        {
            get { return "key"; }
        }

        public IEnumerable<IResult> Items { get; set; }

        public GeneratePasswordProviderPrefs Preferences { get; set; }

        public void OnStart()
        {
            Load();
        }

        public void OnAwake()
        {
        }

        public void OnExit()
        {
        }

        public void OnQueryChange(string query)
        {
        }

        private void Load()
        {
            Items = new[] { new GeneratePasswordResult(this, Preferences) };
        }
    }
}
