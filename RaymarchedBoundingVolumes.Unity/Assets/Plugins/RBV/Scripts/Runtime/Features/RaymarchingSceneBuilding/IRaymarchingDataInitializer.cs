using RBV.Data.Dynamic;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingDataInitializer
    {
        RaymarchingData InitializeData(RaymarchingData data);
    }
}