using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr.Models
{
    [XmlType("filesystemsearch")]
    public class FileSystemSearchRulesList
    {
        public FileSystemSearchRulesList()
        {
            this.Items = new List<FileSystemSearchRule>();
        }

        [XmlArray("items")]
        public virtual List<FileSystemSearchRule> Items { get; set; }
    }

    public enum FileSearchTargets
    {
        [XmlEnum(Name = "files")]
        Files = 1,

        [XmlEnum(Name = "folders")]
        Folders = 2,

        [XmlEnum(Name = "both")]
        BothFilesAndFolders = Files + Folders,
    }

    [XmlType("rule")]
    public class FileSystemSearchRule
    {
        public FileSystemSearchRule()
        {
            Pattern = "*";
            Path = "";
            IsRecursive = false;
            SearchType = FileSearchTargets.BothFilesAndFolders;
        }

        [XmlAttribute("pattern")]
        public string Pattern { get; set; }

        [XmlAttribute("recurse")]
        public bool IsRecursive { get; set; }

        [XmlAttribute("searchtype")]
        public FileSearchTargets SearchType { get; set; }

        [XmlAttribute("includeThis")]
        public bool IncludeRuleFolder { get; set; }

        [XmlText]
        public string Path { get; set; }
    }
}
