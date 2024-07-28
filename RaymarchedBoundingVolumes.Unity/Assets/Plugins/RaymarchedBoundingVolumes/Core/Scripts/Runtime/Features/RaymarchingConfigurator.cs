using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Dynamic;
using UnityEngine;
using static RaymarchedBoundingVolumes.Data.Static.RaymarchedObjectShaderPropertyIds;

namespace RaymarchedBoundingVolumes.Features
{
    [ExecuteInEditMode]
    public class RaymarchingConfigurator : MonoBehaviour
    {
        [SerializeField] private int dynamicallyCreatedOperationsCount;
        [SerializeField] private int dynamicallyCreatedObjectsCount;

        [SerializeField] private List<RaymarchingOperation> operations = new();
        [SerializeField] private List<RaymarchedObject>     objects    = new();

        private ComputeBuffer _operationsBuffer;
        private ComputeBuffer _objectsBuffer;

        private readonly List<RaymarchingOperation>           _changedOperations = new();
        private readonly List<RaymarchedObject>               _changedObjects    = new();
        private          List<RaymarchingOperationShaderData> _operationsData;
        private          List<RaymarchedObjectShaderData>     _objectsData;

        private bool _isOperationsDataChanged;
        private bool _isObjectsDataChanged;

        private void Awake()
        {
            int operationsBufferSize = operations.Count + dynamicallyCreatedOperationsCount;
            int objectsBufferSize = objects.Count + dynamicallyCreatedObjectsCount;
            if (operationsBufferSize != default)
            {
                InitializeBuffers(operationsBufferSize, objectsBufferSize);
                InitializeBufferDataLists(operationsBufferSize, objectsBufferSize);

                _isOperationsDataChanged = true;
                _isObjectsDataChanged = true;
            }
        }
        private void InitializeBuffers(int operationsBufferSize, int objectsBufferSize)
        {
            _operationsBuffer ??= new ComputeBuffer(operationsBufferSize, RaymarchingOperationShaderData.Size);
            _objectsBuffer    ??= new ComputeBuffer(objectsBufferSize, RaymarchedObjectShaderData.Size);
        }
        private void InitializeBufferDataLists(int operationsBufferSize, int objectsBufferSize)
        {
            _operationsData = new List<RaymarchingOperationShaderData>(operationsBufferSize);
            foreach (RaymarchingOperation operation in operations)
                _operationsData.Add(operation.ShaderData);

            _objectsData = new List<RaymarchedObjectShaderData>(objectsBufferSize);
            foreach (RaymarchedObject obj in objects)
                _objectsData.Add(obj.ShaderData);
        }

        private void OnDestroy() => ReleaseBuffers();
        private void ReleaseBuffers()
        {
            _operationsBuffer?.Release();
            _objectsBuffer?.Release();
        }
        
#if UNITY_EDITOR
        [ContextMenu(nameof(FindAndRegisterAllRaymarchedObjectsAndOperations))]
        public void FindAndRegisterAllRaymarchedObjectsAndOperations()
        {
            GameObject[] rootGameObjects = gameObject.scene.GetRootGameObjects();
            foreach (GameObject rootGameObject in rootGameObjects)
                RegisterOperationsAndItsObjects(rootGameObject.GetComponentsInChildren<RaymarchingOperation>());

            if (_isOperationsDataChanged || _isObjectsDataChanged)
                UnityEditor.EditorUtility.SetDirty(this);

            return;

            void RegisterOperationsAndItsObjects(IEnumerable<RaymarchingOperation> newOperations)
            {
                foreach (RaymarchingOperation newOperation in newOperations)
                    if (!operations.Contains(newOperation))
                    {
                        operations.Add(newOperation);
                        newOperation.Changed += UpdateOperationDataInShader;

                        RegisterObjects(newOperation.GetComponentsInChildren<RaymarchedObject>());
                        _isOperationsDataChanged = true;
                        
                    }
            }

            void RegisterObjects(IEnumerable<RaymarchedObject> newObjects)
            {
                foreach (RaymarchedObject newObject in newObjects)
                    if (!objects.Contains(newObject))
                    {
                        objects.Add(newObject);
                        newObject.Changed     += UpdateObjectDataInShader;
                        _isObjectsDataChanged =  true;
                    }
            }
        }

        private void OnEnable()
        {
            Awake();
            SubscribeToChanges();
        }

        private void OnDisable()
        {
            OnDestroy();
            UnsubscribeFromChanges();
        }
#else
        private void OnEnable() => SubscribeToChanges();
        
        private void OnDisable() => UnsubscribeFromChanges();
#endif
        private void SubscribeToChanges()
        {
            foreach (RaymarchingOperation operation in operations)
                operation.Changed += UpdateOperationDataInShader;
            foreach (RaymarchedObject obj in objects)
                obj.Changed += UpdateObjectDataInShader;
        }
        private void UnsubscribeFromChanges()
        {
            foreach (RaymarchingOperation operation in operations)
                operation.Changed -= UpdateOperationDataInShader;
            foreach (RaymarchedObject obj in objects)
                obj.Changed -= UpdateObjectDataInShader;
        }
        
        private void UpdateOperationDataInShader(RaymarchingOperation operation)
        {
            if (!_changedOperations.Contains(operation))
                _changedOperations.Add(operation);
            _isOperationsDataChanged = true;
        }

        private void UpdateObjectDataInShader(RaymarchedObject obj)
        {
            if (!_changedObjects.Contains(obj))
                _changedObjects.Add(obj);
            _isObjectsDataChanged = true;
        }

        private void Update()
        {
            if (_isOperationsDataChanged)
                UpdateShaderOperationsData();
            if (_isObjectsDataChanged)
                UpdateShaderObjectsData();
        }

        private void UpdateShaderOperationsData()
        {
            foreach (RaymarchingOperation changedOperation in _changedOperations)
            {
                int index = operations.IndexOf(changedOperation);
                _operationsData[index] = changedOperation.ShaderData;
            }
            _changedOperations.Clear();

            _operationsBuffer.SetData(_operationsData);
            Shader.SetGlobalInteger(RaymarchingOperationsCount, _operationsData.Count);
            Shader.SetGlobalBuffer (RaymarchingOperations     , _operationsBuffer    );

            _isOperationsDataChanged = false;
        }

        private void UpdateShaderObjectsData()
        {
            foreach (RaymarchedObject changedObject in _changedObjects)
            {
                int index = objects.IndexOf(changedObject);
                _objectsData[index] = changedObject.ShaderData;
            }
            _changedObjects.Clear();

            _objectsBuffer.SetData(_objectsData);
            Shader.SetGlobalBuffer(RaymarchedObjects, _objectsBuffer);

            _isObjectsDataChanged = false;
        }
    }
}