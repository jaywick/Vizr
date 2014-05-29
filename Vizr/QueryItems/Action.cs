using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public class Action : Entry, ILaunchable
    {
        public Action()
        {
            // defaults
            Enabled = true;
            Admin = false;
            Tags = "";
        }

        [XmlAttribute]
        public string Tags { get; set; }

        [XmlAttribute]
        public string Application { get; set; }

        [XmlAttribute]
        public bool Admin { get; set; }

        public override bool Match(string text)
        {
            return Pattern.ToLower().ContainsWordStartingWith(text.ToLower())
                || Pattern.ToLower().StartsWith(text.ToLower())
                || Tags.ToLower().Split(",").Any(t => t.StartsWith(text.ToLower()));
        }

        public void Launch(string originalQuery)
        {
            Launcher.Execute(this.Target, this.Application, this.Admin);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
