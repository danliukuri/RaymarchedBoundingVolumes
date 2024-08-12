using System;
using System.Collections.Generic;
using System.Linq;

namespace RaymarchedBoundingVolumes.Utilities.Extensions
{
    public static class SequenceExtensions
    {
        public static int FirstIndex<T>(this IEnumerable<T> source) => default;
        public static int LastIndex<T> (this IEnumerable<T> source) => source.Count() - 1;

        public static int NextIndex<T>    (this IEnumerable<T> source, int currentIndex) => currentIndex + 1;
        public static int PreviousIndex<T>(this IEnumerable<T> source, int currentIndex) => currentIndex - 1;

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
            Func<TSource, int, TKey> keySelector, Func<TSource, TElement> elementSelector) => source
            .ToDictionary(keySelector.Invoke, (item, index) => elementSelector.Invoke(item));

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, int, TElement> elementSelector) => source
            .ToDictionary((item, index) => keySelector.Invoke(item), elementSelector.Invoke);

        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source,
            Func<TSource, int, TKey> keySelector, Func<TSource, int, TElement> elementSelector) => source
            .Select((item, index) => (Value: item, Index: index))
            .ToDictionary(keySelector.Invoke, elementSelector.Invoke);

        public static TResult Invoke<TSource1, TSource2, TResult>(this Func<TSource1, TSource2, TResult> source,
            (TSource1, TSource2) args) => source.Invoke(args.Item1, args.Item2);
    }
}