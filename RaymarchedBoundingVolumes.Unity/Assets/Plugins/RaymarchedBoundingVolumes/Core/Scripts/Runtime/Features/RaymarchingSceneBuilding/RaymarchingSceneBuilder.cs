using System.Collections.Generic;
using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Infrastructure;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public partial class RaymarchingSceneBuilder : MonoBehaviour, IRaymarchingSceneBuilder
    {
        [field: SerializeField] public RaymarchingData Data { get; set; } = new();

        private IRaymarchingFeaturesRegister _raymarchingFeaturesRegister;
        private IRaymarchingDataInitializer  _raymarchingDataInitializer;
        private IShaderBuffersInitializer    _shaderBuffersInitializer;
        private IShaderDataUpdater           _shaderDataUpdater;

        private bool _isNeededToBuildScene;

        private void Awake() => Construct();

        private void Construct() => Construct(
            IServiceContainer.Scoped(gameObject.scene).Resolve<IRaymarchingFeaturesRegister>(),
            IServiceContainer.Global.Resolve<IRaymarchingDataInitializer>(),
            IServiceContainer.Global.Resolve<IShaderBuffersInitializer>(),
            IServiceContainer.Global.Resolve<IShaderDataUpdater>());

        public void Construct(IRaymarchingFeaturesRegister raymarchingFeaturesRegister,
            IRaymarchingDataInitializer raymarchingDataInitializer,
            IShaderBuffersInitializer shaderBuffersInitializer,
            IShaderDataUpdater shaderDataUpdater)
        {
            _raymarchingFeaturesRegister = raymarchingFeaturesRegister;
            _raymarchingDataInitializer  = raymarchingDataInitializer;
            _shaderBuffersInitializer    = shaderBuffersInitializer;
            _shaderDataUpdater           = shaderDataUpdater;
        }

#if !UNITY_EDITOR
        private void OnEnable()  => SubscribeToEvents();
        private void OnDisable() => UnsubscribeFromEvents();
        private void OnDestroy() => Deinitialize();

#endif
        private void Update()
        {
            if (_isNeededToBuildScene)
            {
                BuildSceneIfChanged();
                _isNeededToBuildScene = false;
            }

            _shaderDataUpdater.Update();
        }

        public IRaymarchingSceneBuilder BuildScene()
        {
            _isNeededToBuildScene = true;
            return this;
        }

        private void BuildSceneIfChanged()
        {
            List<RaymarchingFeature> oldFeatures = Data.Features;
            Dictionary<RaymarchingFeature, OperationChildrenData> oldOperationData = Data.Features.ToDictionary(
                feature => feature,
                feature => feature is RaymarchingOperation operation ? operation.Children : default
            );

            Data.Features = _raymarchingFeaturesRegister.FindAllRaymarchingFeatures(gameObject.scene);
            foreach (RaymarchingOperation operation in Data.Operations)
                operation.CalculateChildrenCount();

            if (IsSceneChanged(oldOperationData, oldFeatures, Data.Features))
            {
                UnsubscribeFromEvents();
                _raymarchingFeaturesRegister.RegisterFeatures();
                SubscribeToEvents();
                BuildSceneInternal();
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
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

        private void BuildSceneInternal()
        {
            _raymarchingDataInitializer.InitializeData(Data);

            _shaderBuffersInitializer.ReleaseBuffers();
            ShaderBuffers shaderBuffers = _shaderBuffersInitializer
                .InitializeBuffers(Data.OperationsShaderData.Count, Data.ObjectsShaderData.Count);

            _shaderDataUpdater.Initialize(shaderBuffers, Data);
        }

        private void Deinitialize() => _shaderBuffersInitializer.ReleaseBuffers();

        private void SubscribeToEvents()
        {
            foreach (RaymarchingFeature feature in Data.Features)
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

            foreach (RaymarchingOperation operation in Data.Operations)
                operation.Changed += _shaderDataUpdater.UpdateOperationData;
            foreach (RaymarchedObject obj in Data.Objects)
                obj.Changed += _shaderDataUpdater.UpdateObjectData;
        }

        private void UnsubscribeFromEvents()
        {
            foreach (RaymarchingFeature feature in Data.Features)
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

            foreach (RaymarchingOperation operation in Data.Operations)
                operation.Changed -= _shaderDataUpdater.UpdateOperationData;
            foreach (RaymarchedObject obj in Data.Objects)
                obj.Changed -= _shaderDataUpdater.UpdateObjectData;
        }

        private void BuildScene(RaymarchingOperation operation, ChangedValue<int> siblingIndex) => BuildScene();
        private void BuildScene(RaymarchedObject obj, ChangedValue<int> siblingIndex)           => BuildScene();
        private void BuildScene(RaymarchingOperation operation)                                 => BuildScene();
        private void BuildScene(RaymarchedObject obj)                                           => BuildScene();
    }
}