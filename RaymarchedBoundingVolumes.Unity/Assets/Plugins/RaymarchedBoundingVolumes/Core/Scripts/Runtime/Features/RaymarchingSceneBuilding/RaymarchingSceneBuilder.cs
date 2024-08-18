using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Infrastructure;
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
        private void Start()
        {
            _raymarchingFeaturesRegister.RegisterFeatures();
            foreach (RaymarchingOperation operation in Data.Operations)
                operation.CalculateChildrenCount();
            SubscribeToChanges();
            BuildScene();
        }

        private void OnEnable()  => SubscribeToChanges();
        private void OnDisable() => UnsubscribeFromChanges();
        private void OnDestroy() => Deinitialize();

#endif
        private void Update() => _shaderDataUpdater.Update();

        private void BuildScene()
        {
            _raymarchingDataInitializer.InitializeData(Data);

            ShaderBuffers shaderBuffers = _shaderBuffersInitializer
                .InitializeBuffers(Data.OperationsShaderData.Count, Data.ObjectsShaderData.Count);

            _shaderDataUpdater.Initialize(shaderBuffers, Data);
        }

        private void Deinitialize() => _shaderBuffersInitializer.ReleaseBuffers();

        private void SubscribeToChanges()
        {
            foreach (RaymarchingOperation operation in Data.Operations)
                operation.Changed += _shaderDataUpdater.UpdateOperationData;
            foreach (RaymarchedObject obj in Data.Objects)
                obj.Changed += _shaderDataUpdater.UpdateObjectData;
        }

        private void UnsubscribeFromChanges()
        {
            foreach (RaymarchingOperation operation in Data.Operations)
                operation.Changed -= _shaderDataUpdater.UpdateOperationData;
            foreach (RaymarchedObject obj in Data.Objects)
                obj.Changed -= _shaderDataUpdater.UpdateObjectData;
        }
    }
}