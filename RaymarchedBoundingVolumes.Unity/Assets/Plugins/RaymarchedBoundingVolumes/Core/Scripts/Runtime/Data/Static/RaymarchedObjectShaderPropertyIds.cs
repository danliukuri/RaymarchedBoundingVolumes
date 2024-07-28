using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Static
{
    public class RaymarchedObjectShaderPropertyIds
    {
        private const char Prefix = '_';

        private static int PropertyToID(string name) => Shader.PropertyToID(Prefix + name);

        public static readonly int
            RaymarchingOperationsCount = PropertyToID(nameof(RaymarchingOperationsCount)),
            RaymarchingOperations      = PropertyToID(nameof(RaymarchingOperations     )),
            RaymarchedObjects          = PropertyToID(nameof(RaymarchedObjects         ));
    }
}