using System.Runtime.InteropServices;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Data.Dynamic
{
    public struct RaymarchedObjectShaderData
    {
        public int     _isActive;
        public Vector3 _position;

        public static int Size { get; } = Marshal.SizeOf<RaymarchedObjectShaderData>();
    }
}