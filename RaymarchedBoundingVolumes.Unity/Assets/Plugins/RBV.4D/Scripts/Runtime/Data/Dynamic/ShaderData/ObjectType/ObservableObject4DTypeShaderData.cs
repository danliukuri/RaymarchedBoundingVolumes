using System;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static.Enumerations;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType
{
    [Serializable]
    public class ObservableObject4DTypeShaderData : IObservableObjectTypeShaderData
    {
        public event Action<ChangedValue<IObjectTypeShaderData>> Changed
        {
            add
            {
                HypersphereShaderData.Changed += value.CastCached<IObjectTypeShaderData, HypersphereShaderData>();
                HypercubeShaderData.Changed   += value.CastCached<IObjectTypeShaderData, HypercubeShaderData>();
            }
            remove
            {
                HypersphereShaderData.Changed -= value.CastCached<IObjectTypeShaderData, HypersphereShaderData>();
                HypercubeShaderData.Changed   -= value.CastCached<IObjectTypeShaderData, HypercubeShaderData>();
            }
        }

        [field: SerializeField]
        public ObservableValue<HypersphereShaderData> HypersphereShaderData { get; set; } =
            new(ObjectType.HypersphereShaderData.Default);

        [field: SerializeField]
        public ObservableValue<HypercubeShaderData> HypercubeShaderData { get; set; } =
            new(ObjectType.HypercubeShaderData.Default);

        public IObjectTypeShaderData GetShaderData(RaymarchedObjectType type) =>
            GetShaderData((RaymarchedObject4DType)(int)type);

        public IObjectTypeShaderData GetShaderData(RaymarchedObject4DType type)
        {
            const float fullToHalfScaleMultiplier =
                IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;

            switch (type)
            {
                case RaymarchedObject4DType.Hypersphere:
                    HypersphereShaderData sphereShaderData = HypersphereShaderData.Value;
                    sphereShaderData.Diameter *= fullToHalfScaleMultiplier;
                    return sphereShaderData;
                case RaymarchedObject4DType.Hypercube:
                    HypercubeShaderData cubeShaderData = HypercubeShaderData.Value;
                    cubeShaderData.Dimensions *= fullToHalfScaleMultiplier;
                    return cubeShaderData;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}