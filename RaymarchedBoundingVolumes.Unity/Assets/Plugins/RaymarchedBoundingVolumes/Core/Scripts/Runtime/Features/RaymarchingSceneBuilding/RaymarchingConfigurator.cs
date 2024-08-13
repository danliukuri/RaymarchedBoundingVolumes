using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public partial class RaymarchingConfigurator : MonoBehaviour
    {
        [SerializeField] private int dynamicallyCreatedOperationsCount;
        [SerializeField] private int dynamicallyCreatedObjectsCount;

        [SerializeField] private RaymarchingData data = new();

        private readonly ShaderBuffersInitializer _shaderBuffersInitializer = new();
        private readonly ShaderDataUpdater        _shaderDataUpdater        = new();

        private void Awake() => RegisterFeatures();

#if !UNITY_EDITOR
        private void Start()
        {
            if (data.Features.Any())
                Initialize();
        }

        private void OnEnable()  => SubscribeToChanges();
        private void OnDisable() => UnsubscribeFromChanges();
        
#endif
        private void Update()    => _shaderDataUpdater.Update();
        private void OnDestroy() => Deinitialize();

        private void RegisterFeatures()
        {
            if (data.Features.Any())
            {
                data.Operations = data.Features.OfType<RaymarchingOperation>().ToList();
                data.Objects    = data.Features.OfType<RaymarchedObject>().ToList();
            }
        }

        private void Initialize()
        {
            new RaymarchingDataInitializer(new RaymarchingSceneTreePostorderDFSTraverser()).InitializeData(data);

            ShaderBuffers shaderBuffers = _shaderBuffersInitializer
                .InitializeBuffers(data.OperationsShaderData.Count + dynamicallyCreatedOperationsCount,
                    data.ObjectsShaderData.Count + dynamicallyCreatedObjectsCount);

            _shaderDataUpdater.Initialize(shaderBuffers, data);
        }

        private void Deinitialize() => _shaderBuffersInitializer.ReleaseBuffers();

        private void SubscribeToChanges()
        {
            foreach (RaymarchingOperation operation in data.Operations)
                operation.Changed += _shaderDataUpdater.UpdateOperationData;
            foreach (RaymarchedObject obj in data.Objects)
                obj.Changed += _shaderDataUpdater.UpdateObjectData;
        }

        private void UnsubscribeFromChanges()
        {
            foreach (RaymarchingOperation operation in data.Operations)
                operation.Changed -= _shaderDataUpdater.UpdateOperationData;
            foreach (RaymarchedObject obj in data.Objects)
                obj.Changed -= _shaderDataUpdater.UpdateObjectData;
        }
    }
}