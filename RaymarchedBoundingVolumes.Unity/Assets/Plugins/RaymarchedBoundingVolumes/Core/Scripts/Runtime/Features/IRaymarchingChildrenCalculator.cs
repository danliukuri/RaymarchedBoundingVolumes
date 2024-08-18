using RaymarchedBoundingVolumes.Data.Dynamic;

namespace RaymarchedBoundingVolumes.Features
{
    public interface IRaymarchingChildrenCalculator
    {
        OperationChildrenData CalculateChildrenCount(RaymarchingOperation operation);
    }
}