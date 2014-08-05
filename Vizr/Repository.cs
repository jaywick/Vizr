using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Vizr
{
    public class Repository
    {
        private List<SourceBase> Sources;

        public Repository()
        {
            Sources = new List<SourceBase>
            {
                new Sources.ActionsSource(),
                new Sources.StartMenuAppsSource(),
                new Sources.FileSystemSearch(),
            };

            Sources.ForEach(s => s.Start());

            Update();
        }

        public void Update()
        {
            Sources.ForEach(s => s.Update());
        }

        public IEnumerable<EntryBase> Query(string text)
        {
            Sources.ForEach(s => s.Query(text));
            
            return Sources.Where(s => s.Enabled)
                          .OrderBy(s => s.Priority)
                          .SelectMany(s => s.Results)
                          .OrderByDescending(r => r.Relevance);
        }
    }
}