using RBV.Data.Dynamic;

namespace RBV.Features
{
    public interface IRaymarchingChildrenCalculator
    {
        OperationChildrenData CalculateChildrenCount(RaymarchingOperation operation);
    }
}