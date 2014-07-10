using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizr
{
    static class Workspace
    {
        public static DirectoryInfo SourcesPath { get; private set; }

        static Workspace()
        {
            var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            SourcesPath = confirmPath(appdataPath, "Jay Wick Labs", "Vizr");
        }

        public static FileInfo GetFile(string fileName)
        {
            var path = Path.Combine(SourcesPath.FullName, fileName);
            return new FileInfo(path);
        }

        /// <summary>
        /// Returns a DirectoryInfo of combined path with a guarantee the path exists
        /// </summary>
        /// <param name="basePath">The base path which is known to exist</param>
        /// <param name="part">The following directories which might not exist</param>
        private static DirectoryInfo confirmPath(string basePath, params string[] parts)
        {
            var nextFolder = new DirectoryInfo(basePath);

            foreach (var item in parts)
            {
                nextFolder = new DirectoryInfo(Path.Combine(nextFolder.FullName, item));

                if (!nextFolder.Exists)
                    nextFolder.Create();
            }

            return nextFolder;
        }
    }
}
