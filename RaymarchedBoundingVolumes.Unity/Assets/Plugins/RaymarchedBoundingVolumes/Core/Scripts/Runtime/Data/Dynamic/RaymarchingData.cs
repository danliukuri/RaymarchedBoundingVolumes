using System;
using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData;
using RaymarchedBoundingVolumes.Features;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Dynamic
{
    [Serializable]
    public class RaymarchingData
    {
        [field: SerializeField, HideInInspector] public List<RaymarchingFeature> Features { get; set; } = new();

        public List<RaymarchingOperation> Operations { get; set; } = new();
        public List<RaymarchedObject>     Objects    { get; set; } = new();

        public List<OperationMetaData>                     OperationMetaData    { get; set; }
        public Dictionary<RaymarchingOperation, List<int>> OperationDataIndexes { get; set; }
        public Dictionary<RaymarchedObject, int>           ObjectDataIndexes    { get; set; }

        public List<RaymarchingOperationNodeShaderData> OperationNodesShaderData { get; set; }
        public List<RaymarchingOperationShaderData>     OperationsShaderData     { get; set; }
        public List<RaymarchedObjectShaderData>         ObjectsShaderData        { get; set; }
    }
}