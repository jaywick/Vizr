using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vizr;

namespace Vizr
{
    public partial class MainWindow : Window
    {
        private bool processTextChange = false; // flag to prevent textchange events if false
        HotKey hotkey;
        private Repository commands = new Repository();

        public MainWindow()
        {
            InitializeComponent();

            processTextChange = true;
            textQuery.Text = "";

            if (StartupOptions.IsBackgroundStart)
                backgroundStart();
        }

        ~MainWindow()
        {
            if (hotkey != null)
                hotkey.Dispose();
        }

        private void backgroundStart()
        {
            this.Hide();
            hotkey = new HotKey(Key.Space, KeyModifier.Alt);
            hotkey.Activated += Hotkey_Activated;
        }

        void Hotkey_Activated()
        {
            this.Show();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    exit();
                    break;

                case Key.Tab:
                    autoCompleteSelected();
                    break;

                case Key.Enter:
                    executeSelected();
                    break;

                case Key.Up:
                    listResults.SelectPrevious();
                    break;

                case Key.Down:
                    listResults.SelectNext();
                    break;

                case Key.PageUp:
                    listResults.SelectFirst();
                    break;

                case Key.PageDown:
                    listResults.SelectLast();
                    break;

                default:
                    return;
            }

            e.Handled = true;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            commands.Load();
            textQuery.Focus();
        }

        private void textQuery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!processTextChange) return;
            updateResults();
        }

        private void listResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            executeSelected();
        }

        private void autoCompleteSelected()
        {
            var item = (listResults.SelectedItem as Command);
            
            if (item == null || item is Request)
                return;

            textQuery.Text = item.Pattern;
            textQuery.MoveCursorToEnd();
        }

        private void executeSelected()
        {
            var item = (listResults.SelectedItem as Command);

            if (item != null)
            {
                item.Launch(textQuery.Text);
                exit();
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void exit()
        {
            textQuery.Clear();

            if (StartupOptions.IsBackgroundStart)
                this.Hide();
            else
                this.Close();
        }

        private void updateResults()
        {
            listResults.Items.Clear();

            foreach (var item in commands.Query(textQuery.Text).Take(6))
            {
                listResults.Items.Add(item);
            }

            listResults.SelectFirst();
        }
    }
}
