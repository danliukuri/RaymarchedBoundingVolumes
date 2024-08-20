using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class RaymarchingFeaturesRegister : IRaymarchingFeaturesRegister
    {
        private readonly IRaymarchingSceneDataProvider _dataProvider;

        public RaymarchingFeaturesRegister(IRaymarchingSceneDataProvider dataProvider) =>
            _dataProvider = dataProvider;

        public IRaymarchingFeaturesRegister RegisterFeatures()
        {
            _dataProvider.Data.Operations = _dataProvider.Data.Features.OfType<RaymarchingOperation>().ToList();
            _dataProvider.Data.Objects    = _dataProvider.Data.Features.OfType<RaymarchedObject>().ToList();
            return this;
        }

        public List<RaymarchingFeature> FindAllRaymarchingFeatures(Scene scene) => scene.GetRootGameObjects()
            .SelectMany(rootGameObject => rootGameObject.GetComponentsInChildren<RaymarchingFeature>()).ToList();
    }
}