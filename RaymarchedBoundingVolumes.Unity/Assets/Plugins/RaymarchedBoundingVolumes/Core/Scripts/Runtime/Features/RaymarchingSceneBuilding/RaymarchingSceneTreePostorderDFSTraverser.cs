using System.Collections.Generic;
using System.Linq;
using RaymarchedBoundingVolumes.Data.Dynamic;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Utilities.Extensions;

namespace RaymarchedBoundingVolumes.Features.RaymarchingSceneBuilding
{
    public class RaymarchingSceneTreePostorderDFSTraverser : IRaymarchingSceneTreeTraverser
    {
        private const int DirectChildObjectsCountForObjectWrappingOperation = 1;

        private List<RaymarchingFeature> _features;

        private Stack<OperationMetaData> _operationsMetaDataInPostorder;
        private Stack<OperationMetaData> _operationsMetaDataInPreorder;
            
        public List<OperationMetaData> TraverseScene(List<RaymarchingFeature> features)
        {
            _features                      = features;
            _operationsMetaDataInPreorder  = new Stack<OperationMetaData>(_features.Count);
            _operationsMetaDataInPostorder = new Stack<OperationMetaData>(_features.Count);

            TraverseSceneInPostorderDFS();
            return _operationsMetaDataInPostorder.ToList();
        }

        private void TraverseSceneInPostorderDFS()
        {
            TraverseChildrenOperationsInPreorderDFS(new OperationMetaData
            {
                Index       = RaymarchingOperationNodeShaderData.RootNodeIndex,
                ParentIndex = RaymarchingOperationNodeShaderData.RootNodeIndex,
                Layer       = RaymarchingOperationNodeShaderData.RootLayer
            }, _features.Count);

            while (_operationsMetaDataInPreorder.TryPop(out OperationMetaData operationData))
            {
                _operationsMetaDataInPostorder.Push(operationData);

                int childFeaturesCount = CalculateChildOperationsCount(operationData.Index);
                TraverseChildrenOperationsInPreorderDFS(operationData, childFeaturesCount);
            }
        }

        private void TraverseChildrenOperationsInPreorderDFS(OperationMetaData operationData, int childFeaturesCount)
        {
            int nextIndex = _features.NextIndex(operationData.Index);
            for (int childIndex = nextIndex; childIndex < nextIndex + childFeaturesCount; childIndex++)
            {
                _operationsMetaDataInPreorder.Push(new OperationMetaData
                {
                    Index       = childIndex,
                    ParentIndex = operationData.Index,
                    Layer       = RaymarchingOperationNodeShaderData.NextLayer(operationData.Layer),

                    DirectChildObjectsCount    = CalculateDirectChildObjectsCount(childIndex),
                    DirectChildOperationsCount = CalculateDirectChildOperationsCount(childIndex)
                });

                childIndex += SkipGrandchildren(childIndex);;
            }
        }

        private int CalculateChildOperationsCount(int index) =>
            _features[index] is RaymarchingOperation operation && operation.ChildOperationsCount != default
                ? operation.ChildOperationsCount + operation.ChildObjectsCount
                : default;

        private int CalculateDirectChildOperationsCount(int index) =>
            _features[index] is RaymarchingOperation operation && operation.ChildOperationsCount != default
                ? operation.DirectChildOperationsCount + operation.DirectChildObjectsCount
                : default;

        private int CalculateDirectChildObjectsCount(int index) =>
            _features[index] switch
            {
                RaymarchingOperation operation => operation.ChildOperationsCount == default
                    ? operation.DirectChildObjectsCount
                    : default,
                RaymarchedObject => DirectChildObjectsCountForObjectWrappingOperation,
                _                => default
            };
        
        private int SkipGrandchildren(int childIndex)
        {
            int grandchildrenCount = default;
            if (_features[childIndex] is RaymarchingOperation operation)
            {
                grandchildrenCount = operation.DirectChildOperationsCount + operation.DirectChildObjectsCount;

                int nextChildIndex = _features.NextIndex(childIndex);
                for (int i = nextChildIndex; i < nextChildIndex + grandchildrenCount; i++)
                    if (_features[i] is RaymarchingOperation childOperation)
                        grandchildrenCount +=
                            childOperation.DirectChildOperationsCount + childOperation.DirectChildObjectsCount;
            }

            return grandchildrenCount;
        }
    }
}