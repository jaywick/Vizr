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
    public class StartMenuProvider : API.IResultProvider
    {
        private static string ShortcutFilePattern = "*.lnk";

        public StartMenuProvider()
        {
            UniqueName = "vizr.standard.startmenu";
            Items = Enumerable.Empty<IResult>();
        }

        public string UniqueName { get; set; }

        public IEnumerable<IResult> Items { get; set; }

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
            var files = new List<FileInfo>();

            var userStartMenu = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
            var commonStartMenu = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu));

            files.AddRange(userStartMenu.EnumerateFiles(ShortcutFilePattern, SearchOption.AllDirectories));
            files.AddRange(commonStartMenu.EnumerateFiles(ShortcutFilePattern, SearchOption.AllDirectories));

            if (AdditionalSearchFolders != null)
            {
                foreach (var folder in AdditionalSearchFolders)
                {
                    files.AddRange(folder.EnumerateFiles(ShortcutFilePattern, SearchOption.AllDirectories));
                }
            }

            Items = FilterByPreferences(files)
                .Select(x => new StartMenuResult(this, x));
        }

        private List<FileInfo> FilterByPreferences(List<FileInfo> files)
        {
            var filtered = new List<FileInfo>(files);

            if (IgnoreDuplicates)
                filtered = filtered
                    .DistinctBy(x => x.FullName)
                    .ToList();

            if (IgnoreUninstallers)
                filtered.RemoveAll(x => x.Name.ToLower().Contains("uninstal"));

            if (IgnoredItems != null && IgnoredItems.Any())
                filtered.RemoveAll(x => IgnoredItems.Any(ignored => ignored.FullName == x.FullName));

            if (IgnoredFolders != null && IgnoredFolders.Any())
                filtered.RemoveAll(x => IgnoredFolders.Any(ignored => IsAncestor(x, ignored)));

            if (IgnoredFolders != null && IgnoredFolders.Any())
                filtered.RemoveAll(x => IgnoredFolders.Any(ignored => IsAncestor(x, ignored)));

            if (!String.IsNullOrEmpty(IgnorePattern))
                filtered.RemoveAll(x => new Regex(IgnorePattern).IsMatch(x.FullName));
            return filtered;
        }

        private bool IsAncestor(FileSystemInfo item, DirectoryInfo potentialAncestor)
        {
            return item.FullName.StartsWith(potentialAncestor.FullName);
        }

        [ProviderPreference]
        public bool IgnoreUninstallers { get; set; }

        [ProviderPreference]
        public bool IgnoreDuplicates { get; set; }

        [ProviderPreference]
        public string IgnorePattern { get; set; }

        [ProviderPreference]
        public List<DirectoryInfo> AdditionalSearchFolders { get; set; }

        [ProviderPreference]
        public List<FileInfo> IgnoredItems { get; set; }

        [ProviderPreference]
        public List<DirectoryInfo> IgnoredFolders { get; set; }
    }
}
