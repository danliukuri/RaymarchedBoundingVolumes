using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct RaymarchedEllipsoidShaderData : IObjectTypeShaderData
    {
        public Vector3 Diameters;

        public static RaymarchedEllipsoidShaderData Default { get; } = new() { Diameters = Vector3.one };
    }
}