﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public class RaymarchingFeaturesRegister : IRaymarchingFeaturesRegister
    {
        private readonly IRaymarchingSceneDataProvider _dataProvider;

        public RaymarchingFeaturesRegister(IRaymarchingSceneDataProvider dataProvider) => _dataProvider = dataProvider;

        public IRaymarchingFeaturesRegister RegisterFeatures()
        {
            _dataProvider.Data.Operations = _dataProvider.Data.Features.OfType<RaymarchingOperation>().ToList();
            _dataProvider.Data.Objects    = _dataProvider.Data.Features.OfType<RaymarchedObject>().ToList();

            _dataProvider.Data.OperationsByType = _dataProvider.Data.Operations
                .GroupBy(operation => operation.Type.Value).ToDictionary(group => group.Key, group => group.ToList());
            _dataProvider.Data.ObjectsByTransformsType = _dataProvider.Data.Objects
                .GroupBy(obj => obj.TransformType).ToDictionary(group => group.Key, group => group.ToList());
            _dataProvider.Data.ObjectsByType = _dataProvider.Data.Objects
                .GroupBy(obj => obj.Type.Value).ToDictionary(group => group.Key, group => group.ToList());

            return this;
        }

        public List<RaymarchingFeature> FindAllRaymarchingFeatures(Scene scene) => scene.GetRootGameObjects()
            .SelectMany(rootGameObject => rootGameObject.GetComponentsInChildren<RaymarchingFeature>())
            .Where(feature => feature.IsActive)
            .ToList();
    }
}