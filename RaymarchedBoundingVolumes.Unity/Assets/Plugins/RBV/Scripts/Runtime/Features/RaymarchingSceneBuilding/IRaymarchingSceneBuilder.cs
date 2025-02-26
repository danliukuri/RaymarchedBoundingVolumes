﻿using System;
using UnityEngine.SceneManagement;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public interface IRaymarchingSceneBuilder
    {
        event Action<Scene> NewSceneBuilt;

        IRaymarchingSceneBuilder SubscribeToFeatureEvents();
        IRaymarchingSceneBuilder UnsubscribeFromFeatureEvents();
        IRaymarchingSceneBuilder Update(Scene scene);
        IRaymarchingSceneBuilder BuildNewScene();
        IRaymarchingSceneBuilder BuildLastScene();
    }
}