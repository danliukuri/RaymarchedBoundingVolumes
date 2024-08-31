using System;

namespace RBV.Data.Dynamic
{
    public struct RaymarchedObjectType : IEquatable<RaymarchedObjectType>
    {
        public int Value { get; set; }

        public override int GetHashCode() => Value;

        public bool Equals(RaymarchedObjectType other) => Value == other.Value;

        public override bool Equals(object obj) => obj is RaymarchedObjectType other && Equals(other);

        public static bool operator ==(RaymarchedObjectType left, RaymarchedObjectType right) => left.Equals(right);
        public static bool operator !=(RaymarchedObjectType left, RaymarchedObjectType right) => !left.Equals(right);

        public static explicit operator RaymarchedObjectType(int type)                 => new() { Value = type };
        public static explicit operator int(RaymarchedObjectType raymarchedObjectType) => raymarchedObjectType.Value;
    }
}