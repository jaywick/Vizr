using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr.Models
{
    [XmlType("actions")]
    public class ActionsList : IEntryModelList
    {
        public ActionsList()
        {
            this.Items = new List<Action>();
        }

        [XmlArray("items")]
        public virtual List<Action> Items { get; set; }

        public List<TEntry> GetAllEntries<TEntry>() where TEntry : EntryBase
        {
            return Items.Select(a => a.ToEntry<TEntry>()).ToList();
        }
    }

    [XmlType("action")]
    public class Action : IEntryModel
    {
        public Action()
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

        public TEntry ToEntry<TEntry>() where TEntry : EntryBase
        {
            return new ActionEntry()
            {
                Application = this.Application,
                IsAdminRequired = this.IsAdminRequired,
                IsEnabled = this.IsEnabled,
                Tags = this.Tags,
                Target = this.Target,
                Title = this.Title,
            } as TEntry;
        }
    }
}
