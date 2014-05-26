using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    [XmlType("VizrPackage")]
    public class Package
    {
        public Package()
        {
            Version = "0.1";
            Items = new List<Entry>();
            Enabled = true;
            Priority = 0;
        }

        public Package(string name)
            : this()
        {
            this.Name = name;
        }

        [XmlAttribute]
        public string Version { get; set; }

        [XmlAttribute]
        public bool Enabled { get; set; }

        [XmlAttribute]
        public int Priority { get; set; }

        [XmlArray]
        [XmlArrayItem(ElementName = "Action", Type = typeof(Action))]
        [XmlArrayItem(ElementName = "Request", Type = typeof(Request))]
        public virtual List<Entry> Items { get; set; }

        [XmlIgnore]
        public string Name { get; set; }
    }
}
