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
                        Tags = new List<string> { "jaywick", "jay wick labs" },
                    }
                });
        }

        public List<ActionItem> Actions { get; set; }
    }

    public class ActionItem
    {
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string Path { get; set; }
        public string Application { get; set; }
        public bool RunAsAdmin { get; set; }

        public ActionItem()
        {
            Title = "";
            Tags = new List<string>();
            Path = "";
            Application = "";
            RunAsAdmin = false;
        }
    }
}
