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
        private static string ShortcutFilePattern = "*.lnk";

        public FileSystemSearchProvider()
        {
            UniqueName = "vizr.standard.filesystemsearch";
            Items = Enumerable.Empty<IResult>();
        }

        public string UniqueName { get; set; }

        public IEnumerable<IResult> Items { get; set; }

        public FileSystemSearchProviderPrefs Preferences { get; set; }

        public void OnBackgroundStart()
        {
            if (!Items.Any())
                Load();
        }

        public void OnAppStart()
        {
            if (!Items.Any())
                Load();
        }

        public void OnAppHide()
        {
        }

        public void OnQueryChange(string query)
        {
        }

        public void OnPreferencesUpdated()
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
            var isRecursive = folderOption.RecurseSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var initialSearchResults = folderOption.RootFolder
                .EnumerateFileSystemInfos(folderOption.IncludePattern, isRecursive);

            var filteredSearchResults = initialSearchResults
                .Where(x => MatchesExcludePattern(x, folderOption));

            if (folderOption.IgnoreDuplicates)
                return filteredSearchResults.DistinctBy(x => x.FullName);
            else
                return filteredSearchResults;
        }

        private bool MatchesExcludePattern(FileSystemInfo fileSystemInfo, FileSystemSearchProviderPrefs.SearchFolderOption folderOption)
        {
            return folderOption.ExcludePattern.Split(";")
                .Any(x => fileSystemInfo.FullName.Like(x));
        }
    }
}
