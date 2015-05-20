using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vizr.StandardProviders.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keySelector)
        {
            return items.GroupBy(keySelector)
                        .Select(x => x.First());
        }

        public static string GetNameWithoutExtension(this FileInfo target)
        {
            if (target.Extension == "")
                return target.Name;

            return target.Name.Substring(0, target.Name.Length - target.Extension.Length);
        }

        public static bool Like(this string target, string query)
        {
            var regexQuery = query
                .Replace(@"\", @"\\")
                .Replace(".", @"\.")
                .Replace("#", @"\d")
                .Replace("?", ".")
                .Replace("*", ".*");

            return Regex.IsMatch(target, regexQuery);
        }

        public static IEnumerable<string> Split(this string target, string separator, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            return target.Split(new [] { separator }, StringSplitOptions.None);
        }

        public static bool IsDirectory(this FileSystemInfo target)
        {
            return target.Attributes.HasFlag(FileAttributes.Directory);
        }

        public static IEnumerable<FileSystemInfo> EnumerateContents(this DirectoryInfo target, string searchPattern, int recursionDepth)
        {
            var foldersAndDepth = new Queue<Tuple<DirectoryInfo, int>>();
            foldersAndDepth.Enqueue(Tuple.Create(target, 0));

            while(foldersAndDepth.Any())
            {
                var folderAndDepth = foldersAndDepth.Dequeue();

                var folder = folderAndDepth.Item1;
                var currentDepth = folderAndDepth.Item2;

                foreach (var file in folder.EnumerateFiles(searchPattern))
                {
                    yield return file;
                }

                if (currentDepth > recursionDepth)
                    continue;

                foreach (var subfolder in folder.EnumerateDirectories())
                {
                    try
                    {
                        foldersAndDepth.Enqueue(Tuple.Create(subfolder, ++currentDepth));
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // ignore unauthorized access
                    }
                }
            }
        }
    }
}
