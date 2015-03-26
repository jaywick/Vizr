using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizr.API
{
    static class Workspace
    {
        public static DirectoryInfo SourcesPath { get; private set; }

        static Workspace()
        {
            var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            SourcesPath = new DirectoryInfo(Path.Combine(appdataPath, "Jay Wick Labs", "Vizr"));

            if (!SourcesPath.Exists)
                SourcesPath.Create();
        }

        public static DirectoryInfo GetProviderFolder(IResultProvider provider)
        {
            var safeFolderName = String.Join("", provider.UniqueName.Where(x => !Path.GetInvalidFileNameChars().Contains(x)));
            var path = Path.Combine(SourcesPath.FullName, "Providers", safeFolderName);

            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
                directory.Create();

            return directory;
        }
    }
}
