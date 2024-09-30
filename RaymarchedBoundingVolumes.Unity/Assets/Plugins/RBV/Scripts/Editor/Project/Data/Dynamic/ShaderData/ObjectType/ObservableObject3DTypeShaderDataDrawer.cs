using System;
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

namespace RBV.Editor.Project.Data.Dynamic.ShaderData.ObjectType
{
    [CustomPropertyDrawer(typeof(ObservableObject3DTypeShaderData))]
    public class ObservableObject3DTypeShaderDataDrawer : ObservableTypeDataDrawer<RaymarchedObjectType>
    {
        protected override string GetTypePropertyPath() => RaymarchedObject3D.FieldNames.Type;

        protected override RaymarchedObjectType[] GetTypeEnumeration() =>
            Enum.GetValues(typeof(RaymarchedObject3DType)).Cast<int>()
                .Select(type => (RaymarchedObjectType)type)
                .Where(type => _typeDataPropertyPaths.ContainsKey(type)).ToArray();

        protected override RaymarchedObjectType Cast(int enumValueFlag) => (RaymarchedObjectType)enumValueFlag;

        protected override Dictionary<RaymarchedObjectType, string> InitializeTypeDataPropertyPaths() =>
            new Dictionary<RaymarchedObject3DType, string>
            {
                [Cube]              = nameof(ObservableObject3DTypeShaderData.Cube),
                [Sphere]            = nameof(ObservableObject3DTypeShaderData.Sphere),
                [Ellipsoid]         = nameof(ObservableObject3DTypeShaderData.Ellipsoid),
                [Capsule]           = nameof(ObservableObject3DTypeShaderData.Capsule),
                [EllipticCapsule]   = nameof(ObservableObject3DTypeShaderData.EllipticCapsule),
                [Cylinder]          = nameof(ObservableObject3DTypeShaderData.Cylinder),
                [EllipticCylinder]  = nameof(ObservableObject3DTypeShaderData.EllipticCylinder),
                [Plane]             = nameof(ObservableObject3DTypeShaderData.Plane),
                [Cone]              = nameof(ObservableObject3DTypeShaderData.Cone),
                [CappedCone]        = nameof(ObservableObject3DTypeShaderData.CappedCone),
                [Torus]             = nameof(ObservableObject3DTypeShaderData.Torus),
                [CappedTorus]       = nameof(ObservableObject3DTypeShaderData.CappedTorus),
                [RegularPrism]      = nameof(ObservableObject3DTypeShaderData.RegularPrism),
                [RegularPolyhedron] = nameof(ObservableObject3DTypeShaderData.RegularPolyhedron)
            }.ToDictionary(pair => (RaymarchedObjectType)(int)pair.Key, pair => pair.Value);

        protected override Dictionary<RaymarchedObjectType, Action> InitializeTypeRelatedDataResetters()
        {
            Dictionary<RaymarchedObject3DType, SerializedProperty> typeDataProperties =
                _typeDataProperties.ToDictionary(pair => (RaymarchedObject3DType)(int)pair.Key, pair => pair.Value);

            return InitializeTypeRelatedDataResetters(typeDataProperties)
                .ToDictionary(pair => (RaymarchedObjectType)(int)pair.Key, pair => pair.Value);
        }

