using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.HierarchicalStates;
using RBV.Utilities.Extensions;
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
                    case RaymarchingOperation operation:
                        operation.ActiveStateChanged += BuildScene;
                        operation.ParentChanged      += BuildScene;
                        operation.Reordered          += BuildScene;
                        operation.TypeChanged        += BuildScene;
                        break;
                    case RaymarchedObject obj:
                        obj.ActiveStateChanged += BuildScene;
                        obj.ParentChanged      += BuildScene;
                        obj.Reordered          += BuildScene;
                        obj.TypeChanged        += BuildScene;
                        break;
                }

            return this;
        }

        public IRaymarchingSceneBuilder UnsubscribeFromFeatureEvents()
        {
            foreach (RaymarchingFeature feature in _dataProvider.Data.Features)
                switch (feature)
                {
                    case RaymarchingOperation operation:
                        operation.ActiveStateChanged -= BuildScene;
                        operation.ParentChanged      -= BuildScene;
                        operation.Reordered          -= BuildScene;
                        operation.TypeChanged        -= BuildScene;
                        break;
                    case RaymarchedObject obj:
                        obj.ActiveStateChanged -= BuildScene;
                        obj.ParentChanged      -= BuildScene;
                        obj.Reordered          -= BuildScene;
                        obj.TypeChanged        -= BuildScene;
                        break;
                }

            return this;
        }

        public IRaymarchingSceneBuilder Update(Scene scene)
        {
            _isNeededToBuildScene.IfYesInvoke(() => BuildSceneIfChanged(scene)).IfYesSet(false);
            return this;
        }

        public IRaymarchingSceneBuilder BuildNewScene()
        {
            _isNeededToBuildScene = true;
            return this;
        }

        public IRaymarchingSceneBuilder BuildLastScene()
        {
            if (_dataProvider.Data.Features?.Any(feature => feature == default) ?? true)
                return this;

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
                _featureEventsSubscriber.UnsubscribeFromFeatureEvents();
                _dataProvider.Data.Features = newFeatures;
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
                                  Enumerable.Range(default, oldFeatures.Count).Any(IsFeatureChanged);

            _oldHierarchicalStates = newHierarchicalStates;

            return isSceneChanged;

            bool IsFeatureChanged(int i) => oldFeatures[i] != newFeatures[i] || IsHierarchicalStateChanged(i);

            bool IsHierarchicalStateChanged(int i) =>
                _oldHierarchicalStates != default                 &&
                newFeatures[i] is IRaymarchingHierarchicalFeature &&
                !_oldHierarchicalStates[oldFeatures[i]].Equals(newHierarchicalStates[newFeatures[i]]);
        }

        private void BuildScene()
        {
            _dataInitializer.InitializeData(_dataProvider.Data);

            _shaderBuffersInitializer.ReleaseBuffers();
            ShaderBuffers shaderBuffers = _shaderBuffersInitializer.InitializeBuffers(_dataProvider.Data);

            _shaderDataUpdater.Initialize(shaderBuffers);
        }

        private void BuildScene(RaymarchingOperation operation, ChangedValue<int> siblingIndex) => BuildNewScene();
        private void BuildScene(RaymarchedObject     obj,       ChangedValue<int> siblingIndex) => BuildNewScene();
        private void BuildScene(RaymarchingOperation operation) => BuildNewScene();
        private void BuildScene(RaymarchedObject     obj)       => BuildNewScene();
    }
}