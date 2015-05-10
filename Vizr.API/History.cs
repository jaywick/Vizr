using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vizr.API;

namespace Vizr
{
    public class History
    {
        private static readonly string _historyFilePath = System.IO.Path.Combine(Workspace.Root.FullName, "history.json");

        private List<HistoryItem> _items = new List<HistoryItem>();
        public IEnumerable<Hash> Items { get { return _items.Select(x => Hash.Parse(x.ID)); } }

        public History()
        {
            ReadFile();
        }

        public void Add(IResult result, string query)
        {
            _items.Add(new HistoryItem(result.ID, query));
            SaveFile();
        }

        private void ReadFile()
        {
            if (!File.Exists(_historyFilePath))
                SaveFile();

            var historyData = File.ReadAllText(_historyFilePath);
            _items = JsonConvert.DeserializeObject<List<HistoryItem>>(historyData) ?? new List<HistoryItem>();
        }

        private void SaveFile()
        {
            File.WriteAllText(_historyFilePath, JsonConvert.SerializeObject(_items, Formatting.Indented));
        }

        private class HistoryItem
        {
            public string ID;
            public string Query;
            public DateTime Date;

            public HistoryItem()
            {
            }

            public HistoryItem(Hash id, string query)
            {
                this.ID = id.ToString();
                this.Query = query;
                this.Date = DateTime.UtcNow;
            }
        }
    }
}
