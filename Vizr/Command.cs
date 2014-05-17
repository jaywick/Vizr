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
        public string Pattern { get; set; }
        
        [XmlAttribute]
        public string Title { get; set; }

        [XmlAttribute]
        public int HitCount { get; set; }

        [XmlElement]
        public string Application { get; set; }
        
        [XmlElement]
        public string Target { get; set; }
        
        public virtual bool Match(string text)
        {
            bool startsWith = this.Pattern.ToLower().StartsWith(text.ToLower().Trim());
            bool innerWordStartsWith = this.Pattern.Contains(" " + text.ToLower().Trim());

            return startsWith || innerWordStartsWith;
        }

        public virtual void Launch(string originalQuery)
        {
            Launcher.Execute(this.Application, this.Target);
            HitCount++;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
