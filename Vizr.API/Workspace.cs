using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizr.API
{
    public static class Workspace
    {
        public static DirectoryInfo Root { get; private set; }

        static Workspace()
        {
            var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            Root = new DirectoryInfo(Path.Combine(appdataPath, "Jay Wick Labs", "Vizr"));

            if (!Root.Exists)
                Root.Create();
        }

        public static DirectoryInfo GetProviderFolder(IResultProvider provider)
        {
            var safeFolderName = String.Join("", provider.UniqueName.Where(x => !Path.GetInvalidFileNameChars().Contains(x)));
            var path = Path.Combine(Root.FullName, "Providers", safeFolderName);

            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
                directory.Create();

            return directory;
        }
    }
}
