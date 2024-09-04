using System.Runtime.CompilerServices;
using UnityEngine;

namespace RBV.Utilities
{
    public static class VectorExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Unscale(this Vector3 vector, Vector3 scale) =>
            new(vector.x / scale.x, vector.y / scale.y, vector.z / scale.z);
    }
}