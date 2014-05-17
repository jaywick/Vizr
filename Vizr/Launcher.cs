using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Vizr
{
    class Launcher
    {
        public static bool Execute(Command command)
        {
            if (command == null)
                return false;

            try
            {
                Process.Start(command.CommandName, command.Arguments);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
