using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizr.StandardProviders
{
    public class FileSystemSearchProviderPrefs : API.IProviderPreferences
    {
        public FileSystemSearchProviderPrefs()
        {
            Folders = new List<SearchFolderOption>();
        }

        public List<SearchFolderOption> Folders { get; set; }

        public class SearchFolderOption
        {
            public SearchFolderOption()
            {
                MaxSubfolderDepth = 5;
                Rescan = false;
                IncludePattern = "*";
                ExcludePattern = null;
                IgnoreDuplicates = true;
                IncludeRoot = true;
                IgnoreShortcuts = false;
                IgnoreNonShortcutFiles = false;
                IgnoreAnyFiles = false;
                IgnoreFolders = false;
                IgnoreHiddenItems = false;
                IgnoreSystemItems = false;
                IgnoreHiddenSystemItems = false;
            }

            public DirectoryInfo RootFolder { get; set; }
            public int MaxSubfolderDepth { get; set; }
            public bool Rescan { get; set; }
            public string IncludePattern { get; set; }
            public string ExcludePattern { get; set; }
            public bool IgnoreDuplicates { get; set; }
            public bool IncludeRoot { get; set; }
            public bool IgnoreShortcuts { get; set; }
            public bool IgnoreNonShortcutFiles { get; set; }
            public bool IgnoreAnyFiles { get; set; }
            public bool IgnoreFolders { get; set; }
            public bool IgnoreHiddenItems { get; set; }
            public bool IgnoreSystemItems { get; set; }
            public bool IgnoreHiddenSystemItems { get; set; }
        }
    }
}
