using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Vizr.API;

namespace Vizr
{
    public class VisualResult
    {
        public ScoredResult ScoredResult { get; set; }
        public SolidColorBrush Brush { get; set; }
        public string IconDisplay { get; set; }

        public string Title
        {
            get { return ScoredResult.Result.Title; }
        }

        public VisualResult(ScoredResult scoredResult)
        {
            ScoredResult = scoredResult;
            
            var hue = Hash8bit(scoredResult.Result.Provider.UniqueName);
            Brush = CreateBrushFromHSV(hue, .663 * 360, .663 * 360);

            IconDisplay = FontAwesome.Icons[scoredResult.Result.Provider.Icon];
        }

        public FlowDocument RenderPreview()
        {
            var previewer = ScoredResult.Result.Preview;

            if (previewer == null)
                previewer = new DefaultPreview(this.ScoredResult.Result);

            FlowDocument document;
            
            try
            {
                document = (FlowDocument)XamlReader.Parse(previewer.Document);
            }
            catch (Exception ex)
            {
                previewer = new RenderIssuePreview(this.ScoredResult.Result, ex);
                document = (FlowDocument)XamlReader.Parse(previewer.Document);
            }

            document.DataContext = previewer;

            return document;
        }

        private static byte Hash8bit(string message)
        {
            byte current = 0xCC; // seed value

            foreach (char character in message)
                current ^= (byte)character;

            return current;
        }

        private static SolidColorBrush CreateBrushFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            var v = (byte)Convert.ToInt32(value);
            var p = (byte)Convert.ToInt32(value * (1 - saturation));
            var q = (byte)Convert.ToInt32(value * (1 - f * saturation));
            var t = (byte)Convert.ToInt32(value * (1 - (1 - f) * saturation));

            Color color;
            if (hi == 0)
                color = Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                color = Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                color = Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                color = Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                color = Color.FromArgb(255, t, p, v);
            else
                color = Color.FromArgb(255, v, p, q);

            return new SolidColorBrush(color);
        }
    }
}
