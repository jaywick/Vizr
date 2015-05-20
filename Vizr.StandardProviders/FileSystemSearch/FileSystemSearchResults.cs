using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizr.API;
using Vizr.StandardProviders.Extensions;

namespace Vizr.StandardProviders
{
    public abstract class FileSystemSearchResult : IResult
    {
        public static FileSystemSearchResult CreateFrom(IResultProvider provider, FileSystemInfo fileSystemInfo)
        {
            if (fileSystemInfo.IsDirectory())
                return new DirectorySearchResult(provider, (DirectoryInfo)fileSystemInfo);

            if (fileSystemInfo.Extension == ".lnk")
                return new ShortcutSearchResult(provider, (FileInfo)fileSystemInfo);

            return new FileSearchResult(provider, (FileInfo)fileSystemInfo);
        }

        public enum Types { Shortcut, File, Folder }

        public abstract Types Type { get; }

        public abstract string Name { get; }

        public abstract string Path { get; }

        public Hash ID { get; set; }

        public string Title { get; set; }

        public abstract bool Launch();

        public abstract IEnumerable<SearchableText> SearchableText { get; }

        public IResultProvider Provider { get; set; }

        public IPreview Preview
        {
            get { return new FileSystemSearchPreview(this); }
            set { }
        }

        public void Edit()
        {
        }

        public void Delete()
        {
        }
    }

    public class FileSearchResult : FileSystemSearchResult
    {
        private FileInfo _fileInfo;

        public FileSearchResult(IResultProvider provider, FileInfo fileInfo)
        {
            _fileInfo = fileInfo;

            Provider = provider;
            ID = Hash.CreateFrom(_fileInfo.FullName);
            Title = fileInfo.GetNameWithoutExtension();
        }

        public override IEnumerable<SearchableText> SearchableText
        {
            get
            {
                yield return new SearchableText(5, _fileInfo.Name);
                yield return new SearchableText(1, _fileInfo.FullName);
            }
        }

        public override bool Launch()
        {
            Process.Start(_fileInfo.FullName);
            return true;
        }

        public override FileSystemSearchResult.Types Type
        {
            get { return Types.File; }
        }

        public override string Path
        {
            get { return _fileInfo.FullName; }
        }

        public override string Name
        {
            get { return _fileInfo.Name; }
        }
    }

    public class ShortcutSearchResult : FileSystemSearchResult
    {
        private FileInfo _fileInfo;

        public ShortcutSearchResult(IResultProvider provider, FileInfo fileInfo)
        {
            _fileInfo = fileInfo;

            Provider = provider;
            ID = Hash.CreateFrom(_fileInfo.FullName);
            Title = fileInfo.GetNameWithoutExtension();
        }

        public override IEnumerable<SearchableText> SearchableText
        {
            get
            {
                yield return new SearchableText(5, _fileInfo.Name);
                yield return new SearchableText(1, _fileInfo.FullName);
            }
        }

        public override bool Launch()
        {
            Process.Start(_fileInfo.FullName);
            return true;
        }

        public override FileSystemSearchResult.Types Type
        {
            get { return Types.Shortcut; }
        }

        public override string Path
        {
            get { return _fileInfo.FullName; }
        }

        public override string Name
        {
            get { return _fileInfo.Name; }
        }
    }

    public class DirectorySearchResult : FileSystemSearchResult
    {
        private DirectoryInfo _directoryInfo;

        public DirectorySearchResult(IResultProvider provider, DirectoryInfo directoryInfo)
        {
            _directoryInfo = directoryInfo;

            Provider = provider;
            ID = Hash.CreateFrom(_directoryInfo.FullName);
            Title = _directoryInfo.Name + "/";
        }

        public override IEnumerable<SearchableText> SearchableText
        {
            get
            {
                yield return new SearchableText(5, _directoryInfo.Name);
                yield return new SearchableText(1, _directoryInfo.FullName);
            }
        }

        public override bool Launch()
        {
            Process.Start(_directoryInfo.FullName);
            return true;
        }

        public override FileSystemSearchResult.Types Type
        {
            get { return Types.Folder; }
        }

        public override string Path
        {
            get { return _directoryInfo.FullName; }
        }

        public override string Name
        {
            get { return _directoryInfo.Name; }
        }
    }
}
