using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizr
{
    static class RepositoryHelper
    {
        public static DirectoryInfo PackagesPath { get; private set; }
        public const string PackageExtension = ".vizr-package";
        public const string DefaultPackageName = "default";
        public const string MetaPackageName = "meta";
        public const string DefaultXMLWarning = "\n\tWARNING!\n\t" +
                                                "Changes to the default package will be lost when updating this\n\t" +
                                                "program with featured commands of that version.\n\t" +
                                                "Please use the user.xml package instead!\n";

        static RepositoryHelper()
        {
            var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            PackagesPath = confirmPath(appdataPath, "Jay Wick Labs", "Vizr", "Packages");
        }

        public static FileInfo[] Packages
        {
            get
            {
                return PackagesPath.GetFiles("*" + PackageExtension);
            }
        }

        public static FileInfo GetPathFromPackageName(string name)
        {
            return new FileInfo(Path.Combine(PackagesPath.FullName, name + PackageExtension));
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

        public static FileInfo DefaultPackage 
        {
            get
            {
                return GetPathFromPackageName(DefaultPackageName);
            }
        }
    }
}
