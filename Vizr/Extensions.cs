using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Vizr
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

        public static bool IsNullOrEmpty(this string target)
        {
            return target == null || target == "";
        }
    }
}
