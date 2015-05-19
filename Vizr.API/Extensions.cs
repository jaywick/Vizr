using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vizr.API.Extensions
{
    public static class Extensions
    {
        /// <summary>
        /// Get all inner exceptions as IEnumerable<System.Exception> including itself
        /// </summary>
        public static IEnumerable<Exception> GetDescendantExceptionsAndSelf(this Exception target)
        {
            yield return target;

            var current = target.InnerException;

            while (current != null)
            {
                yield return current;
                current = current.InnerException;
            }
        }
    }
}
