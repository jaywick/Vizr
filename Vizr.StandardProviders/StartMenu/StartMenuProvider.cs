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
    public class StartMenuProvider : API.IResultProvider, API.IHasPreferences<StartMenuProviderPrefs>
    {
        private static string ShortcutFilePattern = "*.lnk";

        public StartMenuProvider()
        {
            UniqueName = "vizr.standard.startmenu";
            Items = Enumerable.Empty<IResult>();
            Icon = "play";
        }

        public string UniqueName { get; set; }

        public string Icon { get; set; }

        public IEnumerable<IResult> Items { get; set; }

        public StartMenuProviderPrefs Preferences { get; set; }

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

            if (Preferences.AdditionalSearchFolders != null)
            {
                foreach (var folder in Preferences.AdditionalSearchFolders)
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

            if (Preferences.IgnoreDuplicates)
                filtered = filtered
                    .DistinctBy(x => x.FullName)
                    .ToList();

            if (Preferences.IgnoreUninstallers)
                filtered.RemoveAll(x => x.Name.ToLower().Contains("uninstal"));

            if (Preferences.IgnoredItems != null && Preferences.IgnoredItems.Any())
                filtered.RemoveAll(x => Preferences.IgnoredItems.Any(ignored => ignored.FullName == x.FullName));

            if (Preferences.IgnoredFolders != null && Preferences.IgnoredFolders.Any())
                filtered.RemoveAll(x => Preferences.IgnoredFolders.Any(ignored => IsAncestor(x, ignored)));

            if (Preferences.IgnoredFolders != null && Preferences.IgnoredFolders.Any())
                filtered.RemoveAll(x => Preferences.IgnoredFolders.Any(ignored => IsAncestor(x, ignored)));

            if (!String.IsNullOrEmpty(Preferences.IgnorePattern))
                filtered.RemoveAll(x => new Regex(Preferences.IgnorePattern).IsMatch(x.FullName));
            return filtered;
        }

        private bool IsAncestor(FileSystemInfo item, DirectoryInfo potentialAncestor)
        {
            return item.FullName.StartsWith(potentialAncestor.FullName);
        }
    }
}
