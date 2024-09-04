using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingFeaturesRegister
    {
        IRaymarchingFeaturesRegister RegisterFeatures();

        List<RaymarchingFeature> FindAllRaymarchingFeatures(Scene scene);
    }
}