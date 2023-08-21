using System;
using System.Collections.Generic;

namespace Moss
{
    public static class IEnumerableExtensions
    {
        public static void Foreach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var t in self)
            {
                action?.Invoke(t);
            }
        }
    }
}