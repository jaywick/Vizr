using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;

namespace Vizr.Sources
{
    class StartMenuAppsSource : SourceBase
    {
        private List<FileInfo> items;

        public StartMenuAppsSource()
            : base()
        {
            Handler = new ActionsHandler();
        }

        public override void Update()
        {
            var myApps = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu))
                .GetFiles("*.lnk", SearchOption.AllDirectories);

            var allApps = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu))
                .GetFiles("*.lnk", SearchOption.AllDirectories);

            items = myApps
                .Union((allApps))
                .Where(a => !a.Name.ToLower().Contains("uninstal"))
                .OrderBy(a => a.Name)
                .ToList();
        }

        public override void Query(string text)
        {
            var results = new List<Action>();

            foreach (var item in items)
            {
                if (item.Name.ToLower().ContainsPartialsOf(text.ToLower()))
                {
                    results.Add(new Action()
                    {
                        Title = item.GetNameWithoutExtension(),
                        Target = item.FullName,
                        ParentSource = this,
                    });
                }
            }

            Results = results;
        }
    }
}
