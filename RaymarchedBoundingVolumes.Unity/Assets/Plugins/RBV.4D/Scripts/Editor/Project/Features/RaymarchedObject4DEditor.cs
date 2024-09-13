using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Editor.Features;
using RBV.Editor.Utilities.Extensions;
using RBV.Features;
using RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static.Enumerations;
using RBV.FourDimensional.Features;
using RBV.Utilities.Wrappers;
using UnityEditor;
using static RBV.FourDimensional.Data.Static.Enumerations.RaymarchedObject4DType;

namespace RBV.FourDimensional.Editor.Project.Features
{
    [CustomEditor(typeof(RaymarchedObject4D)), CanEditMultipleObjects]
    public class RaymarchedObject4DEditor : UnityEditor.Editor
    {
        private readonly string _observablePropertyValuePath =
            nameof(ObservableValue<RaymarchedObject>.Value).ToLower();

        private SerializedProperty _typeProperty;
        private SerializedProperty _typeValueProperty;
        private SerializedProperty _typeDataProperty;
        private SerializedProperty _transformProperty;

        private Dictionary<RaymarchedObject4DType, SerializedProperty> _typeDataProperties;
        private Dictionary<RaymarchedObject4DType, Action>             _typeDataResetters;

        private ObjectTypeDataDrawer _objectTypeDataDrawer;

        private void OnEnable() => Initialize();

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }

        private void Initialize()
        {
            _typeProperty      = serializedObject.FindProperty(RaymarchedObject4D.FieldNames.Type);
            _typeValueProperty = _typeProperty.FindPropertyRelative(_observablePropertyValuePath);
            _typeDataProperty  = serializedObject.FindProperty(RaymarchedObject4D.FieldNames.TypeData);
            _transformProperty = serializedObject.FindProperty(RaymarchedObject4D.FieldNames.Transform);

            InitializeTypeDataProperties();
            InitializeTypeRelatedDataResetters();

            _objectTypeDataDrawer = new ObjectTypeDataDrawer(_typeDataProperty,
                _typeDataProperties.ToDictionary(type => (RaymarchedObjectType)(int)type.Key, type => type.Value),
                _typeDataResetters.ToDictionary(type => (RaymarchedObjectType)(int)type.Key, type => type.Value),
                (RaymarchedObjectType)Enum.GetValues(typeof(RaymarchedObject4DType)).Cast<int>().First());
        }

        private void InitializeTypeDataProperties()
        {
            var typeDataPropertyPath = new Dictionary<RaymarchedObject4DType, string>
            {
                [Hypercube]               = nameof(ObservableObject4DTypeShaderData.Hypercube),
                [Hypersphere]             = nameof(ObservableObject4DTypeShaderData.Hypersphere),
                [Hyperellipsoid]          = nameof(ObservableObject4DTypeShaderData.Hyperellipsoid),
                [Hypercapsule]            = nameof(ObservableObject4DTypeShaderData.Hypercapsule),
                [EllipsoidalHypercapsule] = nameof(ObservableObject4DTypeShaderData.EllipsoidalHypercapsule),
                [CubicalCylinder]         = nameof(ObservableObject4DTypeShaderData.CubicalCylinder),
                [SphericalCylinder]       = nameof(ObservableObject4DTypeShaderData.SphericalCylinder),
                [EllipsoidalCylinder]     = nameof(ObservableObject4DTypeShaderData.EllipsoidalCylinder),
                [ConicalCylinder]         = nameof(ObservableObject4DTypeShaderData.ConicalCylinder)
            };

            _typeDataProperties = Enum.GetValues(typeof(RaymarchedObject4DType)).Cast<RaymarchedObject4DType>()
                .ToDictionary(type => type, type => _typeDataProperty
                    .FindPropertyRelative(typeDataPropertyPath[type].ToBackingFieldFormat())
                    .FindPropertyRelative(_observablePropertyValuePath));
        }

