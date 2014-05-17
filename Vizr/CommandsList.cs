using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public class CommandsList
    {
        private string filePath;

        public CommandsList()
        {
            Items = new List<Command>();
            MetaItems = new List<Command>();
            addMetaCommands();
        }

        public CommandsList(string filePath)
            : this()
        {
            this.filePath = filePath;
        }

        private void addMetaCommands()
        {
            // about app
            MetaItems.Add(new Command()
            {
                Pattern = "About",
                Title = "About this app",
                Target = "https://github.com/jaywick/vizr",
                HitCount = 1
            });

            // edit commands
            MetaItems.Add(new Command()
            {
                Pattern = "Edit",
                Title = "Edit commands",
                Target = filePath,
                HitCount = 1
            });
        }

        [XmlArray]
        [XmlArrayItem(ElementName = "Command", Type = typeof(Command))]
        [XmlArrayItem(ElementName = "Request", Type = typeof(Request))]
        public List<Command> Items { get; set; }

        [XmlIgnore]
        public List<Command> MetaItems { get; set; }

        [XmlIgnore]
        public List<Command> AllItems
        {
            get
            {
                return Items.Union(MetaItems).ToList();
            }
        }
    }
}
