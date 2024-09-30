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
                .FindPropertyRelative(nameof(CubeShaderData.Dimensions))
                .vector3Value = CubeShaderData.Default.Dimensions,
            [Sphere]    = () => ResetSphereData(typeDataProperties[Sphere]),
            [Ellipsoid] = () => ResetEllipsoidData(typeDataProperties[Ellipsoid]),
            [Capsule]   = () => ResetCapsuleData(typeDataProperties[Capsule]),
            [EllipticCapsule] = () =>
            {
                typeDataProperties[EllipticCapsule]
                    .FindPropertyRelative(nameof(EllipticCapsuleShaderData.Height))
                    .floatValue = EllipticCapsuleShaderData.Default.Height;

                ResetEllipsoidData(typeDataProperties[EllipticCapsule]
                    .FindPropertyRelative(nameof(EllipticCapsuleShaderData.Ellipsoid)));
            },
            [Cylinder] = () =>
            {
                typeDataProperties[Cylinder]
                    .FindPropertyRelative(nameof(CylinderShaderData.Height))
                    .floatValue = CylinderShaderData.Default.Height;

                ResetSphereData(typeDataProperties[Cylinder]
                    .FindPropertyRelative(nameof(CylinderShaderData.Base)));
            },
            [EllipticCylinder] = () => typeDataProperties[EllipticCylinder]
                .FindPropertyRelative(nameof(EllipticCylinderShaderData.Dimensions))
                .vector3Value = EllipticCylinderShaderData.Default.Dimensions,
            [Plane] = () => typeDataProperties[Plane]
                .FindPropertyRelative(nameof(PlaneShaderData.Dimensions))
                .vector3Value = PlaneShaderData.Default.Dimensions,
            [Cone] = () => ResetCapsuleData(typeDataProperties[Cone]),
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
            [Torus] = () => ResetTorusData(typeDataProperties[Torus]),
            [CappedTorus] = () =>
            {
                typeDataProperties[CappedTorus]
                    .FindPropertyRelative(nameof(CappedTorusShaderData.CapAngle))
                    .floatValue = CappedTorusShaderData.Default.CapAngle;

                ResetTorusData(typeDataProperties[CappedTorus]
                    .FindPropertyRelative(nameof(CappedTorusShaderData.Torus)));
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

        private static void ResetSphereData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(SphereShaderData.Diameter)).floatValue =
                SphereShaderData.Default.Diameter;

        private static void ResetEllipsoidData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(EllipsoidShaderData.Diameters)).vector3Value =
                EllipsoidShaderData.Default.Diameters;

        private static void ResetCapsuleData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(CapsuleShaderData.Height)).floatValue =
                CapsuleShaderData.Default.Height;

            ResetSphereData(property.FindPropertyRelative(nameof(CapsuleShaderData.Base)));
        }

        public static void ResetTorusData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(TorusShaderData.MajorDiameter)).floatValue =
                TorusShaderData.Default.MajorDiameter;

            property.FindPropertyRelative(nameof(TorusShaderData.MinorDiameter)).floatValue =
                TorusShaderData.Default.MinorDiameter;
        }
    }
}