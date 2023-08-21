using System;
using System.Collections.Generic;
using System.Linq;

namespace Moss
{
    public static class DictionaryExtensions
    {
        public static void AddElementToListValue<TKey, TElement>(this Dictionary<TKey, List<TElement>> dictionary,
            TKey key, TElement element)
        {
            if (dictionary.TryGetValue(key, out var value))
                value.Add(element);
            else
                dictionary.Add(key, new List<TElement> { element });
        }

        public static void AddElementsToListValue<TKey, TElement>(this Dictionary<TKey, List<TElement>> dictionary,
            TKey key, IEnumerable<TElement> elements)
        {
            if (dictionary.TryGetValue(key, out var value))
                value.AddRange(elements);
            else
                dictionary.Add(key, new List<TElement>(elements));
        }

        public static void Foreach<TKey, TElement>(this Dictionary<TKey, TElement> dictionary,
            Action<TKey, TElement> action)
        {
            foreach (var (key, value) in dictionary.Select(pair => (pair.Key, pair.Value)))
            {
                action?.Invoke(key, value);
            }
        }

        public static void Foreach<TKey1, TKey2, TElement>(
            this Dictionary<TKey1, Dictionary<TKey2, TElement>> dictionary,
            Action<TKey1, TKey2, TElement> action)
        {
            foreach (var (key1, key2AndValue) in dictionary.Select(pair => (pair.Key, pair.Value)))
            {
                foreach (var (key2, value) in key2AndValue.Select(pair => (pair.Key, pair.Value)))
                {
                    action?.Invoke(key1, key2, value);
                }
            }
        }
    }
}