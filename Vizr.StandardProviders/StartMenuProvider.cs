using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vizr.API;
using Vizr.StandardProviders.Extensions;

namespace Vizr.StandardProviders
{
    public class StartMenuProvider : API.IResultProvider
    {
        private static string ShortcutFilePattern = "*.lnk";

        private List<FileInfo> _shortcuts = new List<FileInfo>();

        public StartMenuProvider()
        {
            ID = Hash.CreateFrom("vizr.standard.startmenu");
        }

        public Hash ID { get; set; }

        private void Load()
        {
            var files = new List<FileInfo>();

            var userStartMenu = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
            var commonStartMenu = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu));

            files.AddRange(userStartMenu.EnumerateFiles(ShortcutFilePattern, SearchOption.AllDirectories));
            files.AddRange(commonStartMenu.EnumerateFiles(ShortcutFilePattern, SearchOption.AllDirectories));

            _shortcuts = files
                .Where(x => !x.Name.ToLower().Contains("uninstal")) // ignore anything that could be an uninstaller
                .DistinctBy(x => x.FullName) // remove duplicate shortcuts that point to same path
                .ToList();
        }

        public void OnBackgroundStart()
        {
            if (!_shortcuts.Any())
                Load();
        }

        public void OnAppStart()
        {
            if (!_shortcuts.Any())
                Load();
        }

        public void OnAppHide()
        {
        }

        IEnumerable<IResult> IResultProvider.Query(string message)
        {
            return _shortcuts.Select(x => new StartMenuResult(this, x));
        }
    }
}
