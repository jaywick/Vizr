using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.StandardProviders.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keySelector)
        {
            return items.GroupBy(keySelector)
                        .Select(x => x.First());
        }

        public static string GetNameWithoutExtension(this FileInfo target)
        {
            if (target.Extension == "")
                return target.Name;

            return target.Name.Substring(0, target.Name.Length - target.Extension.Length);
        }
    }
}
