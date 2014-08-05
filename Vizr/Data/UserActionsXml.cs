using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr.Models
{
    [XmlType("actions")]
    public class UserActionListXml : IEntryModelList
    {
        public UserActionListXml()
        {
            this.Items = new List<UserActionXml>();
        }

        [XmlArray("items")]
        public virtual List<UserActionXml> Items { get; set; }

        public List<EntryBase> GetAllEntries()
        {
            return Items.Select(a => a.ToEntry()).ToList();
        }
    }

    [XmlType("action")]
    public class UserActionXml : IEntryModel
    {
        public UserActionXml()
        {
            Title = "";
            Tags = "";
            Application = "";
            IsAdminRequired = false;
            Target = "";
        }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("tags")]
        public string Tags { get; set; }

        [XmlAttribute("application")]
        public string Application { get; set; }

        [XmlAttribute("runElevated")]
        public bool IsAdminRequired { get; set; }

        [XmlAttribute("enabled")]
        public bool IsEnabled { get; set; }

        [XmlText]
        public string Target { get; set; }

        public EntryBase ToEntry()
        {
            return new Action()
            {
                Application = this.Application,
                IsAdminRequired = this.IsAdminRequired,
                IsEnabled = this.IsEnabled,
                Tags = this.Tags,
                Target = this.Target,
                Title = this.Title,
            };
        }
    }
}
