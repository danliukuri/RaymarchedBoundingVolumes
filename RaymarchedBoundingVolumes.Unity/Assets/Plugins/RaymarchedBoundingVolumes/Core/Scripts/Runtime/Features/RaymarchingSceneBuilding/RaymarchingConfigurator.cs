using System.Collections.Generic;
using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    [ExecuteInEditMode]
    public class RaymarchingConfigurator : MonoBehaviour
    {
        [SerializeField] private int dynamicallyCreatedOperationsCount;
        [SerializeField] private int dynamicallyCreatedObjectsCount;

        [SerializeField] private RaymarchingData data = new();

        private readonly ShaderBuffersInitializer _shaderBuffersInitializer = new();
        private readonly ShaderDataUpdater        _shaderDataUpdater        = new();

        private void Awake() => Initialize();

        private void Initialize()
        {
            if (data.Features.Any())
            {
                data.Operations = data.Features.OfType<RaymarchingOperation>().ToList();
                foreach (RaymarchingOperation operation in data.Operations)
                {
                    operation.Initialize();
                    RegisterOperation(operation);
                }

                data.Objects = data.Features.OfType<RaymarchedObject>().ToList();
                foreach (RaymarchedObject raymarchedObject in data.Objects)
                {
                    raymarchedObject.Initialize();
                    RegisterObject(raymarchedObject);
                }

                new RaymarchingDataInitializer(new RaymarchingSceneTreePostorderDFSTraverser()).InitializeData(data);
                
                ShaderBuffers shaderBuffers = _shaderBuffersInitializer
                    .InitializeBuffers(data.OperationsShaderData.Count + dynamicallyCreatedOperationsCount,
                        data.ObjectsShaderData.Count + dynamicallyCreatedObjectsCount);

                _shaderDataUpdater.Initialize(shaderBuffers, data);
            }
        }

        private void OnDestroy() => _shaderBuffersInitializer.ReleaseBuffers();

#if UNITY_EDITOR
        [ContextMenu(nameof(RegisterAllRaymarchingFeatures))]
        public void RegisterAllRaymarchingFeatures()
        {
            int operationsCount = data.Features.Count;
            data.Features = FindAllRaymarchingFeatures(gameObject.scene);

            Initialize();
            if (operationsCount != data.Features.Count)
                EditorUtility.SetDirty(this);
        }
        
        private static List<RaymarchingFeature> FindAllRaymarchingFeatures(Scene scene) => scene.GetRootGameObjects()
            .SelectMany(rootGameObject => rootGameObject.GetComponentsInChildren<RaymarchingFeature>())
            .ToList();

        private void RegisterOperation(RaymarchingOperation operation)
        {
            if (!data.Operations.Contains(operation))
            {
                data.Operations.Add(operation);
                operation.Changed += _shaderDataUpdater.UpdateOperationData;
            }
        }
        private void RegisterObject(RaymarchedObject raymarchedObject)
        {
            if (!data.Objects.Contains(raymarchedObject))
            {
                data.Objects.Add(raymarchedObject);
                raymarchedObject.Changed += _shaderDataUpdater.UpdateObjectData;
            }
        }

        private void OnEnable()
        {
            Initialize();
            UnsubscribeFromChanges();
            SubscribeToChanges();
        }

        private void OnDisable()
        {
            UnsubscribeFromChanges();
            OnDestroy();
        }
#else
        private void OnEnable()
        {
            UnsubscribeFromChanges();
            SubscribeToChanges();
        } 

        private void OnDisable() => UnsubscribeFromChanges();
#endif
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

        private void Update() => _shaderDataUpdater.Update();
    }
}