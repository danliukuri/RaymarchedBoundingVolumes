﻿using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Editor.Features;
using RBV.Editor.Utilities.Extensions;
using RBV.Features;
using RBV.Utilities.Wrappers;
using UnityEditor;
using static RBV.Data.Static.Enumerations.RaymarchedObject3DType;

namespace RBV.Editor.Project.Features
{
    [CustomEditor(typeof(RaymarchedObject3D)), CanEditMultipleObjects]
    public class RaymarchedObject3DEditor : UnityEditor.Editor
    {
        private readonly string _observablePropertyValuePath =
            nameof(ObservableValue<RaymarchedObject>.Value).ToLower();

        private SerializedProperty _typeProperty;
        private SerializedProperty _typeValueProperty;
        private SerializedProperty _typeDataProperty;
        private SerializedProperty _transformProperty;

        private Dictionary<RaymarchedObject3DType, SerializedProperty> _typeDataProperties;
        private Dictionary<RaymarchedObject3DType, Action>             _typeDataResetters;

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
            _typeProperty      = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.Type);
            _typeValueProperty = _typeProperty.FindPropertyRelative(_observablePropertyValuePath);
            _typeDataProperty  = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.TypeData);
            _transformProperty = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.Transform);

            InitializeTypeDataProperties();
            InitializeTypeRelatedDataResetters();

            _objectTypeDataDrawer = new ObjectTypeDataDrawer(_typeDataProperty,
                _typeDataProperties.ToDictionary(type => (RaymarchedObjectType)(int)type.Key, type => type.Value),
                _typeDataResetters.ToDictionary(type => (RaymarchedObjectType)(int)type.Key, type => type.Value),
                (RaymarchedObjectType)Enum.GetValues(typeof(RaymarchedObject3DType)).Cast<int>().First());
        }

        private void InitializeTypeDataProperties()
        {
            var typeDataPropertyPath = new Dictionary<RaymarchedObject3DType, string>
            {
                [Cube]                = nameof(ObservableObject3DTypeShaderData.Cube),
                [Sphere]              = nameof(ObservableObject3DTypeShaderData.Sphere),
                [Ellipsoid]           = nameof(ObservableObject3DTypeShaderData.Ellipsoid),
                [Capsule]             = nameof(ObservableObject3DTypeShaderData.Capsule),
                [EllipsoidalCapsule]  = nameof(ObservableObject3DTypeShaderData.EllipsoidalCapsule),
                [Cylinder]            = nameof(ObservableObject3DTypeShaderData.Cylinder),
                [EllipsoidalCylinder] = nameof(ObservableObject3DTypeShaderData.EllipsoidalCylinder),
                [Plane]               = nameof(ObservableObject3DTypeShaderData.Plane),
                [Cone]                = nameof(ObservableObject3DTypeShaderData.Cone),
                [CappedCone]          = nameof(ObservableObject3DTypeShaderData.CappedCone),
                [Torus]               = nameof(ObservableObject3DTypeShaderData.Torus),
                [CappedTorus]         = nameof(ObservableObject3DTypeShaderData.CappedTorus),
                [RegularPrism]        = nameof(ObservableObject3DTypeShaderData.RegularPrism),
                [RegularPolyhedron]   = nameof(ObservableObject3DTypeShaderData.RegularPolyhedron)
            };

            _typeDataProperties = Enum.GetValues(typeof(RaymarchedObject3DType)).Cast<RaymarchedObject3DType>()
                .ToDictionary(type => type, type => _typeDataProperty
                    .FindPropertyRelative(typeDataPropertyPath[type].ToBackingFieldFormat())
                    .FindPropertyRelative(_observablePropertyValuePath));
        }

        private void InitializeTypeRelatedDataResetters() =>
            _typeDataResetters = new Dictionary<RaymarchedObject3DType, Action>
            {
                [Cube] = () => _typeDataProperties[Cube]
                    .FindPropertyRelative(nameof(RaymarchedCubeShaderData.Dimensions))
                    .vector3Value = RaymarchedCubeShaderData.Default.Dimensions,
                [Sphere] = () => _typeDataProperties[Sphere]
                    .FindPropertyRelative(nameof(RaymarchedSphereShaderData.Diameter))
                    .floatValue = RaymarchedSphereShaderData.Default.Diameter,
                [Ellipsoid] = () => _typeDataProperties[Ellipsoid]
                    .FindPropertyRelative(nameof(RaymarchedEllipsoidShaderData.Diameters))
                    .vector3Value = RaymarchedEllipsoidShaderData.Default.Diameters,
                [Capsule] = () =>
                {
                    _typeDataProperties[Capsule]
                        .FindPropertyRelative(nameof(RaymarchedCapsuleShaderData.Diameter))
                        .floatValue = RaymarchedCapsuleShaderData.Default.Diameter;

                    _typeDataProperties[Capsule]
                        .FindPropertyRelative(nameof(RaymarchedCapsuleShaderData.Height))
                        .floatValue = RaymarchedCapsuleShaderData.Default.Height;
                },
                [EllipsoidalCapsule] = () =>
                {
                    _typeDataProperties[EllipsoidalCapsule]
                        .FindPropertyRelative(nameof(RaymarchedEllipsoidalCapsuleShaderData.Diameters))
                        .vector3Value = RaymarchedEllipsoidalCapsuleShaderData.Default.Diameters;

                    _typeDataProperties[EllipsoidalCapsule]
                        .FindPropertyRelative(nameof(RaymarchedEllipsoidalCapsuleShaderData.Height))
                        .floatValue = RaymarchedEllipsoidalCapsuleShaderData.Default.Height;
                },
                [Cylinder] = () =>
                {
                    _typeDataProperties[Cylinder]
                        .FindPropertyRelative(nameof(RaymarchedCylinderShaderData.Diameter))
                        .floatValue = RaymarchedCylinderShaderData.Default.Diameter;

                    _typeDataProperties[Cylinder]
                        .FindPropertyRelative(nameof(RaymarchedCylinderShaderData.Height))
                        .floatValue = RaymarchedCylinderShaderData.Default.Height;
                },
                [EllipsoidalCylinder] = () => _typeDataProperties[EllipsoidalCylinder]
                    .FindPropertyRelative(nameof(RaymarchedEllipsoidalCylinderShaderData.Dimensions))
                    .vector3Value = RaymarchedEllipsoidalCylinderShaderData.Default.Dimensions,
                [Plane] = () => _typeDataProperties[Plane]
                    .FindPropertyRelative(nameof(RaymarchedPlaneShaderData.Dimensions))
                    .vector3Value = RaymarchedPlaneShaderData.Default.Dimensions,
                [Cone] = () =>
                {
                    _typeDataProperties[Cone]
                        .FindPropertyRelative(nameof(RaymarchedConeShaderData.Height))
                        .floatValue = RaymarchedConeShaderData.Default.Height;

                    _typeDataProperties[Cone]
                        .FindPropertyRelative(nameof(RaymarchedConeShaderData.Diameter))
                        .floatValue = RaymarchedConeShaderData.Default.Diameter;
                },
                [CappedCone] = () =>
                {
                    _typeDataProperties[Cone]
                        .FindPropertyRelative(nameof(RaymarchedCappedConeShaderData.Height))
                        .floatValue = RaymarchedCappedConeShaderData.Default.Height;

                    _typeDataProperties[CappedCone]
                        .FindPropertyRelative(nameof(RaymarchedCappedConeShaderData.TopBaseDiameter))
                        .floatValue = RaymarchedCappedConeShaderData.Default.TopBaseDiameter;

                    _typeDataProperties[CappedCone]
                        .FindPropertyRelative(nameof(RaymarchedCappedConeShaderData.BottomBaseDiameter))
                        .floatValue = RaymarchedCappedConeShaderData.Default.BottomBaseDiameter;
                },
                [Torus] = () =>
                {
                    _typeDataProperties[Torus]
                        .FindPropertyRelative(nameof(RaymarchedTorusShaderData.MajorDiameter))
                        .floatValue = RaymarchedTorusShaderData.Default.MajorDiameter;

                    _typeDataProperties[Torus]
                        .FindPropertyRelative(nameof(RaymarchedTorusShaderData.MinorDiameter))
                        .floatValue = RaymarchedTorusShaderData.Default.MinorDiameter;
                },
                [CappedTorus] = () =>
                {
                    _typeDataProperties[CappedTorus]
                        .FindPropertyRelative(nameof(RaymarchedCappedTorusShaderData.CapAngle))
                        .floatValue = RaymarchedCappedTorusShaderData.Default.CapAngle;

                    _typeDataProperties[CappedTorus]
                        .FindPropertyRelative(nameof(RaymarchedCappedTorusShaderData.MajorDiameter))
                        .floatValue = RaymarchedCappedTorusShaderData.Default.MajorDiameter;

                    _typeDataProperties[CappedTorus]
                        .FindPropertyRelative(nameof(RaymarchedCappedTorusShaderData.MinorDiameter))
                        .floatValue = RaymarchedCappedTorusShaderData.Default.MinorDiameter;
                },
                [RegularPrism] = () =>
                {
                    _typeDataProperties[RegularPrism]
                        .FindPropertyRelative(nameof(RaymarchedRegularPrismShaderData.VerticesCount))
                        .intValue = RaymarchedRegularPrismShaderData.Default.VerticesCount;

                    _typeDataProperties[RegularPrism]
                        .FindPropertyRelative(nameof(RaymarchedRegularPrismShaderData.Circumdiameter))
                        .floatValue = RaymarchedRegularPrismShaderData.Default.Circumdiameter;

                    _typeDataProperties[RegularPrism]
                        .FindPropertyRelative(nameof(RaymarchedRegularPrismShaderData.Length))
                        .floatValue = RaymarchedRegularPrismShaderData.Default.Length;
                },
                [RegularPolyhedron] = () =>
                {
                    _typeDataProperties[RegularPolyhedron]
                        .FindPropertyRelative(nameof(RaymarchedRegularPolyhedronShaderData.InscribedDiameter))
                        .floatValue = RaymarchedRegularPolyhedronShaderData.Default.InscribedDiameter;

                    SerializedProperty activeBoundPlaneRange = _typeDataProperties[RegularPolyhedron]
                        .FindPropertyRelative(nameof(RaymarchedRegularPolyhedronShaderData.ActiveBoundPlanesRange));

                    activeBoundPlaneRange.FindPropertyRelative(nameof(Range<int>.Start))
                        .intValue = RaymarchedRegularPolyhedronShaderData.Default.ActiveBoundPlanesRange.Start;

                    activeBoundPlaneRange.FindPropertyRelative(nameof(Range<int>.End))
                        .intValue = RaymarchedRegularPolyhedronShaderData.Default.ActiveBoundPlanesRange.End;
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