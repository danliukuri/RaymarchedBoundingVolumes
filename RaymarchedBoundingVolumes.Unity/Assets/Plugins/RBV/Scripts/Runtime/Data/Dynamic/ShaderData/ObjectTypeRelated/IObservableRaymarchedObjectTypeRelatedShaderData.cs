using System;
using RBV.Utilities.Wrappers;

namespace RBV.Data.Dynamic.ShaderData.ObjectTypeRelated
{
    public interface IObservableRaymarchedObjectTypeRelatedShaderData
    {
        const float FullToHalfScaleMultiplier = 0.5f;

        event Action<ChangedValue<object>> Changed;

        object GetShaderData(int type);
    }
}