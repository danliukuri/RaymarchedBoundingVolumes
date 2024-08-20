using System;
using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingSceneBuilder
    {
        event Action<Scene> SceneBuilt;

        IRaymarchingSceneBuilder SubscribeToFeatureEvents();
        IRaymarchingSceneBuilder UnsubscribeFromFeatureEvents();
        IRaymarchingSceneBuilder Update(Scene scene);
        IRaymarchingSceneBuilder BuildNewScene();
        IRaymarchingSceneBuilder BuildLastScene();
    }
}