using System;
using System.Collections.Generic;

namespace Moss
{
    public static class ListExtensions
    {
        public static void RemoveElements<TElement>(this List<TElement> list, List<TElement> elements)
        {
            foreach (var element in elements)
                list.Remove(element);
        }

        public static TElement GetRandom<TElement>(this List<TElement> list)
        {
            var random = new Random();
            var i = random.Next(0, list.Count);
            return list[i];
        }
    }
}