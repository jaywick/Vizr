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

        public StartMenuProvider()
        {
            ID = Hash.CreateFrom("vizr.standard.startmenu");
            Items = Enumerable.Empty<IResult>();
        }

        public Hash ID { get; set; }

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

        private void Load()
        {
            var files = new List<FileInfo>();

            var userStartMenu = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu));
            var commonStartMenu = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu));

            files.AddRange(userStartMenu.EnumerateFiles(ShortcutFilePattern, SearchOption.AllDirectories));
            files.AddRange(commonStartMenu.EnumerateFiles(ShortcutFilePattern, SearchOption.AllDirectories));

            Items = files
                .Where(x => !x.Name.ToLower().Contains("uninstal")) // ignore anything that could be an uninstaller
                .DistinctBy(x => x.FullName) // remove duplicate shortcuts that point to same path
                .Select(x => new StartMenuResult(this, x));
        }
    }
}
