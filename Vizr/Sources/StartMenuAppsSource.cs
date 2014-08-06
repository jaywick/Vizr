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
            Decoration = new EntryDecoration("#6a9fb5");
        }

        public override void Update()
        {
            var files = new List<FileInfo>();

            files.AddRange(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu)).GetFiles("*.lnk", SearchOption.AllDirectories));
            files.AddRange(new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu)).GetFiles("*.lnk", SearchOption.AllDirectories));

            items = files
                .Where(a => !a.Name.ToLower().Contains("uninstal"))
                .Distinct(a => a.FullName)
                .OrderBy(a => a.Name)
                .ToList();
        }

        public override void Query(string text)
        {
            var results = new List<ActionEntry>();

            foreach (var item in items)
            {
                var score = TextCompare.Score(text, item.Name);

                if (score > 0)
                {
                    results.Add(new ActionEntry
                    {
                        Title = item.GetNameWithoutExtension(),
                        Target = item.FullName,
                        ParentSource = this,
                        Relevance = score,
                    });
                }
            }

            Results = results;
        }
    }
}
