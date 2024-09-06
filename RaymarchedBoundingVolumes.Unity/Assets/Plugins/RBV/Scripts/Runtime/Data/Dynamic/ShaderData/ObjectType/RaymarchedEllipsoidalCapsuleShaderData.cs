using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedEllipsoidalCapsuleShaderData : IObjectTypeShaderData
    {
        public float   Height;
        public Vector3 Diameters;

        public static RaymarchedEllipsoidalCapsuleShaderData Default { get; } =
            new() { Height = 1f, Diameters = Vector3.one };
    }
}