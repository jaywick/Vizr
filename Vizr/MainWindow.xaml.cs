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
        private bool processTextChange = false;

        public MainWindow()
        {
            InitializeComponent();
            textQuery.Text = "";

            processTextChange = true;
            textQuery.Focus();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    this.Close();
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

        private void textQuery_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!processTextChange) return;

            var message = textQuery.Text;

            listResults.Items.Clear();
            if (message == "d")
            { 
                listResults.Items.Add("hai");
                listResults.Items.Add("2");
            }

            if (listResults.Items.Count != 0)
                listResults.SelectedIndex = 0;
        }
    }
}
