﻿using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.HierarchicalStates;
using RBV.Utilities.Wrappers;
using UnityEngine.SceneManagement;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public class RaymarchingSceneBuilder : IRaymarchingSceneBuilder
    {
        public event Action<Scene> NewSceneBuilt;

        private readonly IRaymarchingDataInitializer         _dataInitializer;
        private readonly IRaymarchingSceneDataProvider       _dataProvider;
        private readonly IRaymarchingFeatureEventsSubscriber _featureEventsSubscriber;
        private readonly IRaymarchingFeaturesRegister        _featuresRegister;
        private readonly IShaderBuffersInitializer           _shaderBuffersInitializer;
        private readonly IShaderDataUpdater                  _shaderDataUpdater;

        private bool _isNeededToBuildScene;

        private Dictionary<RaymarchingFeature, IRaymarchingFeatureHierarchicalState> _oldHierarchicalStates;

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
                        operation.ActiveStateChanged += BuildScene;
                        operation.ParentChanged      += BuildScene;
                        operation.Reordered          += BuildScene;
                        break;
                    case RaymarchingHierarchicalFeature<RaymarchedObject> obj:
                        obj.ActiveStateChanged += BuildScene;
                        obj.ParentChanged      += BuildScene;
                        obj.Reordered          += BuildScene;
                        if (obj is RaymarchedObject raymarchedObject)
                            raymarchedObject.TypeChanged += BuildScene;
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
                        operation.ActiveStateChanged -= BuildScene;
                        operation.ParentChanged      -= BuildScene;
                        operation.Reordered          -= BuildScene;
                        break;
                    case RaymarchingHierarchicalFeature<RaymarchedObject> obj:
                        obj.ActiveStateChanged -= BuildScene;
                        obj.ParentChanged      -= BuildScene;
                        obj.Reordered          -= BuildScene;
                        if (obj is RaymarchedObject raymarchedObject)
                            raymarchedObject.TypeChanged -= BuildScene;
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
            _dataProvider.Data.Operations.ForEach(operation => operation.CalculateChildrenCount());
            BuildScene();
            _featureEventsSubscriber.SubscribeToFeatureEvents();
            return this;
        }

        private void BuildSceneIfChanged(Scene scene)
        {
            List<RaymarchingFeature> newFeatures = _featuresRegister.FindAllRaymarchingFeatures(scene);
            foreach (RaymarchingOperation operation in newFeatures.OfType<RaymarchingOperation>())
                operation.CalculateChildrenCount();
            
            if (IsSceneChanged(_dataProvider.Data.Features, newFeatures))
            {
                _dataProvider.Data.Features = newFeatures;
                _featureEventsSubscriber.UnsubscribeFromFeatureEvents();
                _featuresRegister.RegisterFeatures();
                BuildScene();
                _featureEventsSubscriber.SubscribeToFeatureEvents();
                NewSceneBuilt?.Invoke(scene);
            }
        }

        private bool IsSceneChanged(List<RaymarchingFeature> oldFeatures, List<RaymarchingFeature> newFeatures)
        {
            Dictionary<RaymarchingFeature, IRaymarchingFeatureHierarchicalState> newHierarchicalStates =
                newFeatures.ToDictionary(feature => feature,
                    feature => feature is IRaymarchingHierarchicalFeature hierarchicalFeature
                        ? hierarchicalFeature.HierarchicalState
                        : default);

            bool isSceneChanged = oldFeatures.Count != newFeatures.Count ||
                                  (_oldHierarchicalStates != default &&
                                   Enumerable.Range(default, oldFeatures.Count).Any(IsHierarchicalStateChanged));

            _oldHierarchicalStates = newHierarchicalStates;

            return isSceneChanged;

            bool IsHierarchicalStateChanged(int i) =>
                oldFeatures[i] != newFeatures[i] ||
                (newFeatures[i] is IRaymarchingHierarchicalFeature &&
                 !_oldHierarchicalStates[oldFeatures[i]].Equals(newHierarchicalStates[newFeatures[i]]));
        }

        private void BuildScene()
        {
            _dataInitializer.InitializeData(_dataProvider.Data);

            _shaderBuffersInitializer.ReleaseBuffers();
            ShaderBuffers shaderBuffers = _shaderBuffersInitializer.InitializeBuffers(
                _dataProvider.Data.OperationsShaderData.Count, _dataProvider.Data.ObjectsShaderData.Count,
                _dataProvider.Data.ObjectsByTransformsType, _dataProvider.Data.ObjectsByType);

            _shaderDataUpdater.Initialize(shaderBuffers);
        }

        private void BuildScene(RaymarchingOperation operation, ChangedValue<int> siblingIndex) => BuildNewScene();
        private void BuildScene(RaymarchedObject     obj,       ChangedValue<int> siblingIndex) => BuildNewScene();
        private void BuildScene(RaymarchingOperation operation) => BuildNewScene();
        private void BuildScene(RaymarchedObject     obj)       => BuildNewScene();
    }
}