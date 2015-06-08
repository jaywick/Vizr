using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Vizr.API;
using Vizr.StandardProviders.Extensions;

namespace Vizr.StandardProviders
{
    public class FileSystemSearchProvider : API.IResultProvider, API.IHasPreferences<FileSystemSearchProviderPrefs>
    {
        private static string ShortcutFileExtension = ".lnk";

        public FileSystemSearchProvider()
        {
            Items = Enumerable.Empty<IResult>();
        }

        public string UniqueName
        {
            get { return "vizr.standard.filesystemsearch"; }
        }

        public string Name
        {
            get { return "File System Search"; }
        }

        public string Icon
        {
            get { return "binoculars"; }
        }

        public IEnumerable<IResult> Items { get; set; }

        public FileSystemSearchProviderPrefs Preferences { get; set; }

        public void OnStart()
        {
            Load();
        }

        public void OnAwake()
        {
        }

        public void OnExit()
        {
        }

        public void OnQueryChange(string query)
        {
        }

        private void Load()
        {
            var searchResults = new List<FileSystemInfo>();

            foreach (var folderOption in Preferences.Folders)
            {
                if (folderOption.IncludeRoot)
                    searchResults.Add(folderOption.RootFolder);

                searchResults.AddRange(PerformSearch(folderOption));
            }

            Items = searchResults.Select(x => FileSystemSearchResult.CreateFrom(this, x));
        }

        private IEnumerable<FileSystemInfo> PerformSearch(FileSystemSearchProviderPrefs.SearchFolderOption folderOption)
        {
            var filteredResults = folderOption.RootFolder
                .EnumerateContents(folderOption.IncludePattern, folderOption.MaxSubfolderDepth)
                .Where(x => Filter(x, folderOption));

            if (folderOption.IgnoreDuplicates)
                return filteredResults.DistinctBy(x => x.FullName);
            else
                return filteredResults;
        }

        private bool Filter(FileSystemInfo fileSystemInfo, FileSystemSearchProviderPrefs.SearchFolderOption folderOption)
        {
            if (!String.IsNullOrWhiteSpace(folderOption.ExcludePattern))
            {
                var matchesExclusionPattern = folderOption.ExcludePattern.Split(";")
                    .Any(x => fileSystemInfo.FullName.Like(x));

                if (!matchesExclusionPattern)
                    return false;
            }

            if (folderOption.IgnoreAnyFiles && !fileSystemInfo.IsDirectory())
                return false;

            if (folderOption.IgnoreFolders && fileSystemInfo.IsDirectory())
                return false;

            if (folderOption.IgnoreHiddenItems && fileSystemInfo.Attributes.HasFlag(FileAttributes.Hidden))
                return false;

            if (folderOption.IgnoreHiddenSystemItems && fileSystemInfo.Attributes.HasFlag(FileAttributes.Hidden) && fileSystemInfo.Attributes.HasFlag(FileAttributes.System))
                return false;

            if (folderOption.IgnoreNonShortcutFiles && !fileSystemInfo.IsDirectory() && fileSystemInfo.Extension != ShortcutFileExtension)
                return false;

            if (folderOption.IgnoreShortcuts && !fileSystemInfo.IsDirectory() && fileSystemInfo.Extension == ShortcutFileExtension)
                return false;

            if (folderOption.IgnoreSystemItems && fileSystemInfo.Attributes.HasFlag(FileAttributes.System))
                return false;

            return true;
        }
    }
}