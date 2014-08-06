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
        private Repository repository = new Repository();
        private HotKey hotkey;
        private bool ignoreChanges;

        public MainWindow()
        {
            ignoreChanges = true;
            InitializeComponent();
            ignoreChanges = false;

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

        private EntryBase selectedEntry
        {
            get { return listResults.SelectedItem as EntryBase; }
        }

        private void autoCompleteSelected()
        {
            if (selectedEntry == null)
                return;

            textQuery.Text = selectedEntry.HandlePreview();
            textQuery.MoveCursorToEnd();
        }

        private void executeSelected()
        {
            if (selectedEntry == null)
                return;

            var result = selectedEntry.HandleExecute();
            if (result == ExecutionResult.Failed)
                playSubtleErrorSound();

            exit();
        }

        private void playSubtleErrorSound()
        {
            System.Media.SystemSounds.Beep.Play();
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
            listResults.ItemsSource = repository.QueryAll(textQuery.Text).Take(6);
            listResults.SelectFirst();
        }

        #region Event Handlers


        void Hotkey_Activated()
        {
            this.Show();
            this.Activate();
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
            repository.Update();
            textQuery.Focus();
        }

        private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
                listResults.SelectPrevious();
            else
                listResults.SelectNext();
        }

        private void textQuery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ignoreChanges)
                updateResults();
        }

        private void listResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            executeSelected();
        }

        #endregion
    }
}
