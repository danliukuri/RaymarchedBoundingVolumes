using System;
using RBV.Utilities.Wrappers;

namespace RBV.Data.Dynamic.ShaderData.ObjectType
{
    public interface IObservableObjectTypeShaderData
    {
        const float FullToHalfScaleMultiplier = 0.5f;

        event Action<ChangedValue<IObjectTypeShaderData>> Changed;

        IObjectTypeShaderData GetShaderData(RaymarchedObjectType type);
    }
}