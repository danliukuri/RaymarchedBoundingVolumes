using RaymarchedBoundingVolumes.Data.Dynamic;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingSceneBuilder
    {
        RaymarchingData Data { get; }

        IRaymarchingSceneBuilder BuildScene();
    }
}