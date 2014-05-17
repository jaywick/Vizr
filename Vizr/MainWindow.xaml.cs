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
        
        private Commands commands = new Commands();

        public MainWindow()
        {
            InitializeComponent();

            processTextChange = true;
            textQuery.Text = "";
            textQuery.Focus();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
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

                default:
                    break;
            }
        }

        private void executeSelected()
        {
            var item = (listResults.SelectedItem as Command);

            if (item != null)
            {
                item.Launch(textQuery.Text);
                this.Close();
            }
            else
            {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        private void textQuery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!processTextChange) return;
            updateResults();
        }

        private void updateResults()
        {
            listResults.Items.Clear();

            foreach (var item in commands.Query(textQuery.Text).Take(5))
            {
                listResults.Items.Add(item);
            }

            listResults.SelectFirst();
        }
    }
}
