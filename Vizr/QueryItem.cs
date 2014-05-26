using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public class QueryItem
    {
        private string title = "";
        private string pattern = "";

        public QueryItem()
        {
            // defaults
            Enabled = true;
        }

        [XmlAttribute]
        public string Title
        {
            get { return chooseValid(title, pattern); }
            set { title = value; }
        }

        [XmlAttribute]
        public virtual string Pattern
        {
            get { return chooseValid(pattern, title); }
            set { pattern = value; }
        }

        private string chooseValid(string subject, string other)
        {
            if (subject.IsNullOrEmpty() && other.IsNullOrEmpty())
                return "";
            else if (!subject.IsNullOrEmpty())
                return subject;
            else if (!other.IsNullOrEmpty())
                return other;

            return null;
        }

        [XmlAttribute]
        public bool Enabled { get; set; }

        [XmlText]
        public string Target { get; set; }

        public virtual bool Match(string text)
        {
            // primitive matching by default
            return Pattern == text;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
