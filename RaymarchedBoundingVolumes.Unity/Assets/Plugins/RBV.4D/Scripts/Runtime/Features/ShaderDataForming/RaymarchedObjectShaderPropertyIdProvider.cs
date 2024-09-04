using System;
using System.Collections.Generic;
using RBV.Features.ShaderDataForming;
using RBV.FourDimensional.Data.Static.Enumerations;

namespace RBV.FourDimensional.Features.ShaderDataForming
{
    public class RaymarchedObject4DShaderPropertyIdProvider : RaymarchedObjectShaderPropertyIdProvider
    {
        protected override List<Type> GetObjectTypes()
        {
            List<Type> objectTypes = base.GetObjectTypes();
            objectTypes.Add(typeof(RaymarchedObject4DType));
            return objectTypes;
        }
    }
}