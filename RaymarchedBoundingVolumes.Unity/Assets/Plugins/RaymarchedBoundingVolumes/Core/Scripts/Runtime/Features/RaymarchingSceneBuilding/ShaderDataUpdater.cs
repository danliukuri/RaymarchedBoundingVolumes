using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Dynamic;
using UnityEngine;
using static RaymarchedBoundingVolumes.Data.Static.RaymarchedObjectShaderPropertyIds;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class ShaderDataUpdater : IShaderDataUpdater
    {
        private bool _isOperationNodesDataChanged;
        private bool _isOperationsDataChanged;
        private bool _isObjectsDataChanged;

        private readonly List<RaymarchingOperation> _changedOperations = new();
        private readonly List<RaymarchedObject>     _changedObjects    = new();

        private ShaderBuffers   _shaderBuffers;
        private RaymarchingData _raymarchingData;

        public ShaderDataUpdater Initialize(ShaderBuffers shaderBuffers, RaymarchingData raymarchingData)
        {
            _shaderBuffers               = shaderBuffers;
            _raymarchingData             = raymarchingData;
            _isOperationNodesDataChanged = true;
            _isOperationsDataChanged     = true;
            _isObjectsDataChanged        = true;
            return this;
        }

        public void UpdateOperationData(RaymarchingOperation operation)
        {
            if (!_changedOperations.Contains(operation))
                _changedOperations.Add(operation);
            _isOperationsDataChanged = true;
        }
        public void UpdateObjectData(RaymarchedObject obj)
        {
            if (!_changedObjects.Contains(obj))
                _changedObjects.Add(obj);
            _isObjectsDataChanged = true;
        }

        public void Update()
        {
            if (_isOperationNodesDataChanged)
                UpdateOperationNodesData();
            if (_isOperationsDataChanged)
                UpdateOperationsData();
            if (_isObjectsDataChanged)
                UpdateObjectsData();
        }

        private void UpdateOperationNodesData()
        {
            _shaderBuffers.OperationNodes.SetData(_raymarchingData.OperationNodesShaderData);
            Shader.SetGlobalBuffer(RaymarchingOperationNodes, _shaderBuffers.OperationNodes);

            _isOperationNodesDataChanged = false;
        }

        private void UpdateOperationsData()
        {
            foreach (RaymarchingOperation changedOperation in _changedOperations)
                foreach (int index in _raymarchingData.OperationDataIndexes[changedOperation])
                    _raymarchingData.OperationsShaderData[index] = changedOperation.ShaderData;
            _changedOperations.Clear();

            _shaderBuffers.Operations.SetData(_raymarchingData.OperationsShaderData);
            Shader.SetGlobalInteger(RaymarchingOperationsCount, _raymarchingData.OperationsShaderData.Count);
            Shader.SetGlobalBuffer (RaymarchingOperations     , _shaderBuffers.Operations);

            _isOperationsDataChanged = false;
        }

        private void UpdateObjectsData()
        {
            foreach (RaymarchedObject changedObject in _changedObjects)
            {
                int index = _raymarchingData.ObjectDataIndexes[changedObject];
                _raymarchingData.ObjectsShaderData[index] = changedObject.ShaderData;
            }
            _changedObjects.Clear();

            _shaderBuffers.Objects.SetData(_raymarchingData.ObjectsShaderData);
            Shader.SetGlobalBuffer(RaymarchedObjects, _shaderBuffers.Objects);

            _isObjectsDataChanged = false;
        }
    }
}