using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vizr.API
{
    public class SearchableText
    {
        public int Weight { get; set; }
        public string Text { get; set; }

        public SearchableText(int weight, string text)
        {
            Weight = weight;
            Text = text;
        }
    }
}
