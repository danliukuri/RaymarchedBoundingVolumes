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
                HypersphereShaderData.Changed +=
                    value.CastCached<IObjectTypeShaderData, RaymarchedHypersphereShaderData>();
                HypercubeShaderData.Changed +=
                    value.CastCached<IObjectTypeShaderData, RaymarchedHypercubeShaderData>();
            }
            remove
            {
                HypersphereShaderData.Changed -=
                    value.CastCached<IObjectTypeShaderData, RaymarchedHypersphereShaderData>();
                HypercubeShaderData.Changed -=
                    value.CastCached<IObjectTypeShaderData, RaymarchedHypercubeShaderData>();
            }
        }

        [field: SerializeField]
        public ObservableValue<RaymarchedHypersphereShaderData> HypersphereShaderData { get; set; } =
            new(RaymarchedHypersphereShaderData.Default);

        [field: SerializeField]
        public ObservableValue<RaymarchedHypercubeShaderData> HypercubeShaderData { get; set; } =
            new(RaymarchedHypercubeShaderData.Default);

        public IObjectTypeShaderData GetShaderData(RaymarchedObjectType type) =>
            GetShaderData((RaymarchedObject4DType)(int)type);

        public IObjectTypeShaderData GetShaderData(RaymarchedObject4DType type)
        {
            const float fullToHalfScaleMultiplier =
                IObservableObjectTypeShaderData.FullToHalfScaleMultiplier;

            switch (type)
            {
                case RaymarchedObject4DType.Hypersphere:
                    RaymarchedHypersphereShaderData sphereShaderData = HypersphereShaderData.Value;
                    sphereShaderData.Diameter *= fullToHalfScaleMultiplier;
                    return sphereShaderData;
                case RaymarchedObject4DType.Hypercube:
                    RaymarchedHypercubeShaderData cubeShaderData = HypercubeShaderData.Value;
                    cubeShaderData.Dimensions *= fullToHalfScaleMultiplier;
                    return cubeShaderData;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, default);
            }
        }
    }
}