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
    public class ActionsProvider : API.IResultProvider, API.IHasPreferences<ActionsProviderPrefs>
    {
        public ActionsProvider()
        {
            UniqueName = "vizr.standard.actions";
            Items = Enumerable.Empty<IResult>();
            Icon = "cube";
        }

        public string UniqueName { get; set; }

        public string Icon { get; set; }

        public IEnumerable<IResult> Items { get; set; }

        public ActionsProviderPrefs Preferences { get; set; }

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
            Items = Preferences.Actions
                .Select(x => new ActionResult(this, x))
                .DistinctBy(x => x.ID);
        }
    }
}
