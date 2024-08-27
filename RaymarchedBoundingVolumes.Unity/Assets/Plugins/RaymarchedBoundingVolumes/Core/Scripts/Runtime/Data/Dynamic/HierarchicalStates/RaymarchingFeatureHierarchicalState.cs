using System;

namespace RaymarchedBoundingVolumes.Data.Dynamic.HierarchicalStates
{
    public struct RaymarchingFeatureHierarchicalState : IRaymarchingFeatureHierarchicalState,
                                                        IEquatable<RaymarchingFeatureHierarchicalState>
    {
        public int SiblingIndex { get; set; }

        public bool Equals(RaymarchingFeatureHierarchicalState other) => SiblingIndex == other.SiblingIndex;

        public override bool Equals(object obj) => obj is RaymarchingFeatureHierarchicalState other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(SiblingIndex);

        public static bool operator ==(RaymarchingFeatureHierarchicalState left,
                                       RaymarchingFeatureHierarchicalState right) => left.Equals(right);

        public static bool operator !=(RaymarchingFeatureHierarchicalState left,
                                       RaymarchingFeatureHierarchicalState right) => !left.Equals(right);
    }
}