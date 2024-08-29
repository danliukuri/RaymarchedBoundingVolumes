using System.Linq;
using RBV.Data.Dynamic;
using UnityEngine;

namespace RBV.Features
{
    public class RaymarchingChildrenCalculator : IRaymarchingChildrenCalculator
    {
        private RaymarchingOperation _parentOperation;
        private Transform            _transform;

        public OperationChildrenData CalculateChildrenCount(RaymarchingOperation operation)
        {
            _parentOperation = operation;
            _transform       = operation.transform;

            return CalculateChildrenCount();
        }

        private OperationChildrenData CalculateChildrenCount()
        {
            var children = new OperationChildrenData
            {
                TotalObjectsCount     = CalculateTotalChildObjectsCount(),
                TotalOperationsCount  = CalculateTotalChildOperationsCount(),
                TotalFeaturesCount    = default,
                DirectObjectsCount    = CalculateDirectChildObjectsCount(),
                DirectOperationsCount = CalculateDirectChildOperationsCount(),
                DirectFeaturesCount   = default
            };
            children.TotalFeaturesCount  = children.TotalObjectsCount  + children.TotalOperationsCount;
            children.DirectFeaturesCount = children.DirectObjectsCount + children.DirectOperationsCount;

            return children;
        }

        private int CalculateTotalChildObjectsCount() => _transform
            .GetComponentsInChildren<RaymarchedObject>().Length;

        private int CalculateTotalChildOperationsCount() => _transform
            .GetComponentsInChildren<RaymarchingOperation>().Count(operation => operation != _parentOperation);

        private int CalculateDirectChildObjectsCount() => _transform
            .GetComponentsInChildren<RaymarchedObject>()
            .Count(child => child.GetComponentInParent<RaymarchingOperation>() == _parentOperation);

        private int CalculateDirectChildOperationsCount() => _transform
            .GetComponentsInChildren<RaymarchingOperation>().Count(operation =>
                operation != _parentOperation && operation.GetComponentsInParent<RaymarchingOperation>()
                    .SkipWhile(component => component == operation).First() == _parentOperation);
    }
}