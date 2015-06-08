using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vizr.API;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Vizr
{
    public partial class PrefsWindow : Window
    {
        private Repository Repository { get; set; }

        private PrefsWindow()
        {
            InitializeComponent();
        }

        public PrefsWindow(Repository repository)
            : this()
        {
            Repository = repository;

            foreach (var provider in Repository.ProvidersWithPrefs)
            {
                var prefs = PreferencesLoader.Read(provider);

                foreach (PropertyInfo property in prefs.GetType().GetProperties())
                {
                    preferencesList.Items.Add(new PreferenceRow(provider, property.Name, property.GetValue(prefs)));
                    property.GetValue(prefs);
                }
            }
        }
    }

    public class PreferenceRow
    {
        public IResultProvider Provider { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        public string ProviderName
        {
            get { return Provider.UniqueName; }
        }

        public PreferenceRow(IResultProvider provider, string key, object value)
        {
            Provider = provider;
            Key = key;
            Value = (value == null) ? "null" : value.ToString();
        }
    }
}
