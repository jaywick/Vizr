using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Vizr;

namespace Vizr
{
    public class Action : EntryBase
    {
        public Action()
        {
            // defaults
            this.IsEnabled = true;
            this.Title = "";
            this.Tags = "";
            this.Application = "";
            this.Target = "";
            this.IsAdminRequired = false;
        }

        public string Tags { get; set; }

        public string Application { get; set; }

        public bool IsAdminRequired { get; set; }

        public string Target { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
