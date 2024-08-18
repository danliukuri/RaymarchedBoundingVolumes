using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class RaymarchingFeaturesRegister : IRaymarchingFeaturesRegister
    {
        private readonly IRaymarchingSceneBuilder _sceneBuilder;

        public RaymarchingFeaturesRegister(IRaymarchingSceneBuilder sceneBuilder) => _sceneBuilder = sceneBuilder;

        public IRaymarchingFeaturesRegister RegisterFeatures()
        {
            _sceneBuilder.Data.Operations = _sceneBuilder.Data.Features.OfType<RaymarchingOperation>().ToList();
            _sceneBuilder.Data.Objects    = _sceneBuilder.Data.Features.OfType<RaymarchedObject>().ToList();

            return this;
        }

        public List<RaymarchingFeature> FindAllRaymarchingFeatures(Scene scene) => scene.GetRootGameObjects()
            .SelectMany(rootGameObject => rootGameObject.GetComponentsInChildren<RaymarchingFeature>()).ToList();
    }
}