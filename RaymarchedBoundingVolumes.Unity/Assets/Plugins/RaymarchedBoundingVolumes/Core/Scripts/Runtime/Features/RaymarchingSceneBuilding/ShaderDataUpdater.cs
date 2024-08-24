using System.Collections.Generic;
using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Data.Static.Enumerations;
using UnityEngine;
using static RaymarchedBoundingVolumes.Data.Static.RaymarchedObjectShaderPropertyIds;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class ShaderDataUpdater : IShaderDataUpdater
    {
        private readonly IRaymarchingSceneDataProvider _dataProvider;

        private readonly List<RaymarchingOperation> _changedOperations = new();
        private readonly List<RaymarchedObject>     _changedObjects    = new();

        private readonly Dictionary<RaymarchedObjectType, List<int>> _changedObjectTypeRelatedData = new();

        private ShaderBuffers _shaderBuffers;

        private bool _isOperationNodesDataChanged;
        private bool _isOperationsDataChanged;
        private bool _isObjectsDataChanged;

        private Dictionary<RaymarchedObjectType, bool> _isObjectTypeRelatedDataChanged;

        public ShaderDataUpdater(IRaymarchingSceneDataProvider dataProvider) => _dataProvider = dataProvider;

        public IShaderDataUpdater Initialize(ShaderBuffers shaderBuffers)
        {
            _shaderBuffers = shaderBuffers;

            _isOperationNodesDataChanged = true;
            _isOperationsDataChanged     = true;
            _isObjectsDataChanged        = true;
            _isObjectTypeRelatedDataChanged =
                _dataProvider.Data.ObjectsByType.Keys.ToDictionary(type => type, type => true);

            _changedObjects.Clear();
            _changedOperations.Clear();
            _changedObjectTypeRelatedData.Clear();
            return this;
        }

        public IShaderDataUpdater SubscribeToFeatureEvents()
        {
            foreach (RaymarchingOperation operation in _dataProvider.Data.Operations)
                operation.Changed += UpdateOperationData;
            foreach (RaymarchedObject obj in _dataProvider.Data.Objects)
            {
                obj.Changed                += UpdateObjectData;
                obj.TypeRelatedDataChanged += UpdateObjectTypeRelatedData;
            }

            return this;
        }

        public IShaderDataUpdater UnsubscribeToFeatureEvents()
        {
            foreach (RaymarchingOperation operation in _dataProvider.Data.Operations)
                operation.Changed -= UpdateOperationData;
            foreach (RaymarchedObject obj in _dataProvider.Data.Objects)
            {
                obj.Changed                -= UpdateObjectData;
                obj.TypeRelatedDataChanged -= UpdateObjectTypeRelatedData;
            }

            return this;
        }

        public IShaderDataUpdater Update()
        {
            if (_isOperationNodesDataChanged)
                UpdateOperationNodesData();
            if (_isOperationsDataChanged)
                UpdateOperationsData();
            if (_isObjectsDataChanged)
                UpdateObjectsData();
            foreach (RaymarchedObjectType type in _dataProvider.Data.ObjectsByType.Keys)
                if (_isObjectTypeRelatedDataChanged[type])
                    UpdateObjectsTypeRelatedData(type);
            return this;
        }

        private void UpdateOperationData(RaymarchingOperation operation)
        {
            if (!_changedOperations.Contains(operation))
                _changedOperations.Add(operation);
            _isOperationsDataChanged = true;
        }

        private void UpdateObjectData(RaymarchedObject obj)
        {
            if (!_changedObjects.Contains(obj))
                _changedObjects.Add(obj);
            _isObjectsDataChanged = true;
        }

        private void UpdateObjectTypeRelatedData(RaymarchedObject obj)
        {
            RaymarchedObjectType objectType = obj.Type.Value;
            if (!_changedObjectTypeRelatedData.ContainsKey(objectType))
                _changedObjectTypeRelatedData.Add(objectType, new List<int>());

            if (!_changedObjectTypeRelatedData[objectType].Contains(obj.TypeRelatedDataIndex))
                _changedObjectTypeRelatedData[objectType].Add(obj.TypeRelatedDataIndex);

            _isObjectTypeRelatedDataChanged[objectType] = true;
        }

        private void UpdateOperationNodesData()
        {
            _shaderBuffers.OperationNodes.SetData(_dataProvider.Data.OperationNodesShaderData);
            Shader.SetGlobalBuffer(RaymarchingOperationNodes, _shaderBuffers.OperationNodes);

            _isOperationNodesDataChanged = false;
        }

        private void UpdateOperationsData()
        {
            foreach (RaymarchingOperation changedOperation in _changedOperations)
                foreach (int index in _dataProvider.Data.OperationDataIndexes[changedOperation])
                    _dataProvider.Data.OperationsShaderData[index] = changedOperation.ShaderData;
            _changedOperations.Clear();

            _shaderBuffers.Operations.SetData(_dataProvider.Data.OperationsShaderData);
            Shader.SetGlobalInteger(RaymarchingOperationsCount, _dataProvider.Data.OperationsShaderData.Count);
            Shader.SetGlobalBuffer(RaymarchingOperations, _shaderBuffers.Operations);

            _isOperationsDataChanged = false;
        }

        private void UpdateObjectsData()
        {
            foreach (RaymarchedObject changedObject in _changedObjects)
            {
                int index = _dataProvider.Data.ObjectDataIndexes[changedObject];
                _dataProvider.Data.ObjectsShaderData[index] = changedObject.ShaderData;
            }

            _changedObjects.Clear();

            _shaderBuffers.Objects.SetData(_dataProvider.Data.ObjectsShaderData);
            Shader.SetGlobalBuffer(RaymarchedObjects, _shaderBuffers.Objects);

            _isObjectsDataChanged = false;
        }

        private void UpdateObjectsTypeRelatedData(RaymarchedObjectType type)
        {
            if (_changedObjectTypeRelatedData.ContainsKey(type))
            {
                foreach (int typeRelatedDataIndex in _changedObjectTypeRelatedData[type])
                {
                    RaymarchedObject raymarchedObject = _dataProvider.Data.ObjectsByType[type][typeRelatedDataIndex];
                    object           shaderData       = raymarchedObject.TypeRelatedShaderData;
                    _dataProvider.Data.ObjectsShaderDataByType[type].SetValue(shaderData, typeRelatedDataIndex);
                }

                _changedObjectTypeRelatedData[type].Clear();
            }

            _shaderBuffers.ObjectTypeRelatedData[type].SetData(_dataProvider.Data.ObjectsShaderDataByType[type]);
            Shader.SetGlobalBuffer(ObjectTypeRelatedDataIds[type], _shaderBuffers.ObjectTypeRelatedData[type]);

            _isObjectTypeRelatedDataChanged[type] = false;
        }
    }
}