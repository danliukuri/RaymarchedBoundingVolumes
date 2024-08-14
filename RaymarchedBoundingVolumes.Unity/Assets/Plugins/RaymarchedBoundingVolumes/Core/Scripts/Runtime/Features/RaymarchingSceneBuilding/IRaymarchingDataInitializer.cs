using RaymarchedBoundingVolumes.Data.Dynamic;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingDataInitializer
    {
        RaymarchingData InitializeData(RaymarchingData data);
    }
}