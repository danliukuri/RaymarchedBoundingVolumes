using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Extensions;

namespace RBV.Features.RaymarchingSceneBuilding
{
    public class RaymarchingDataInitializer : IRaymarchingDataInitializer
    {
        private readonly IRaymarchingSceneTreeTraverser _raymarchingSceneTreeTraverser;

        private RaymarchingData _data;

        private int _objectsIndex;

        private Dictionary<int, int> _parentIndexesByFeatureIndex;

        public RaymarchingDataInitializer(IRaymarchingSceneTreeTraverser raymarchingSceneTreeTraverser) =>
            _raymarchingSceneTreeTraverser = raymarchingSceneTreeTraverser;

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

            foreach (List<RaymarchedObject> objects in _data.ObjectsByType.Values)
                FillTypeRelatedIndexes(objects);

            FillShaderDataLists();

            _data.ObjectsShaderDataByType = _data.ObjectsByType.ToDictionary(objects => objects.Key, objects =>
                objects.Key.CastToShaderDataTypeArray(objects.Value.Select(obj => obj.TypeRelatedShaderData)));

            return _data;
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

        private void FillTypeRelatedIndexes(List<RaymarchedObject> objects)
        {
            for (var i = 0; i < objects.Count; i++)
                objects[i].TypeRelatedDataIndex = i;
        }
    }
}