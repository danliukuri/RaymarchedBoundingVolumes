using System;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.FourDimensional.Utilities.Wrappers
{
    [Serializable]
    public class ObservableTransform4D : IObservableTransform
    {
        event Action IObservableTransform.Changed
        {
            add
            {
                Position.Changed   += value.CastCached<Vector4>();
                Rotation.Changed   += value.CastCached<Vector3>();
                Rotation4D.Changed += value.CastCached<Vector3>();
                Scale.Changed      += value.CastCached<Vector4>();
            }
            remove
            {
                Position.Changed   -= value.CastCached<Vector4>();
                Rotation.Changed   -= value.CastCached<Vector3>();
                Rotation4D.Changed -= value.CastCached<Vector3>();
                Scale.Changed      -= value.CastCached<Vector4>();
            }
        }

        [field: SerializeField] public ObservableValue<Vector4> Position   { get; set; } = new();
        [field: SerializeField] public ObservableValue<Vector3> Rotation   { get; set; } = new();
        [field: SerializeField] public ObservableValue<Vector3> Rotation4D { get; set; } = new();
        [field: SerializeField] public ObservableValue<Vector4> Scale      { get; set; } = new(Vector4.one);
    }
}