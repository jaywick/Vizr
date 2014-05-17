using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public class Command
    {
        [XmlAttribute]
        public string Name { get; set; }
        
        [XmlAttribute]
        public string Subtitle { get; set; }

        [XmlAttribute]
        public int HitCount { get; set; }

        [XmlElement]
        public string CommandName { get; set; }
        
        [XmlElement]
        public string Arguments { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
