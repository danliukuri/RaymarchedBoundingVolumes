using System.Runtime.InteropServices;

namespace RaymarchedBoundingVolumes.Data.Dynamic
{
    public struct RaymarchingOperationShaderData
    {
        public int   _operationType;
        public int   _childCount;
        public float _blendStrength;

        public static int Size { get; } = Marshal.SizeOf<RaymarchingOperationShaderData>();
    }
}