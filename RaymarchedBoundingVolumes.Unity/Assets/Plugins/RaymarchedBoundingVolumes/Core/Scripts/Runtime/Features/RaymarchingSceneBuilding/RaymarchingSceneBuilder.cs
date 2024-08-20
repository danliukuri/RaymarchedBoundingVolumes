using System;
using System.Collections.Generic;
using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class RaymarchingSceneBuilder : IRaymarchingSceneBuilder
    {
        public event Action<Scene> SceneBuilt;

        private readonly IRaymarchingDataInitializer         _dataInitializer;
        private readonly IRaymarchingSceneDataProvider       _dataProvider;
        private readonly IRaymarchingFeatureEventsSubscriber _featureEventsSubscriber;
        private readonly IRaymarchingFeaturesRegister        _featuresRegister;
        private readonly IShaderBuffersInitializer           _shaderBuffersInitializer;
        private readonly IShaderDataUpdater                  _shaderDataUpdater;

        private bool _isNeededToBuildScene;

        public RaymarchingSceneBuilder(IRaymarchingDataInitializer         dataInitializer,
                                       IRaymarchingSceneDataProvider       dataProvider,
                                       IRaymarchingFeaturesRegister        featuresRegister,
                                       IShaderBuffersInitializer           shaderBuffersInitializer,
                                       IShaderDataUpdater                  shaderDataUpdater,
                                       IRaymarchingFeatureEventsSubscriber featureEventsSubscriber)
        {
            _dataInitializer          = dataInitializer;
            _dataProvider             = dataProvider;
            _featuresRegister         = featuresRegister;
            _shaderBuffersInitializer = shaderBuffersInitializer;
            _shaderDataUpdater        = shaderDataUpdater;
            _featureEventsSubscriber  = featureEventsSubscriber;
        }

        public IRaymarchingSceneBuilder SubscribeToFeatureEvents()
        {
            foreach (RaymarchingFeature feature in _dataProvider.Data.Features)
                switch (feature)
                {
                    case RaymarchingHierarchicalFeature<RaymarchingOperation> operation:
                        operation.ParentChanged += BuildScene;
                        operation.Reordered     += BuildScene;
                        break;
                    case RaymarchingHierarchicalFeature<RaymarchedObject> obj:
                        obj.ParentChanged += BuildScene;
                        obj.Reordered     += BuildScene;
                        break;
                }

            return this;
        }

        public IRaymarchingSceneBuilder UnsubscribeFromFeatureEvents()
        {
            foreach (RaymarchingFeature feature in _dataProvider.Data.Features)
                switch (feature)
                {
                    case RaymarchingHierarchicalFeature<RaymarchingOperation> operation:
                        operation.ParentChanged -= BuildScene;
                        operation.Reordered     -= BuildScene;
                        break;
                    case RaymarchingHierarchicalFeature<RaymarchedObject> obj:
                        obj.ParentChanged -= BuildScene;
                        obj.Reordered     -= BuildScene;
                        break;
                }

            return this;
        }

        public IRaymarchingSceneBuilder Update(Scene scene)
        {
            if (_isNeededToBuildScene)
            {
                BuildSceneIfChanged(scene);
                _isNeededToBuildScene = false;
            }

            return this;
        }

        public IRaymarchingSceneBuilder BuildNewScene()
        {
            _isNeededToBuildScene = true;
            return this;
        }

        public IRaymarchingSceneBuilder BuildLastScene()
        {
            _featuresRegister.RegisterFeatures();
            foreach (RaymarchingOperation operation in _dataProvider.Data.Operations)
                operation.CalculateChildrenCount();
            BuildScene();
            _featureEventsSubscriber.SubscribeToFeatureEvents();
            return this;
        }

        private void BuildSceneIfChanged(Scene scene)
        {
            List<RaymarchingFeature> oldFeatures = _dataProvider.Data.Features;
            Dictionary<RaymarchingFeature, OperationChildrenData> oldOperationData =
                _dataProvider.Data.Features.ToDictionary(
                    feature => feature,
                    feature => feature is RaymarchingOperation operation ? operation.Children : default
                );

            _dataProvider.Data.Features = _featuresRegister.FindAllRaymarchingFeatures(scene);
            foreach (RaymarchingOperation operation in _dataProvider.Data.Operations)
                operation.CalculateChildrenCount();

            if (IsSceneChanged(oldOperationData, oldFeatures, _dataProvider.Data.Features))
            {
                _featureEventsSubscriber.UnsubscribeFromFeatureEvents();
                _featuresRegister.RegisterFeatures();
                BuildScene();
                _featureEventsSubscriber.SubscribeToFeatureEvents();
                SceneBuilt?.Invoke(scene);
            }
        }

        private bool IsSceneChanged(Dictionary<RaymarchingFeature, OperationChildrenData> oldOperationData,
                                    List<RaymarchingFeature> oldFeatures, List<RaymarchingFeature> newFeatures)
        {
            if (oldFeatures.Count != newFeatures.Count)
                return true;

            for (var i = 0; i < oldFeatures.Count; i++)
            {
                if (oldFeatures[i] != newFeatures[i])
                    return true;

                if (newFeatures[i] is RaymarchingOperation newOperation &&
                    oldOperationData[oldFeatures[i]] != newOperation.Children)
                    return true;
            }

            return false;
        }

        private void BuildScene()
        {
            _dataInitializer.InitializeData(_dataProvider.Data);

            _shaderBuffersInitializer.ReleaseBuffers();
            ShaderBuffers shaderBuffers = _shaderBuffersInitializer
                .InitializeBuffers(_dataProvider.Data.OperationsShaderData.Count,
                    _dataProvider.Data.ObjectsShaderData.Count);

            _shaderDataUpdater.Initialize(shaderBuffers);
        }

        private void BuildScene(RaymarchingOperation operation, ChangedValue<int> siblingIndex) => BuildNewScene();
        private void BuildScene(RaymarchedObject     obj,       ChangedValue<int> siblingIndex) => BuildNewScene();
        private void BuildScene(RaymarchingOperation operation) => BuildNewScene();
        private void BuildScene(RaymarchedObject     obj)       => BuildNewScene();
    }
}