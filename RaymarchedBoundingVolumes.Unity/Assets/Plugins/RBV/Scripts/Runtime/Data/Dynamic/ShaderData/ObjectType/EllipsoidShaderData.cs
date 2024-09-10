using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public struct EllipsoidShaderData : IObjectTypeShaderData
    {
        public Vector3 Diameters;

        public static EllipsoidShaderData Default { get; } = new() { Diameters = Vector3.one };
    }
}