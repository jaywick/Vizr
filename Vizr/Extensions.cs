using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Vizr.Extensions
{
    static class Extensions
    {
        public static void SelectNext(this ListBox target)
        {
            if (target.SelectedIndex == -1)
                return; // nothing selected

            if (target.SelectedIndex == target.Items.Count - 1)
                target.SelectedIndex = 0; // loop back to top
            else
                target.SelectedIndex++; // go to next
        }

        public static void SelectPrevious(this ListBox target)
        {
            if (target.SelectedIndex == -1)
                return; // nothing selected

            if (target.SelectedIndex == 0)
                target.SelectedIndex = target.Items.Count - 1; // loop back to top
            else
                target.SelectedIndex--; // go to next
        }

        public static void SelectFirst(this ListBox target)
        {
            if (target.Items.Count > 0)
                target.SelectedIndex = 0;
        }

        public static void SelectLast(this ListBox target)
        {
            if (target.Items.Count > 0)
                target.SelectedIndex = target.Items.Count - 1;
        }

        public static void MoveCursorToEnd(this TextBox target)
        {
            target.Select(target.Text.Length, 0);
        }

        public static bool IsNullOrEmpty(this string target)
        {
            return target == null || target == "";
        }

        public static IEnumerable<string> Split(this string target, params string[] separators)
        {
            return target.Split(separators, StringSplitOptions.None);
        }

        // match start of each word
        public static bool ContainsPartialsOf(this string target, string value)
        {
            string[] wordSplitters = { " ", ".", "-", "_" };
            var targetWords = target.Split(wordSplitters, StringSplitOptions.RemoveEmptyEntries).ToList();
            var valueWords = value.Split(wordSplitters, StringSplitOptions.RemoveEmptyEntries).ToList();

            return targetWords.Exists(t => valueWords.Exists(v => t.StartsWith(v)));
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static void Show(this Control target)
        {
            target.Visibility = System.Windows.Visibility.Visible;
        }

        public static void Hide(this Control target)
        {
            target.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
