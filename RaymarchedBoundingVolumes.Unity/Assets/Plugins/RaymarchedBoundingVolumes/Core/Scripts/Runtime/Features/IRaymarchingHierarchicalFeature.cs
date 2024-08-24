using RaymarchedBoundingVolumes.Data.Dynamic.HierarchicalStates;

namespace RaymarchedBoundingVolumes.Features
{
    public interface IRaymarchingHierarchicalFeature
    {
        IRaymarchingFeatureHierarchicalState HierarchicalState { get; }
    }
}