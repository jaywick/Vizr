using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Vizr
{
    public class Request : Entry, ILaunchable
    {
        string modifiedArgument = "";
        string extract = "";

        [XmlAttribute]
        public string Application { get; set; }

        [XmlAttribute]
        public bool Admin { get; set; }

        public override bool Match(string text)
        {
            var query = Regex.Match(text, this.Pattern, RegexOptions.IgnoreCase);

            if (query.Success) 
            {
                extract = query.Groups[1].Value;
                modifiedArgument = String.Format(this.Target, extract);
            }

            return query.Success;
        }

        public void Launch(string originalQuery)
        {
            Launcher.Execute(modifiedArgument, this.Application);
        }

        public override string ToString()
        {
            return String.Format(this.Title, extract);
        }
    }
}