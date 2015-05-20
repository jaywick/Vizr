using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Vizr.API;
using Vizr.Extensions;

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
            textQuery.Text = "";
            ignoreChanges = false;

            if (StartupOptions.IsBackgroundStart)
            {
                backgroundStart();
                repository.OnBackgroundStart();
            }
            else
            {
                repository.OnAppStart();
            }

            UpdateResults();
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

        private VisualResult SelectedVisualResult
        {
            get { return ((VisualResult)listResults.SelectedItem); }
        }

        private IResult SelectedResult
        {
            get { return SelectedVisualResult.ScoredResult.Result; }
        }

        private void PreviewSelected()
        {
            if (SelectedResult == null)
                return;

            // hide if already visible
            if (previewDisplay.IsVisible)
            {
                previewDisplay.Hide();
                listResults.Show();
                return;
            }

            previewDisplay.Document = SelectedVisualResult.RenderPreview();
            previewDisplay.Show();
            listResults.Hide();

            textQuery.MoveCursorToEnd();
        }

        private void ExecuteSelected()
        {
            if (SelectedResult == null)
                return;

            var result = SelectedResult.Launch();
            repository.History.Add(SelectedResult, textQuery.Text);

            if (!result)
                playSubtleErrorSound();

            Exit();
        }

        private void playSubtleErrorSound()
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void Exit()
        {
            textQuery.Clear();

            if (StartupOptions.IsBackgroundStart)
            {
                this.Hide();
                repository.OnAppHide();
            }
            else
            {
                this.Close();
            }
        }

        private void UpdateResults()
        {
            previewDisplay.Visibility = System.Windows.Visibility.Collapsed;
            listResults.Visibility = System.Windows.Visibility.Visible;

            listResults.ItemsSource = repository.Query(textQuery.Text)
                .Select(x => new VisualResult(x))
                .Take(7);

            listResults.SelectFirst();
        }

        #region Event Handlers


        void Hotkey_Activated()
        {
            this.Show();
            this.Activate();

            repository.OnAppStart();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Exit();
                    break;

                case Key.Tab:
                    PreviewSelected();
                    break;

                case Key.Enter:
                    ExecuteSelected();
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
                UpdateResults();
        }

        private void listResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ExecuteSelected();
        }

        #endregion
    }
}
