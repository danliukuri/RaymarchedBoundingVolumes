using System;
using System.Collections.Generic;

namespace RBV.Utilities.Wrappers
{
    public static class ChangedValueExtensions
    {
        private static readonly Dictionary<object, Dictionary<Type, object>> _actionsCache = new();

        public static Action<ChangedValue<TResult>> CastCached<TResult>(this Action source)
        {
            if (!_actionsCache.ContainsKey(source))
                _actionsCache.Add(source, new Dictionary<Type, object>());

            if (!_actionsCache[source].ContainsKey(typeof(TResult)))
                _actionsCache[source].Add(typeof(TResult), source.Cast<TResult>());

            return _actionsCache[source][typeof(TResult)] as Action<ChangedValue<TResult>>;
        }

        public static Action<ChangedValue<TResult>> CastCached<TSource, TResult>(
            this Action<ChangedValue<TSource>> source) where TResult : TSource
        {
            if (!_actionsCache.ContainsKey(source))
                _actionsCache.Add(source, new Dictionary<Type, object>());

            if (!_actionsCache[source].ContainsKey(typeof(TResult)))
                _actionsCache[source].Add(typeof(TResult), source.Cast<TSource, TResult>());

            return _actionsCache[source][typeof(TResult)] as Action<ChangedValue<TResult>>;
        }

        public static Action<ChangedValue<TResult>> Cast<TResult>(this Action source) => value => source.Invoke();

        public static Action<ChangedValue<TResult>> Cast<TSource, TResult>(this Action<ChangedValue<TSource>> source)
            where TResult : TSource => value => source.Invoke(value.Cast<TResult, TSource>());

        public static ChangedValue<TResult> Cast<TSource, TResult>(this ChangedValue<TSource> source)
            where TSource : TResult => new(source.Old, source.New);
    }
}