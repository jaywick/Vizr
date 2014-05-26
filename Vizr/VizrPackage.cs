using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public class VizrPackage
    {
        public VizrPackage()
        {
            Version = "0.1";
            Items = new List<Command>();
        }

        public VizrPackage(string name)
            : this()
        {
            this.Name = name;
        }

        [XmlAttribute]
        public string Version { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Command", Type = typeof(Command))]
        [XmlArrayItem(ElementName = "Request", Type = typeof(Request))]
        public List<Command> Items { get; set; }

        [XmlIgnore]
        public virtual string Name { get; set; }

        public void Tidy()
        {
            foreach (var item in Items)
            {
                if (item is Command)
                {
                    if (item.Pattern == "") item.Pattern = item.Title;
                    if (item.Title == "") item.Title = item.Pattern;
                }
            }
        }
    }
}
