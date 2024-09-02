using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Features;
using RBV.FourDimensional.Data.Dynamic.ShaderData;
using RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static.Enumerations;
using RBV.FourDimensional.Utilities.Wrappers;
using RBV.Utilities.Attributes;
using RBV.Utilities.Wrappers;
using UnityEngine;

namespace RBV.FourDimensional.Features
{
    public class RaymarchedObject4D : RaymarchedObject
    {
        [SerializeField] private ObservableValue<RaymarchedObject4DType> type;
        [SerializeField] private ObservableObject4DTypeShaderData        typeData;

        [SerializeField, ChildTooltip(nameof(ObservableTransform<Vector4>.Scale),
             "Warning: Non-uniform SDF scaling distorts Euclidean spaces!")]
        private new ObservableTransform4D transform = new();

        public override ObservableValue<RaymarchedObjectType> Type          { get; protected set; }
        public override IObservableObjectTypeShaderData       TypeData      => typeData;
        public override IObservableTransform                  Transform     => transform;
        public override TransformType                         TransformType => TransformType.FourDimensional;

        public override ITransformShaderData TransformShaderData => new Transform4DShaderData
        {
            Position   = (Vector4)base.transform.position + transform.Position.Value,
            Rotation   = transform.Rotation.Value   * Mathf.Deg2Rad,
            Rotation4D = transform.Rotation4D.Value * Mathf.Deg2Rad,
            Scale      = transform.Scale.Value
        };

        protected override void Initialize()
        {
            base.Initialize();
            Type = type.Cast(enumType => (RaymarchedObjectType)(int)enumType);
        }

#if UNITY_EDITOR
        public static class FieldNames
        {
            public const string Type      = nameof(type);
            public const string TypeData  = nameof(typeData);
            public const string Transform = nameof(transform);
        }
#endif
    }
}