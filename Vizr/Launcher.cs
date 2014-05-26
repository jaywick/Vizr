using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Vizr
{
    class Launcher
    {
        public static bool Execute(string target, string application = null, bool runAsAdmin = false)
        {
            try
            {
                var info = new ProcessStartInfo(target);

                if (!application.IsNullOrEmpty())
                {
                    info.FileName = application;
                    info.Arguments = target;
                }

                if (runAsAdmin)
                    info.Verb = "runas";

                Process.Start(info);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
