using System.Collections.Generic;
using RBV.Data.Static.Enumerations;
using UnityEngine;

namespace RBV.Data.Dynamic
{
    public class ShaderBuffers
    {
        public ComputeBuffer OperationNodes { get; set; }
        public ComputeBuffer Operations     { get; set; }
        public ComputeBuffer Objects        { get; set; }

        public Dictionary<RaymarchingOperationType, ComputeBuffer> OperationTypeData { get; set; }

        public Dictionary<TransformType, ComputeBuffer>        ObjectTransformData { get; set; }
        public Dictionary<RaymarchedObjectType, ComputeBuffer> ObjectTypeData      { get; set; }

        public ComputeBuffer ObjectsRenderingSettings { get; set; }
    }
}