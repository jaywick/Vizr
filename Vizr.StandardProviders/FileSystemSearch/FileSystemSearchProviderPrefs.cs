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
                RecurseSubdirectories = true;
                Rescan = false;
                IncludePattern = "*";
                ExcludePattern = null;
                IgnoreDuplicates = true;
                IncludeRoot = true;
            }

            public DirectoryInfo RootFolder { get; set; }
            public bool RecurseSubdirectories { get; set; }
            public bool Rescan { get; set; }
            public string IncludePattern { get; set; }
            public string ExcludePattern { get; set; }
            public bool IgnoreDuplicates { get; set; }
            public bool IncludeRoot { get; set; }
        }
    }
}
