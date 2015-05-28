using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WinForms = System.Windows.Forms;

namespace Vizr
{
    public class SystemTrayIcon
    {
        private MainWindow mainWindow;
        private WinForms.NotifyIcon notifyIcon;

        public SystemTrayIcon(MainWindow parent)
        {
            mainWindow = parent;

            notifyIcon = new WinForms.NotifyIcon();
            notifyIcon.MouseUp += Notify_Click;
            notifyIcon.Icon = CreateIcon(mainWindow.Icon);
            notifyIcon.Visible = true;
            notifyIcon.ContextMenu = CreateSystemTrayContextMenu();
        }

        private WinForms.ContextMenu CreateSystemTrayContextMenu()
        {
            var contextMenu = new WinForms.ContextMenu();
            contextMenu.MenuItems.Add(new WinForms.MenuItem("Launch Vizr", Notify_LaunchVizr));
            contextMenu.MenuItems.Add(new WinForms.MenuItem("Reload providers", Notify_Reload));
            contextMenu.MenuItems.Add(new WinForms.MenuItem("-"));
            contextMenu.MenuItems.Add(new WinForms.MenuItem("Exit", Notify_Exit));

            return contextMenu;
        }

        private void Notify_Click(object sender, WinForms.MouseEventArgs e)
        {
            if (e.Button != WinForms.MouseButtons.Left)
                return;

            mainWindow.ShowApp();
        }

        private void Notify_LaunchVizr(object sender, EventArgs e)
        {
            mainWindow.ShowApp();
        }

        private void Notify_Reload(object sender, EventArgs e)
        {
            mainWindow.ReloadProviders();
        }

        private void Notify_Exit(object sender, EventArgs e)
        {
            mainWindow.ForceExit();
        }

        // thanks to @StanislavKniazev http://stackoverflow.com/a/430909/80428
        private System.Drawing.Icon CreateIcon(ImageSource source)
        {
            return System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().ManifestModule.Name);
        }

        public void Remove()
        {
            notifyIcon.Visible = false;
        }
    }
}
