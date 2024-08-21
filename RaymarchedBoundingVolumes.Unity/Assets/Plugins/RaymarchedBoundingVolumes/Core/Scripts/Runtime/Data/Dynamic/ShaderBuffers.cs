using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Dynamic
{
    public class ShaderBuffers
    {
        public ComputeBuffer OperationNodes { get; set; }
        public ComputeBuffer Operations     { get; set; }
        public ComputeBuffer Objects        { get; set; }
    }
}