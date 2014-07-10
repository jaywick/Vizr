using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Vizr
{
    public abstract class SourceBase
    {
        public string Version { get; protected set; }
        public bool Enabled { get; protected set; }
        public int Priority { get; protected set; }
        public string Name { get; protected set; }
        public IEnumerable<EntryBase> Results { get; protected set; }
        public IEntryHandler Handler { get; protected set; }

        public abstract void Update();
        public abstract void Query(string text);

        protected SourceBase()
        {
            Enabled = true;
            Priority = 0;
        }

        public virtual void Start()
        {
            Update();
        }
    }
}
