using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Vizr
{
    class Launcher
    {
        public static bool Execute(string target, string application = null)
        {
            try
            {
                if (application.IsNullOrEmpty())
                    Process.Start(target);
                else
                    Process.Start(application, target);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
