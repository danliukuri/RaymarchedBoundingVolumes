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

        public Dictionary<RaymarchedObjectType, ComputeBuffer> ObjectTypeRelatedData { get; set; }
    }
}