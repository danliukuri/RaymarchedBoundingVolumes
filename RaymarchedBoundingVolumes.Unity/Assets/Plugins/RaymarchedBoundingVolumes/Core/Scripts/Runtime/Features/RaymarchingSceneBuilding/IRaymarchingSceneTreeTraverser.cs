using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Dynamic;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingSceneTreeTraverser
    {
        List<OperationMetaData> TraverseScene(List<RaymarchingFeature> features);
    }
}