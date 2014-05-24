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
        public string Title { get; set; }
        
        [XmlAttribute]
        public string Pattern { get; set; }
        
        [XmlAttribute]
        public string Application { get; set; }
        
        [XmlText]
        public string Target { get; set; }
        
        public virtual bool Match(string text)
        {
            return Pattern.ToLower().ContainsWordStartingWith(text.ToLower());
        }

        public virtual void Launch(string originalQuery)
        {
            Launcher.Execute(this.Target, this.Application);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