        private void InitializeTypeRelatedDataResetters() =>
            _typeDataResetters = new Dictionary<RaymarchedObject4DType, Action>
            {
                [Hypercube] = () => _typeDataProperties[Hypercube]
                    .FindPropertyRelative(nameof(HypercubeShaderData.Dimensions))
                    .vector4Value = HypercubeShaderData.Default.Dimensions,
                [Hypersphere] = () => _typeDataProperties[Hypersphere]
                    .FindPropertyRelative(nameof(HypersphereShaderData.Diameter))
                    .floatValue = HypersphereShaderData.Default.Diameter,
                [Hyperellipsoid] = () => _typeDataProperties[Hyperellipsoid]
                    .FindPropertyRelative(nameof(HyperellipsoidShaderData.Diameters))
                    .vector4Value = HyperellipsoidShaderData.Default.Diameters,
                [Hypercapsule] = () =>
                {
                    _typeDataProperties[Hypercapsule]
                        .FindPropertyRelative(nameof(HypercapsuleShaderData.Height))
                        .floatValue = HypercapsuleShaderData.Default.Height;

                    _typeDataProperties[Hypercapsule]
                        .FindPropertyRelative(nameof(HypercapsuleShaderData.Diameter))
                        .floatValue = HypercapsuleShaderData.Default.Diameter;
                },
                [EllipsoidalHypercapsule] = () =>
                {
                    _typeDataProperties[EllipsoidalHypercapsule]
                        .FindPropertyRelative(nameof(EllipsoidalHypercapsuleShaderData.Height))
                        .floatValue = EllipsoidalHypercapsuleShaderData.Default.Height;

                    _typeDataProperties[EllipsoidalHypercapsule]
                        .FindPropertyRelative(nameof(EllipsoidalHypercapsuleShaderData.Diameters))
                        .vector4Value = EllipsoidalHypercapsuleShaderData.Default.Diameters;
                },
                [CubicalCylinder] = () =>
                {
                    _typeDataProperties[CubicalCylinder]
                        .FindPropertyRelative(nameof(CubicalCylinderShaderData.Diameter))
                        .floatValue = CubicalCylinderShaderData.Default.Diameter;

                    _typeDataProperties[CubicalCylinder]
                        .FindPropertyRelative(nameof(CubicalCylinderShaderData.Height))
                        .floatValue = CubicalCylinderShaderData.Default.Height;

                    _typeDataProperties[CubicalCylinder]
                        .FindPropertyRelative(nameof(CubicalCylinderShaderData.Trength))
                        .floatValue = CubicalCylinderShaderData.Default.Trength;
                },
                [SphericalCylinder] = () =>
                {
                    _typeDataProperties[SphericalCylinder]
                        .FindPropertyRelative(nameof(SphericalCylinderShaderData.Diameter))
                        .floatValue = SphericalCylinderShaderData.Default.Diameter;

                    _typeDataProperties[SphericalCylinder]
                        .FindPropertyRelative(nameof(SphericalCylinderShaderData.Trength))
                        .floatValue = SphericalCylinderShaderData.Default.Trength;
                },
                [EllipsoidalCylinder] = () =>
                {
                    _typeDataProperties[EllipsoidalCylinder]
                        .FindPropertyRelative(nameof(EllipsoidalCylinderShaderData.Diameters))
                        .vector4Value = EllipsoidalCylinderShaderData.Default.Diameters;

                    _typeDataProperties[EllipsoidalCylinder]
                        .FindPropertyRelative(nameof(EllipsoidalCylinderShaderData.Trength))
                        .floatValue = EllipsoidalCylinderShaderData.Default.Trength;
                },
                [ConicalCylinder] = () =>
                {
                    _typeDataProperties[ConicalCylinder]
                        .FindPropertyRelative(nameof(ConicalCylinderShaderData.Diameter))
                        .floatValue = ConicalCylinderShaderData.Default.Diameter;

                    _typeDataProperties[ConicalCylinder]
                        .FindPropertyRelative(nameof(ConicalCylinderShaderData.Height))
                        .floatValue = ConicalCylinderShaderData.Default.Height;

                    _typeDataProperties[ConicalCylinder]
                        .FindPropertyRelative(nameof(ConicalCylinderShaderData.Trength))
                        .floatValue = ConicalCylinderShaderData.Default.Trength;
                }
            };

        private void DrawProperties()
        {
            this.DrawScriptField<RaymarchedObject>();
            this.DrawEditorField();

            EditorGUILayout.PropertyField(_typeProperty);

            if (!_typeProperty.hasMultipleDifferentValues)
                _objectTypeDataDrawer.DrawTypeDataProperty((RaymarchedObjectType)_typeValueProperty.enumValueFlag);

            EditorGUILayout.PropertyField(_transformProperty);
        }
    }
}