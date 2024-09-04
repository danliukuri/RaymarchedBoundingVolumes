using System;

namespace RBV.Data.Dynamic.HierarchicalStates
{
    public struct RaymarchingOperationHierarchicalState : IRaymarchingFeatureHierarchicalState,
                                                          IEquatable<RaymarchingOperationHierarchicalState>
    {
        public IRaymarchingFeatureHierarchicalState BaseState { get; set; }

        public OperationChildrenData Children { get; set; }

        public bool Equals(RaymarchingOperationHierarchicalState other) => BaseState.Equals(other.BaseState) &&
                                                                           Children == other.Children;

        public override bool Equals(object obj) => obj is RaymarchingOperationHierarchicalState other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(BaseState, Children);

        public static bool operator ==(RaymarchingOperationHierarchicalState left,
                                       RaymarchingOperationHierarchicalState right) => left.Equals(right);

        public static bool operator !=(RaymarchingOperationHierarchicalState left,
                                       RaymarchingOperationHierarchicalState right) => !left.Equals(right);
    }
}