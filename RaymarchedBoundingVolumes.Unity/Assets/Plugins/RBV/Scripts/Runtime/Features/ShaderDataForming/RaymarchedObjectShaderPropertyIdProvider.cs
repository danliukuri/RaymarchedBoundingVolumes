using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Static.Enumerations;
using UnityEngine;

namespace RBV.Features.ShaderDataForming
{
    public class RaymarchedObjectShaderPropertyIdProvider : IRaymarchedObjectShaderPropertyIdProvider
    {
        private const string Prefix                      = "_";
        private const string OperationTypeDataIdFormat   = "Raymarching{0}OperationData";
        private const string ObjectTypeDataIdFormat      = "Raymarched{0}Data";
        private const string ObjectTransformDataIdFormat = "RaymarchedObjects{0}Transforms";

        private readonly Lazy<Dictionary<RaymarchingOperationType, int>> _operationTypeDataIds;
        private readonly Lazy<Dictionary<TransformType, int>>            _objectTransformDataIds;
        private readonly Lazy<Dictionary<RaymarchedObjectType, int>>     _objectTypeDataIds;

        public RaymarchedObjectShaderPropertyIdProvider()
        {
            _operationTypeDataIds =
                new Lazy<Dictionary<RaymarchingOperationType, int>>(CreateShaderIdsForOperationTypes);
            _objectTransformDataIds = new Lazy<Dictionary<TransformType, int>>(CreateShaderIdsForTransformTypes);
            _objectTypeDataIds      = new Lazy<Dictionary<RaymarchedObjectType, int>>(CreateShaderIdsForObjectTypes);
        }

        public Dictionary<RaymarchingOperationType, int> OperationTypeDataIds   => _operationTypeDataIds.Value;
        public Dictionary<TransformType, int>            ObjectTransformDataIds => _objectTransformDataIds.Value;
        public Dictionary<RaymarchedObjectType, int>     ObjectTypeDataIds      => _objectTypeDataIds.Value;

        public int RaymarchingOperationsCount { get; } = PropertyToID(nameof(RaymarchingOperationsCount));
        public int RaymarchingOperationNodes  { get; } = PropertyToID(nameof(RaymarchingOperationNodes));
        public int RaymarchingOperations      { get; } = PropertyToID(nameof(RaymarchingOperations));
        public int RaymarchedObjects          { get; } = PropertyToID(nameof(RaymarchedObjects));

        public int RaymarchedObjectsRenderingSettings { get; } =
            PropertyToID(nameof(RaymarchedObjectsRenderingSettings));

        private static int PropertyToID(string name) => Shader.PropertyToID(Prefix + name);

        private Dictionary<RaymarchingOperationType, int> CreateShaderIdsForOperationTypes() =>
            Enum.GetValues(typeof(RaymarchingOperationType)).Cast<object>()
                .ToDictionary(type => (RaymarchingOperationType)type,
                    type => PropertyToID(string.Format(OperationTypeDataIdFormat, type)));

        private Dictionary<TransformType, int> CreateShaderIdsForTransformTypes() =>
            Enum.GetValues(typeof(TransformType)).Cast<TransformType>()
                .ToDictionary(type => type, type => PropertyToID(string.Format(ObjectTransformDataIdFormat, type)));

        private Dictionary<RaymarchedObjectType, int> CreateShaderIdsForObjectTypes() => GetObjectTypes()
            .SelectMany(type => Enum.GetValues(type).Cast<object>())
            .ToDictionary(type => (RaymarchedObjectType)(int)type,
                type => PropertyToID(string.Format(ObjectTypeDataIdFormat, type)));

        protected virtual List<Type> GetObjectTypes() => new() { typeof(RaymarchedObject3DType) };
    }
}