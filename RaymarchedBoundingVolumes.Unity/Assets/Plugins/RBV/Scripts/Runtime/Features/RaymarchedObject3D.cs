using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Static.Enumerations;
using RBV.Utilities.Attributes;
using RBV.Utilities.Wrappers;
using UnityEngine;
using IObservableTypeRelatedShaderData =
    RBV.Data.Dynamic.ShaderData.ObjectTypeRelated.IObservableRaymarchedObjectTypeRelatedShaderData;
using ObservableTypeRelatedShaderData =
    RBV.Data.Dynamic.ShaderData.ObjectTypeRelated.ObservableRaymarchedObjectTypeRelatedShaderData;

namespace RBV.Features
{
    public class RaymarchedObject3D : RaymarchedObject
    {
        [SerializeField] private ObservableValue<RaymarchedObject3DType> type;
        [SerializeField] private ObservableTypeRelatedShaderData         typeRelatedData;

        [SerializeField, ChildTooltip(nameof(ObservableTransform<Vector3>.Scale),
             "Warning: Non-uniform SDF scaling distorts Euclidean spaces!")]
        private new ObservableTransform<Vector3> transform = new()
        {
            Scale = new ObservableValue<Vector3>(Vector3.one)
        };

        public override ObservableValue<RaymarchedObjectType> Type            { get; protected set; }
        public override IObservableTypeRelatedShaderData      TypeRelatedData => typeRelatedData;
        public override IObservableTransform                  Transform       => transform;
        public override TransformType                         TransformType   => TransformType.ThreeDimensional;

        public override ITransformShaderData TransformShaderData => new Transform3DShaderData
        {
            Position = base.transform.position + transform.Position.Value,
            Rotation = transform.Rotation.Value * Mathf.Deg2Rad,
            Scale    = transform.Scale.Value
        };

        protected override void Initialize()
        {
            base.Initialize();
            Type = type.Cast(enumType => (RaymarchedObjectType)(int)enumType);
        }

#if UNITY_EDITOR
        public static class FieldNames
        {
            public const string Type            = nameof(type);
            public const string TypeRelatedData = nameof(typeRelatedData);
            public const string Transform       = nameof(transform);
        }
#endif
    }
}