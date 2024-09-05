using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Features.ShaderDataForming;
using UnityEngine;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public class ShaderDataUpdater : IShaderDataUpdater
    {
        private readonly IRaymarchingSceneDataProvider             _dataProvider;
        private readonly IRaymarchedObjectShaderPropertyIdProvider _objectShaderPropertyIdProvider;

        private readonly List<RaymarchingOperation> _changedOperations = new();
        private readonly List<RaymarchedObject>     _changedObjects    = new();

        private readonly Dictionary<TransformType, List<int>>        _changedObjectTransformData = new();
        private readonly Dictionary<RaymarchedObjectType, List<int>> _changedObjectTypeData      = new();

        private ShaderBuffers _shaderBuffers;

        private bool _isOperationNodesDataChanged;
        private bool _isOperationsDataChanged;
        private bool _isObjectsDataChanged;

        private Dictionary<TransformType, bool>        _isObjectTransformDataChanged;
        private Dictionary<RaymarchedObjectType, bool> _isObjectTypeDataChanged;

        public ShaderDataUpdater(IRaymarchingSceneDataProvider             dataProvider,
                                 IRaymarchedObjectShaderPropertyIdProvider objectShaderPropertyIdProvider)
        {
            _objectShaderPropertyIdProvider = objectShaderPropertyIdProvider;
            _dataProvider                   = dataProvider;
        }

        public IShaderDataUpdater Initialize(ShaderBuffers shaderBuffers)
        {
            _shaderBuffers = shaderBuffers;

            _isOperationNodesDataChanged = true;
            _isOperationsDataChanged     = true;
            _isObjectsDataChanged        = true;
            _isObjectTransformDataChanged =
                _dataProvider.Data.ObjectsByTransformsType.Keys.ToDictionary(type => type, type => true);
            _isObjectTypeDataChanged =
                _dataProvider.Data.ObjectsByType.Keys.ToDictionary(type => type, type => true);

            _changedObjects.Clear();
            _changedOperations.Clear();
            _changedObjectTypeData.Clear();
            return this;
        }

        public IShaderDataUpdater SubscribeToFeatureEvents()
        {
            foreach (RaymarchingOperation operation in _dataProvider.Data.Operations)
                operation.Changed += UpdateOperationData;
            foreach (RaymarchedObject obj in _dataProvider.Data.Objects)
            {
                obj.Changed          += UpdateObjectData;
                obj.TransformChanged += UpdateTransformData;
                obj.TypeDataChanged  += UpdateObjectTypeData;
            }

            return this;
        }

        public IShaderDataUpdater UnsubscribeToFeatureEvents()
        {
            foreach (RaymarchingOperation operation in _dataProvider.Data.Operations)
                operation.Changed -= UpdateOperationData;
            foreach (RaymarchedObject obj in _dataProvider.Data.Objects)
            {
                obj.Changed          -= UpdateObjectData;
                obj.TransformChanged -= UpdateTransformData;
                obj.TypeDataChanged  -= UpdateObjectTypeData;
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
            foreach (TransformType type in _dataProvider.Data.ObjectsByTransformsType.Keys)
                if (_isObjectTransformDataChanged[type])
                    UpdateObjectsTransformData(type);
            foreach (RaymarchedObjectType type in _dataProvider.Data.ObjectsByType.Keys)
                if (_isObjectTypeDataChanged[type])
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

        private void UpdateTransformData(RaymarchedObject obj)
        {
            if (!_changedObjectTransformData.ContainsKey(obj.TransformType))
                _changedObjectTransformData.Add(obj.TransformType, new List<int>());

            if (!_changedObjectTransformData[obj.TransformType].Contains(obj.TransformDataIndex))
                _changedObjectTransformData[obj.TransformType].Add(obj.TransformDataIndex);

            _isObjectTransformDataChanged[obj.TransformType] = true;
        }

        private void UpdateObjectTypeData(RaymarchedObject obj)
        {
            RaymarchedObjectType objectType = obj.Type.Value;
            if (!_changedObjectTypeData.ContainsKey(objectType))
                _changedObjectTypeData.Add(objectType, new List<int>());

            if (!_changedObjectTypeData[objectType].Contains(obj.TypeDataIndex))
                _changedObjectTypeData[objectType].Add(obj.TypeDataIndex);

            _isObjectTypeDataChanged[objectType] = true;
        }

        private void UpdateOperationNodesData()
        {
            _shaderBuffers.OperationNodes.SetData(_dataProvider.Data.OperationNodesShaderData);
            Shader.SetGlobalBuffer(_objectShaderPropertyIdProvider.RaymarchingOperationNodes,
                _shaderBuffers.OperationNodes);

            _isOperationNodesDataChanged = false;
        }

        private void UpdateOperationsData()
        {
            foreach (RaymarchingOperation changedOperation in _changedOperations)
                foreach (int index in _dataProvider.Data.OperationDataIndexes[changedOperation])
                    _dataProvider.Data.OperationsShaderData[index] = changedOperation.ShaderData;
            _changedOperations.Clear();

            _shaderBuffers.Operations.SetData(_dataProvider.Data.OperationsShaderData);
            Shader.SetGlobalInteger(_objectShaderPropertyIdProvider.RaymarchingOperationsCount,
                _dataProvider.Data.OperationsShaderData.Count);
            Shader.SetGlobalBuffer(_objectShaderPropertyIdProvider.RaymarchingOperations,
                _shaderBuffers.Operations);

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
            Shader.SetGlobalBuffer(_objectShaderPropertyIdProvider.RaymarchedObjects, _shaderBuffers.Objects);

            _isObjectsDataChanged = false;
        }

        private void UpdateObjectsTransformData(TransformType type)
        {
            if (_changedObjectTransformData.ContainsKey(type))
            {
                foreach (int transformDataIndex in _changedObjectTransformData[type])
                {
                    RaymarchedObject raymarchedObject =
                        _dataProvider.Data.ObjectsByTransformsType[type][transformDataIndex];
                    _dataProvider.Data.ObjectTransformsShaderDataByType[type]
                        .SetValue(raymarchedObject.TransformShaderData, transformDataIndex);
                }

                _changedObjectTransformData[type].Clear();
            }

            _shaderBuffers.ObjectTransformData[type]
                .SetData(_dataProvider.Data.ObjectTransformsShaderDataByType[type]);
            Shader.SetGlobalBuffer(_objectShaderPropertyIdProvider.ObjectTransformDataIds[type],
                _shaderBuffers.ObjectTransformData[type]);

            _isObjectTransformDataChanged[type] = false;
        }

        private void UpdateObjectsTypeRelatedData(RaymarchedObjectType type)
        {
            if (_changedObjectTypeData.ContainsKey(type))
            {
                foreach (int typeRelatedDataIndex in _changedObjectTypeData[type])
                {
                    RaymarchedObject raymarchedObject = _dataProvider.Data.ObjectsByType[type][typeRelatedDataIndex];
                    IObjectTypeShaderData shaderData = raymarchedObject.TypeShaderData;
                    _dataProvider.Data.ObjectsShaderDataByType[type].SetValue(shaderData, typeRelatedDataIndex);
                }

                _changedObjectTypeData[type].Clear();
            }

            _shaderBuffers.ObjectTypeRelatedData[type].SetData(_dataProvider.Data.ObjectsShaderDataByType[type]);
            Shader.SetGlobalBuffer(_objectShaderPropertyIdProvider.ObjectTypeRelatedDataIds[type],
                _shaderBuffers.ObjectTypeRelatedData[type]);

            _isObjectTypeDataChanged[type] = false;
        }
    }
}