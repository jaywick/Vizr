using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizr.StandardProviders
{
    public class StartMenuProviderPrefs : API.IProviderPreferences
    {
        public bool IgnoreUninstallers { get; set; }

        public bool IgnoreDuplicates { get; set; }

        public string IgnorePattern { get; set; }

        public List<DirectoryInfo> AdditionalSearchFolders { get; set; }

        public List<FileInfo> IgnoredItems { get; set; }

        public List<DirectoryInfo> IgnoredFolders { get; set; }
    }
}
