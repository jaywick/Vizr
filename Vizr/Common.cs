using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr
{
    internal class Common
    {
        public static string GetVersionInfo()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            return FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;
        }

    }
}
