using System;

namespace RaymarchedBoundingVolumes.Data.Dynamic
{
    public struct OperationChildrenData : IEquatable<OperationChildrenData>
    {
        public int TotalObjectsCount    { get; set; }
        public int TotalOperationsCount { get; set; }
        public int TotalFeaturesCount   { get; set; }

        public int DirectObjectsCount    { get; set; }
        public int DirectOperationsCount { get; set; }
        public int DirectFeaturesCount   { get; set; }

        public bool Equals(OperationChildrenData other) => TotalObjectsCount     == other.TotalObjectsCount     &&
                                                           TotalOperationsCount  == other.TotalOperationsCount  &&
                                                           TotalFeaturesCount    == other.TotalFeaturesCount    &&
                                                           DirectObjectsCount    == other.DirectObjectsCount    &&
                                                           DirectOperationsCount == other.DirectOperationsCount &&
                                                           DirectFeaturesCount   == other.DirectFeaturesCount;

        public override bool Equals(object obj) => obj is OperationChildrenData other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(TotalObjectsCount, TotalOperationsCount,
            TotalFeaturesCount, DirectObjectsCount, DirectOperationsCount, DirectFeaturesCount);

        public static bool operator ==(OperationChildrenData left, OperationChildrenData right) => left.Equals(right);

        public static bool operator !=(OperationChildrenData left, OperationChildrenData right) => !left.Equals(right);
    }
}