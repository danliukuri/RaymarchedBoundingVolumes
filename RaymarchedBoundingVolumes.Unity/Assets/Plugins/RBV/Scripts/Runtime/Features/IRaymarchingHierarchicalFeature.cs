using RBV.Data.Dynamic.HierarchicalStates;

namespace RBV.Features
{
    public interface IRaymarchingHierarchicalFeature
    {
        IRaymarchingFeatureHierarchicalState HierarchicalState { get; }
    }
}