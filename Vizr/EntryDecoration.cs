using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Media;

namespace Vizr
{
    public class EntryDecoration
    {
        public Brush Foreground { get; set; }

        public EntryDecoration(Brush foreground)
        {
            Foreground = foreground;
        }

        public EntryDecoration(string foreground)
        {
            Foreground = hexToBrush(foreground);
        }

        private static Brush hexToBrush(string hexCode)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hexCode));
        }
    }
}
