using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Dynamic;
using UnityEngine;
using static RaymarchedBoundingVolumes.Data.Static.RaymarchedObjectShaderPropertyIds;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class ShaderDataUpdater : IShaderDataUpdater
    {
        private readonly List<RaymarchedObject>     _changedObjects    = new();
        private readonly List<RaymarchingOperation> _changedOperations = new();

        private readonly IRaymarchingSceneDataProvider _dataProvider;

        private bool _isObjectsDataChanged;
        private bool _isOperationNodesDataChanged;
        private bool _isOperationsDataChanged;

        private ShaderBuffers _shaderBuffers;

        public ShaderDataUpdater(IRaymarchingSceneDataProvider dataProvider) => _dataProvider = dataProvider;

        public IShaderDataUpdater Initialize(ShaderBuffers shaderBuffers)
        {
            _shaderBuffers               = shaderBuffers;
            _isOperationNodesDataChanged = true;
            _isOperationsDataChanged     = true;
            _isObjectsDataChanged        = true;
            return this;
        }

        public IShaderDataUpdater SubscribeToFeatureEvents()
        {
            foreach (RaymarchingOperation operation in _dataProvider.Data.Operations)
                operation.Changed += UpdateOperationData;
            foreach (RaymarchedObject obj in _dataProvider.Data.Objects)
                obj.Changed += UpdateObjectData;
            return this;
        }

        public IShaderDataUpdater UnsubscribeToFeatureEvents()
        {
            foreach (RaymarchingOperation operation in _dataProvider.Data.Operations)
                operation.Changed -= UpdateOperationData;
            foreach (RaymarchedObject obj in _dataProvider.Data.Objects)
                obj.Changed -= UpdateObjectData;
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
    }
}