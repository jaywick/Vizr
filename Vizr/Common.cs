using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Vizr
{
    static class Common
    {
        static Common()
        {
            CommandsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "default.xml");
        }

        public static string CommandsFile { get; private set; }
    }
}
