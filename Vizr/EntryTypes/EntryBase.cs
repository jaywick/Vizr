using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public class EntryBase
    {
        public EntryBase()
        {
            // defaults
            IsEnabled = true;
        }

        public SourceBase ParentSource { get; set; }
        public int Relevance { get; set; }
        public string Title { get; set; }
        public bool IsEnabled { get; set; }

        public string HandlePreview()
        {
            return ParentSource.Handler.Preview(this);
        }

        public ExecutionResult HandleExecute()
        {
            return ParentSource.Handler.Execute(this);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
