using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    [XmlRoot("Items")]
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
                Name = "About",
                Subtitle = "About this app",
                CommandName = "explorer.exe",
                Arguments = "https://github.com/jaywick/vizr",
                HitCount = 1
            });

            // edit commands
            MetaItems.Add(new Command()
            {
                Name = "Edit",
                Subtitle = "Edit commands",
                CommandName = "explorer.exe",
                Arguments = filePath,
                HitCount = 1
            });
        }

        [XmlElement("Command")]
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
