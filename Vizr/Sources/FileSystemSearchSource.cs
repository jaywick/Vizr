using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using Vizr.Models;

namespace Vizr.Sources
{
    class FileSystemSearch : SourceBase
    {
        private readonly string FileSearchRulesFileName = "FileSystemSearch.xml";

        private List<FileSystemInfo> items;

        public FileSystemSearch()
            : base()
        {
            Handler = new ActionsHandler();
            Decoration = new EntryDecoration("#90a959");
        }

        public override void Start()
        {
            var rules = XmlRealizer.Realize<FileSystemSearchRulesList>(Workspace.GetFile(FileSearchRulesFileName));
            var results = rules.Items.SelectMany(r => getFileSystemItems(r));

            items = results
                .Where(f => !f.Attributes.HasFlag(FileAttributes.Hidden))
                .Distinct(f => f.FullName)
                .OrderBy(f => f.Name)
                .ToList<FileSystemInfo>();
        }

        private IEnumerable<FileSystemInfo> getFileSystemItems(FileSystemSearchRule searchRule)
        {
            var directory = new DirectoryInfo(searchRule.Path);
            var recurse = searchRule.IsRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var items = new List<FileSystemInfo>();

            if (searchRule.SearchType.HasFlag(FileSearchTargets.Files))
                items.AddRange(directory.GetFiles(searchRule.Pattern, recurse));

            if (searchRule.SearchType.HasFlag(FileSearchTargets.Folders))
                items.AddRange(directory.GetDirectories(searchRule.Pattern, recurse));

            return items;
        }

        public override void Update()
        {
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
                        Title = getTitle(item),
                        Target = item.FullName,
                        ParentSource = this,
                        Relevance = score,
                    });
                }
            }

            Results = results;
        }

        private static string getTitle(FileSystemInfo item)
        {
            if (item is FileInfo)
            {
                var file = (FileInfo)item;
                return file.Directory.Name + "/" + file.GetNameWithoutExtension();
            }
            else if (item is DirectoryInfo)
            {
                var folder = (DirectoryInfo)item;
                return folder.Name + "/";
            }
            else
            {
                return item.Name;
            }
        }
    }
}
