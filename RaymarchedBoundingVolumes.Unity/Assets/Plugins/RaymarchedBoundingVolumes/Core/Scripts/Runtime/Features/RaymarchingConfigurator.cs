using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Data.Static;
using RaymarchedBoundingVolumes.Utilities.Extensions;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
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

        private RaymarchingOperationShaderData _unionOperationForTail = new()
        {
            _operationType = (int)RaymarchingOperationType.Union,
            _childCount    = default,
            _blendStrength = default,
        };

        private bool _isOperationsDataChanged;
        private bool _isObjectsDataChanged;

        private void Awake()
        {
            int operationsBufferSize = operations.Count + dynamicallyCreatedOperationsCount + 1;
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
            _objectsData = new List<RaymarchedObjectShaderData>(objectsBufferSize);
            foreach (RaymarchedObject obj in objects)
                _objectsData.Add(obj.ShaderData);
            
            _operationsData = new List<RaymarchingOperationShaderData>(operationsBufferSize);
            foreach (RaymarchingOperation operation in operations)
                _operationsData.Add(operation.ShaderData);

            AddUnionOperationForObjectsWithNoOperation();
        }

        private void AddUnionOperationForObjectsWithNoOperation()
        {
            if (_unionOperationForTail._childCount > 0)
            {
                if (!_operationsData.Contains(_unionOperationForTail))
                    _operationsData.Add(_unionOperationForTail);
            }
            else if (_operationsData.Contains(_unionOperationForTail))
                    _operationsData.Remove(_unionOperationForTail);
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

            RegisterObjectWithNoOperation(rootGameObjects);

            if (_isOperationsDataChanged || _isObjectsDataChanged)
                UnityEditor.EditorUtility.SetDirty(this);
        }

        private void RegisterObjectWithNoOperation(GameObject[] rootGameObjects)
        {
            foreach (GameObject rootGameObject in rootGameObjects)
                RegisterObjects(rootGameObject.GetComponentsInChildren<RaymarchedObject>());
        }

        private void RegisterOperationsAndItsObjects(IEnumerable<RaymarchingOperation> raymarchingOperations)
        {
            foreach (RaymarchingOperation newOperation in raymarchingOperations)
                RegisterOperationAndItsObjects(newOperation);
        }
        private void RegisterOperationAndItsObjects(RaymarchingOperation operation)
        {
            RegisterOperation(operation);
            RegisterObjects(operation.GetComponentsInChildren<RaymarchedObject>());
        }
        private void RegisterOperation(RaymarchingOperation operation)
        {
            if (!operations.Contains(operation))
            {
                operations.Add(operation);
                operation.Changed     += UpdateOperationDataInShader;
                _isOperationsDataChanged =  true;
            }
        }

        private void RegisterObjects(IEnumerable<RaymarchedObject> raymarchedObjects)
        {
            foreach (RaymarchedObject raymarchedObject in raymarchedObjects)
                RegisterObject(raymarchedObject);
        }
        private void RegisterObject(RaymarchedObject raymarchedObject)
        {
            if (!objects.Contains(raymarchedObject))
            {
                objects.Add(raymarchedObject);
                raymarchedObject.Changed       += UpdateObjectDataInShader;
                raymarchedObject.ParentChanged += HandleObjectParentChange;
                _isObjectsDataChanged          =  true;
            }
        }

        private void OnEnable()
        {
            Awake();
            UnsubscribeFromChanges();
            SubscribeToChanges();
        }

        private void OnDisable()
        {
            OnDestroy();
            UnsubscribeFromChanges();
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
            foreach (RaymarchingOperation operation in operations)
                operation.Changed += UpdateOperationDataInShader;
            foreach (RaymarchedObject obj in objects)
            {
                obj.Changed       += UpdateObjectDataInShader;
                obj.ParentChanged += HandleObjectParentChange;
            }
        }

        private void UnsubscribeFromChanges()
        {
            foreach (RaymarchingOperation operation in operations)
                operation.Changed -= UpdateOperationDataInShader;
            foreach (RaymarchedObject obj in objects)
            {
                obj.Changed       -= UpdateObjectDataInShader;
                obj.ParentChanged -= HandleObjectParentChange;
            }
        }

        private void HandleObjectParentChange(RaymarchedObject obj, IChangedValue<Transform> parent)
        {
            var oldOperation   = parent.Old?.GetComponentInParent<RaymarchingOperation>();
            var newOperation   = parent.New?.GetComponentInParent<RaymarchingOperation>();
            int oldObjectIndex = objects.IndexOf(obj);
            objects.RemoveAt(oldObjectIndex);

            if (oldOperation != default)
                UpdateOperationDataInShader(oldOperation);
            else
                _unionOperationForTail._childCount = Mathf.Max(default, _unionOperationForTail._childCount - 1);

            int newObjectIndex;
            if (newOperation != default)
            {
                newObjectIndex = default;
                for (var i = 0; i < operations.IndexOf(newOperation); i++)
                    newObjectIndex += operations[i].RaymarchedChildCount;

                objects.Insert(newObjectIndex, obj);
                UpdateOperationDataInShader(newOperation);
            }
            else
            {
                objects.Add(obj);
                newObjectIndex = objects.LastIndex();
                _unionOperationForTail._childCount++;
            }

            int startIndex = Mathf.Min(oldObjectIndex, newObjectIndex);
            int endIndex   = Mathf.Max(oldObjectIndex, newObjectIndex);
            for (int i = startIndex; i <= endIndex; i++)
                UpdateObjectDataInShader(objects[i]);
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
            AddUnionOperationForObjectsWithNoOperation();
            
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