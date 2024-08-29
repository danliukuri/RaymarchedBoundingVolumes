using System;
using RBV.Data.Static.Enumerations;

namespace RBV.Data.Dynamic.HierarchicalStates
{
    public struct RaymarchedObjectHierarchicalState : IRaymarchingFeatureHierarchicalState,
                                                      IEquatable<RaymarchedObjectHierarchicalState>
    {
        public IRaymarchingFeatureHierarchicalState BaseState { get; set; }

        public RaymarchedObjectType Type { get; set; }

        public bool Equals(RaymarchedObjectHierarchicalState other) => BaseState.Equals(other.BaseState) &&
                                                                       Type == other.Type;

        public override bool Equals(object obj) => obj is RaymarchedObjectHierarchicalState other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(BaseState, Type);

        public static bool operator ==(RaymarchedObjectHierarchicalState left,
                                       RaymarchedObjectHierarchicalState right) => left.Equals(right);

        public static bool operator !=(RaymarchedObjectHierarchicalState left,
                                       RaymarchedObjectHierarchicalState right) => !left.Equals(right);
    }
}