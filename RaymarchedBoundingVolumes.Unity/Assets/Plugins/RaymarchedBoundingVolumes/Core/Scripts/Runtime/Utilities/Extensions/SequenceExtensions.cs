using System.Collections.Generic;

namespace RaymarchedBoundingVolumes.Utilities.Extensions
{
    public static class SequenceExtensions
    {
        public static int FirstIndex<T>(this ICollection<T> source) => default;
        public static int LastIndex<T>(this ICollection<T> source)  => source.Count - 1;
    }
}