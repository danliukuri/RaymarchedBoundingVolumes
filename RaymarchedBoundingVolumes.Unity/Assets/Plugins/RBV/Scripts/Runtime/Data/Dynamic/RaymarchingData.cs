using System;
using System.Collections.Generic;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Static.Enumerations;
using RBV.Features;
using UnityEngine;

namespace RBV.Data.Dynamic
{
    [Serializable]
    public class RaymarchingData
    {
        [field: SerializeField, HideInInspector] public List<RaymarchingFeature> Features { get; set; } = new();

        public List<RaymarchingOperation> Operations { get; set; } = new();
        public List<RaymarchedObject>     Objects    { get; set; } = new();

        public Dictionary<TransformType, List<RaymarchedObject>> ObjectsByTransformsType          { get; set; } = new();
        public Dictionary<TransformType, Array>                  ObjectTransformsShaderDataByType { get; set; } = new();


        public Dictionary<RaymarchingOperationType, List<RaymarchingOperation>> OperationsByType { get; set; } = new();
        public Dictionary<RaymarchingOperationType, Array> OperationsShaderDataByType { get; set; } = new();

        public Dictionary<RaymarchedObjectType, List<RaymarchedObject>> ObjectsByType           { get; set; } = new();
        public Dictionary<RaymarchedObjectType, Array>                  ObjectsShaderDataByType { get; set; } = new();

        public List<OperationMetaData>                     OperationMetaData    { get; set; }
        public Dictionary<RaymarchingOperation, List<int>> OperationDataIndexes { get; set; }
        public Dictionary<RaymarchedObject, int>           ObjectDataIndexes    { get; set; }

        public List<RaymarchingOperationNodeShaderData> OperationNodesShaderData { get; set; }
        public List<RaymarchingOperationShaderData>     OperationsShaderData     { get; set; }
        public List<RaymarchedObjectShaderData>         ObjectsShaderData        { get; set; }

        public List<RaymarchedObjectRenderingSettingsShaderData> ObjectsRenderingSettingsShaderData { get; set; }
    }
}