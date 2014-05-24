using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace Vizr
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // allow only single instance
            if (Process.GetProcesses().Where(p => p.ProcessName == Process.GetCurrentProcess().ProcessName).Count() > 1)
                this.Shutdown();

            base.OnStartup(e);
        }
    }
}
