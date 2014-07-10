using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    [XmlType("actions")]
    public class UserActionListXml : IEntryModelList
    {
        public UserActionListXml()
        {
        }

        public UserActionListXml(List<UserActionXml> items)
        {
            this.Items = items;
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
            : this("")
        {
        }

        public UserActionXml(string title, string tags = "", string application = "", bool isAdminRequired = false, string target = "")
        {
            Title = title;
            Tags = tags;
            Application = application;
            IsAdminRequired = isAdminRequired;
            Target = target;
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
