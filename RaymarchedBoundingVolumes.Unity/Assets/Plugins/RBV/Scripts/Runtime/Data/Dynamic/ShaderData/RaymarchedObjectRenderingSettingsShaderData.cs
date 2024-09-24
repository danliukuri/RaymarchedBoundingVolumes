using System;
using UnityEngine;

namespace RBV.Data.Dynamic.ShaderData
{
    [Serializable]
    public struct RaymarchedObjectRenderingSettingsShaderData : IEquatable<RaymarchedObjectRenderingSettingsShaderData>
    {
        public Color Color;

        public bool Equals(RaymarchedObjectRenderingSettingsShaderData other) => Color.Equals(other.Color);

        public override bool Equals(object obj) =>
            obj is RaymarchedObjectRenderingSettingsShaderData other && Equals(other);

        public override int GetHashCode() => Color.GetHashCode();

        public static bool operator ==(RaymarchedObjectRenderingSettingsShaderData left,
                                       RaymarchedObjectRenderingSettingsShaderData right) => left.Equals(right);

        public static bool operator !=(RaymarchedObjectRenderingSettingsShaderData left,
                                       RaymarchedObjectRenderingSettingsShaderData right) => !left.Equals(right);
    }
}