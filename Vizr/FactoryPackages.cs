using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public class FactoryPackages
    {
        public static Package CreateDefaultPackage()
        {
            var package = new Package(RepositoryHelper.DefaultPackageName);

            // labs link
            package.Items.Add(new Action()
            {
                Pattern = "Visit Jay Wick Labs",
                Title = "Visit Jay Wick Labs",
                Target = "http://labs.jay-wick.com",
            });

            // google
            package.Items.Add(new Request()
            {
                Pattern = @"imdb (.+)",
                Title = "Search IMDB for '{0}'",
                Target = "http://www.imdb.com/find?q={0}",
            });

            // google
            package.Items.Add(new Request()
            {
                Pattern = @"(.+)",
                Title = "Google for '{0}'",
                Target = "https://www.google.com/search?q={0}",
            });

            // google IFL
            package.Items.Add(new Request()
            {
                Pattern = @"(.+)",
                Title = "I'm feeling lucky '{0}'",
                Target = "https://www.google.com/search?q={0}&btnI",
            });

            // find on pc
            /// see more: http://msdn.microsoft.com/en-us/library/ff684385.aspx
            package.Items.Add(new Request()
            {
                Pattern = @"(.+)",
                Title = "Search PC for '{0}'",
                Target = "search-ms:query={0}&",
            });

            return package;
        }

        public static Package CreateMetaPackage()
        {
            var package = new Package(RepositoryHelper.MetaPackageName);

            // labs link
            package.Items.Add(new Action()
            {
                Pattern = "About",
                Title = "About this app",
                Target = "https://github.com/jaywick/vizr"
            });

            // edit commands
            package.Items.Add(new Action()
            {
                Pattern = "Edit",
                Title = "Edit commands",
                Target = RepositoryHelper.PackagesPath.FullName
            });

            return package;
        }
    }
}
