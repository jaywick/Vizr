using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Vizr
{
    class Launcher
    {
        public static bool Execute(string command, string argument = "")
        {
            if (command == null)
                return false;

            try
            {
                if (argument == "")
                    Process.Start(command);
                else
                    Process.Start(command, argument);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
