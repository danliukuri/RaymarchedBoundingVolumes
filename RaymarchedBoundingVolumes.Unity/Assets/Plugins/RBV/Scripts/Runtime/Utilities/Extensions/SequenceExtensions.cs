using System;
using System.Collections.Generic;
using System.Linq;

namespace RBV.Utilities.Extensions
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

        public static Array CastToArray<T>(this IEnumerable<T> source, Type elementType)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (elementType == null)
                throw new ArgumentNullException(nameof(elementType));
            if (!typeof(T).IsAssignableFrom(elementType))
                throw new ArgumentException($"Cannot cast from type {typeof(T)} to {elementType}.",
                    nameof(elementType));

            List<T> list = source.ToList();

            var array = Array.CreateInstance(elementType, list.Count);

            for (var i = 0; i < list.Count; i++)
                array.SetValue(Convert.ChangeType(list[i], elementType), i);

            return array;
        }
    }
}