        private Dictionary<RaymarchedObject3DType, Action> InitializeTypeRelatedDataResetters(
            Dictionary<RaymarchedObject3DType, SerializedProperty> typeDataProperties) => new()
        {
            [Cube] = () => typeDataProperties[Cube]
                .FindPropertyRelative(nameof(RaymarchedCubeShaderData.Dimensions))
                .vector3Value = RaymarchedCubeShaderData.Default.Dimensions,
            [Sphere] = () => typeDataProperties[Sphere]
                .FindPropertyRelative(nameof(SphereShaderData.Diameter))
                .floatValue = SphereShaderData.Default.Diameter,
            [Ellipsoid] = () => typeDataProperties[Ellipsoid]
                .FindPropertyRelative(nameof(EllipsoidShaderData.Diameters))
                .vector3Value = EllipsoidShaderData.Default.Diameters,
            [Capsule] = () =>
            {
                typeDataProperties[Capsule]
                    .FindPropertyRelative(nameof(RaymarchedCapsuleShaderData.Diameter))
                    .floatValue = RaymarchedCapsuleShaderData.Default.Diameter;

                typeDataProperties[Capsule]
                    .FindPropertyRelative(nameof(RaymarchedCapsuleShaderData.Height))
                    .floatValue = RaymarchedCapsuleShaderData.Default.Height;
            },
            [EllipticCapsule] = () =>
            {
                typeDataProperties[EllipticCapsule]
                    .FindPropertyRelative(nameof(EllipticCapsuleShaderData.Diameters))
                    .vector3Value = EllipticCapsuleShaderData.Default.Diameters;

                typeDataProperties[EllipticCapsule]
                    .FindPropertyRelative(nameof(EllipticCapsuleShaderData.Height))
                    .floatValue = EllipticCapsuleShaderData.Default.Height;
            },
            [Cylinder] = () =>
            {
                typeDataProperties[Cylinder]
                    .FindPropertyRelative(nameof(RaymarchedCylinderShaderData.Diameter))
                    .floatValue = RaymarchedCylinderShaderData.Default.Diameter;

                typeDataProperties[Cylinder]
                    .FindPropertyRelative(nameof(RaymarchedCylinderShaderData.Height))
                    .floatValue = RaymarchedCylinderShaderData.Default.Height;
            },
            [EllipticCylinder] = () => typeDataProperties[EllipticCylinder]
                .FindPropertyRelative(nameof(EllipticCylinderShaderData.Dimensions))
                .vector3Value = EllipticCylinderShaderData.Default.Dimensions,
            [Plane] = () => typeDataProperties[Plane]
                .FindPropertyRelative(nameof(PlaneShaderData.Dimensions))
                .vector3Value = PlaneShaderData.Default.Dimensions,
            [Cone] = () =>
            {
                typeDataProperties[Cone]
                    .FindPropertyRelative(nameof(RaymarchedConeShaderData.Height))
                    .floatValue = RaymarchedConeShaderData.Default.Height;

                typeDataProperties[Cone]
                    .FindPropertyRelative(nameof(RaymarchedConeShaderData.Diameter))
                    .floatValue = RaymarchedConeShaderData.Default.Diameter;
            },
            [CappedCone] = () =>
            {
                typeDataProperties[Cone]
                    .FindPropertyRelative(nameof(CappedConeShaderData.Height))
                    .floatValue = CappedConeShaderData.Default.Height;

                typeDataProperties[CappedCone]
                    .FindPropertyRelative(nameof(CappedConeShaderData.TopBaseDiameter))
                    .floatValue = CappedConeShaderData.Default.TopBaseDiameter;

                typeDataProperties[CappedCone]
                    .FindPropertyRelative(nameof(CappedConeShaderData.BottomBaseDiameter))
                    .floatValue = CappedConeShaderData.Default.BottomBaseDiameter;
            },
            [Torus] = () =>
            {
                typeDataProperties[Torus]
                    .FindPropertyRelative(nameof(TorusShaderData.MajorDiameter))
                    .floatValue = TorusShaderData.Default.MajorDiameter;

                typeDataProperties[Torus]
                    .FindPropertyRelative(nameof(TorusShaderData.MinorDiameter))
                    .floatValue = TorusShaderData.Default.MinorDiameter;
            },
            [CappedTorus] = () =>
            {
                typeDataProperties[CappedTorus]
                    .FindPropertyRelative(nameof(CappedTorusShaderData.CapAngle))
                    .floatValue = CappedTorusShaderData.Default.CapAngle;

                typeDataProperties[CappedTorus]
                    .FindPropertyRelative(nameof(CappedTorusShaderData.MajorDiameter))
                    .floatValue = CappedTorusShaderData.Default.MajorDiameter;

                typeDataProperties[CappedTorus]
                    .FindPropertyRelative(nameof(CappedTorusShaderData.MinorDiameter))
                    .floatValue = CappedTorusShaderData.Default.MinorDiameter;
            },
            [RegularPrism] = () =>
            {
                typeDataProperties[RegularPrism]
                    .FindPropertyRelative(nameof(RegularPrismShaderData.VerticesCount))
                    .intValue = RegularPrismShaderData.Default.VerticesCount;

                typeDataProperties[RegularPrism]
                    .FindPropertyRelative(nameof(RegularPrismShaderData.Circumdiameter))
                    .floatValue = RegularPrismShaderData.Default.Circumdiameter;

                typeDataProperties[RegularPrism]
                    .FindPropertyRelative(nameof(RegularPrismShaderData.Length))
                    .floatValue = RegularPrismShaderData.Default.Length;
            },
            [RegularPolyhedron] = () =>
            {
                typeDataProperties[RegularPolyhedron]
                    .FindPropertyRelative(nameof(RegularPolyhedronShaderData.InscribedDiameter))
                    .floatValue = RegularPolyhedronShaderData.Default.InscribedDiameter;

                SerializedProperty activeBoundPlaneRange = typeDataProperties[RegularPolyhedron]
                    .FindPropertyRelative(nameof(RegularPolyhedronShaderData.ActiveBoundPlanesRange));

                activeBoundPlaneRange.FindPropertyRelative(nameof(Range<int>.Start).ToBackingFieldFormat())
                    .intValue = RegularPolyhedronShaderData.Default.ActiveBoundPlanesRange.Start;

                activeBoundPlaneRange.FindPropertyRelative(nameof(Range<int>.End).ToBackingFieldFormat())
                    .intValue = RegularPolyhedronShaderData.Default.ActiveBoundPlanesRange.End;
            }
        };
    }
}