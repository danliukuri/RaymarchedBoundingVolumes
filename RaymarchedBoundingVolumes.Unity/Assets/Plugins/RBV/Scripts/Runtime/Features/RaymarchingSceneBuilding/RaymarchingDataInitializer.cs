using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Static.Enumerations;
using RBV.Features.ShaderDataForming;
using RBV.Utilities.Extensions;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public class RaymarchingDataInitializer : IRaymarchingDataInitializer
    {
        private readonly IRaymarchingSceneTreeTraverser _raymarchingSceneTreeTraverser;
        private readonly IOperationTypeCaster           _operationTypeCaster;
        private readonly IObjectTypeCaster              _objectTypeCaster;
        private readonly ITransformTypeCaster           _transformTypeCaster;

        private RaymarchingData _data;

        private int _objectsIndex;

        private Dictionary<int, int> _parentIndexesByFeatureIndex;

        public RaymarchingDataInitializer(IRaymarchingSceneTreeTraverser raymarchingSceneTreeTraverser,
                                          IOperationTypeCaster           operationTypeCaster,
                                          IObjectTypeCaster              objectTypeCaster,
                                          ITransformTypeCaster           transformTypeCaster)
        {
            _raymarchingSceneTreeTraverser = raymarchingSceneTreeTraverser;
            _operationTypeCaster           = operationTypeCaster;
            _objectTypeCaster              = objectTypeCaster;
            _transformTypeCaster           = transformTypeCaster;
        }

        public RaymarchingData InitializeData(RaymarchingData data)
        {
            _data = data;

            _data.OperationDataIndexes = _data.Features.OfType<RaymarchingOperation>()
                .ToDictionary(operation => operation, operation => new List<int>());
            _data.ObjectDataIndexes = _data.Features.OfType<RaymarchedObject>()
                .ToDictionary(obj => obj, (obj, index) => index);

            _data.OperationMetaData = _raymarchingSceneTreeTraverser.TraverseScene(_data.Features);

            _data.OperationNodesShaderData = new List<RaymarchingOperationNodeShaderData>(_data.Operations.Count);
            _data.OperationsShaderData     = new List<RaymarchingOperationShaderData>(_data.Operations.Count);
            _data.ObjectsShaderData        = new List<RaymarchedObjectShaderData>(_data.Objects.Count);

            _parentIndexesByFeatureIndex =
                _data.OperationMetaData.ToDictionary(metaData => metaData.Index, (metaData, index) => index);
            _parentIndexesByFeatureIndex.Add(RaymarchingOperationNodeShaderData.RootNodeIndex,
                RaymarchingOperationNodeShaderData.RootNodeIndex);

            FillShaderDataByType();
            FillRenderingSettingsShaderData();
            FillTransformShaderData();

            FillTypeDataIndexes();
            FillTransformDataIndexes();
            FillShaderDataLists();

            return _data;
        }

        private void FillTransformShaderData() =>
            _data.ObjectTransformsShaderDataByType = _data.ObjectsByTransformsType
                .ToDictionary(objects => objects.Key, _transformTypeCaster.CastToShaderDataTypeArray);

        private void FillShaderDataByType()
        {
            _data.OperationsShaderDataByType = _data.OperationsByType
                .Where(pair => _operationTypeCaster.HasCorrespondingShaderData(pair.Key))
                .ToDictionary(objects => objects.Key, _operationTypeCaster.CastToShaderDataTypeArray);

            _data.ObjectsShaderDataByType = _data.ObjectsByType
                .ToDictionary(objects => objects.Key, _objectTypeCaster.CastToShaderDataTypeArray);
        }

        private void FillRenderingSettingsShaderData() => _data.ObjectsRenderingSettingsShaderData =
            _data.Objects.Select(obj => obj.RenderingSettings.Value).ToList();

        private void FillTypeDataIndexes()
        {
            foreach ((RaymarchingOperationType type, List<RaymarchingOperation> operations) in _data.OperationsByType)
                if (_data.OperationsShaderDataByType.ContainsKey(type))
                    for (var i = 0; i < operations.Count; i++)
                        operations[i].TypeDataIndex = i;

            foreach (List<RaymarchedObject> objects in _data.ObjectsByType.Values)
                for (var i = 0; i < objects.Count; i++)
                    objects[i].TypeDataIndex = i;
        }

        private void FillTransformDataIndexes()
        {
            foreach (List<RaymarchedObject> objects in _data.ObjectsByTransformsType.Values)
                for (var i = 0; i < objects.Count; i++)
                    objects[i].TransformDataIndex = i;
        }

        private void FillShaderDataLists()
        {
            _objectsIndex = default;
            foreach (OperationMetaData operationData in _data.OperationMetaData)
                switch (_data.Features[operationData.Index])
                {
                    case RaymarchingOperation operation:
                        FillShaderDataForOperation(operation, operationData);
                        break;
                    case RaymarchedObject:
                        FillShaderDataForObject(operationData);
                        break;
                }
        }

        private void FillShaderDataForOperation(RaymarchingOperation operation, OperationMetaData operationData)
        {
            FillShaderData(operation.ShaderData, operationData);
            _data.OperationDataIndexes[operation].Add(_data.OperationsShaderData.LastIndex());
        }

        private void FillShaderDataForObject(OperationMetaData operationData)
        {
            if (operationData.ParentIndex == RaymarchingOperationNodeShaderData.RootNodeIndex)
            {
                FillShaderData(new RaymarchingOperationShaderData(), operationData);
            }
            else if (_data.Features[operationData.ParentIndex] is RaymarchingOperation parent)
            {
                FillShaderData(parent.ShaderData, operationData);
                _data.OperationDataIndexes[parent].Add(_data.OperationsShaderData.LastIndex());
            }
        }

        private void FillShaderData(RaymarchingOperationShaderData shaderData, OperationMetaData operationData)
        {
            _data.OperationNodesShaderData.Add(new RaymarchingOperationNodeShaderData
            {
                ChildObjectsCount    = operationData.DirectChildObjectsCount,
                ChildOperationsCount = operationData.DirectChildOperationsCount,
                ParentIndex          = _parentIndexesByFeatureIndex[operationData.ParentIndex],
                Layer                = operationData.Layer
            });

            _data.OperationsShaderData.Add(shaderData);

            int objectsCount = _objectsIndex + operationData.DirectChildObjectsCount;
            for (int j = _objectsIndex; j < objectsCount; j++, _objectsIndex++)
                _data.ObjectsShaderData.Add(_data.Objects[j].ShaderData);
        }
    }
}