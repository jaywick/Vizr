using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion;

            if (versionInfo == "DEBUG")
                return versionInfo + "-" + new FileInfo(assembly.Location).LastWriteTime.ToString("yyyyMMdd_HHmmss");

            return versionInfo;
        }

    }
}
