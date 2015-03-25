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

        public static FileInfo GetFile(string fileName)
        {
            var path = Path.Combine(SourcesPath.FullName, fileName);
            return new FileInfo(path);
        }
    }
}
