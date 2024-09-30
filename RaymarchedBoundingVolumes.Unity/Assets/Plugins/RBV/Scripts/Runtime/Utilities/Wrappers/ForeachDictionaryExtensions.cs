using System;
using System.Collections.Generic;
using RBV.Utilities.Extensions;

namespace RBV.Utilities.Wrappers
{
    public static class ForeachDictionaryExtensions
    {
        public static ForeachDictionaryBuilder<TKey, bool> ForeachYesInvoke<TKey>(
            this ForeachDictionaryBuilder<TKey, bool> source, Action<TKey> action)
        {
            source.Steps.Add((key, flag) => flag.IfYesInvoke(() => action?.Invoke(key)));
            return source;
        }

        public static ForeachDictionaryBuilder<TKey, bool> ForeachYesSet<TKey>(
            this ForeachDictionaryBuilder<TKey, bool> source, bool value)
        {
            source.Steps.Add((key, flag) => source.Source[key] = flag.IfYesSet(value));
            return source;
        }

        public static ForeachDictionaryBuilder<TKey, TValue> ToForeachBuilder<TKey, TValue>(
            this Dictionary<TKey, TValue> source, IEnumerable<TKey> keys = default) =>
            new() { Source = source, Keys = keys ?? source.Keys };
    }
}