using System;
using System.Collections.Generic;

namespace RBV.Utilities.Wrappers
{
    public class ForeachDictionaryBuilder<TKey, TValue>
    {
        public Dictionary<TKey, TValue> Source { get; set; }
        public IEnumerable<TKey>        Keys   { get; set; }

        public List<Action<TKey, TValue>> Steps { get; } = new();

        public Dictionary<TKey, TValue> Execute()
        {
            foreach (TKey key in Keys)
                if (Source != default && Source.TryGetValue(key, out TValue value))
                    foreach (Action<TKey, TValue> step in Steps)
                        step.Invoke(key, value);

            return Source;
        }
    }
}