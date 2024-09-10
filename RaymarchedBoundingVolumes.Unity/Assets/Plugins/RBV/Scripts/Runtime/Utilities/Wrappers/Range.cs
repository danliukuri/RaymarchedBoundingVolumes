using System;
using UnityEngine;

namespace RBV.Utilities.Wrappers
{
    [Serializable]
    public struct Range<T> : IEquatable<Range<T>> where T : IEquatable<T>
    {
        [field: SerializeField] public T Start { get; set; }
        [field: SerializeField] public T End   { get; set; }

        public Range(T start, T end)
        {
            Start = start;
            End   = end;
        }

        public bool Equals(Range<T> other) => Start.Equals(other.Start) && End.Equals(other.End);

        public override bool Equals(object obj) => obj is Range<T> other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Start, End);

        public static bool operator ==(Range<T> left, Range<T> right) =>
            left.Equals(right);

        public static bool operator !=(Range<T> left, Range<T> right) =>
            !left.Equals(right);

        public void Deconstruct(out T start, out T end)
        {
            start = Start;
            end   = End;
        }
    }
}