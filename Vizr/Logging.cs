using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr
{
    public static class Logging
    {
        public static void WriteLine(string message, params string[] args)
        {
            File.AppendAllLines(API.Workspace.LogFilePath, new[] { String.Format(message, args) });
        }
    }
}
