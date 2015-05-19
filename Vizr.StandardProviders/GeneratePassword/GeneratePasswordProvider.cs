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
            UniqueName = "vizr.standard.generatepassword";
            Items = Enumerable.Empty<IResult>();
            Icon = "key";
        }

        public string UniqueName { get; set; }

        public string Icon { get; set; }

        public IEnumerable<IResult> Items { get; set; }

        public GeneratePasswordProviderPrefs Preferences { get; set; }

        public void OnBackgroundStart()
        {
            if (!Items.Any())
                Load();
        }

        public void OnAppStart()
        {
            if (!Items.Any())
                Load();
        }

        public void OnAppHide()
        {
        }

        public void OnQueryChange(string query)
        {
        }

        public void OnPreferencesUpdated()
        {
        }

        private void Load()
        {
            Items = new[] { new GeneratePasswordResult(this, Preferences) };
        }
    }
}
