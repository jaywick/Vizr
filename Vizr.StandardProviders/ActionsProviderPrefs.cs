using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizr.StandardProviders
{
    public class ActionsProviderPrefs : API.IProviderPreferences
    {
        public ActionsProviderPrefs()
        {
            Actions = new List<ActionItem>(new[]
                {
                    new ActionItem
                    {
                        Title = "Visit jaywick.io",
                        Application= "iexplore",
                        Path = "http://jaywick.io",
                        RunAsAdmin = false,
                        Tags = "jaywick,jay wick labs",
                    }
                });
        }

        public List<ActionItem> Actions { get; set; }
    }

    public class ActionItem
    {
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Path { get; set; }
        public string Application { get; set; }
        public bool RunAsAdmin { get; set; }
    }
}
