using System.Collections.Generic;
using RBV.Data.Dynamic;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingSceneTreeTraverser
    {
        List<OperationMetaData> TraverseScene(List<RaymarchingFeature> features);
    }
}