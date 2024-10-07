using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Editor.Features;
using RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static.Enumerations;
using RBV.FourDimensional.Features;
using UnityEditor;
using static RBV.Editor.Project.Data.Dynamic.ShaderData.ObjectType.ObservableObject3DTypeShaderDataDrawer;
using static RBV.FourDimensional.Data.Static.Enumerations.RaymarchedObject4DType;

namespace RBV.FourDimensional.Editor.Project.Data.Dynamic.ShaderData.ObjectType
{
    [CustomPropertyDrawer(typeof(ObservableObject4DTypeShaderData))]
    public class ObservableObject4DTypeShaderDataDrawer : ObservableTypeDataDrawer<RaymarchedObjectType>
    {
        protected override string GetTypePropertyPath() => RaymarchedObject4D.FieldNames.Type;

        protected override RaymarchedObjectType[] GetTypeEnumeration() =>
            Enum.GetValues(typeof(RaymarchedObject4DType)).Cast<int>()
                .Select(type => (RaymarchedObjectType)type)
                .Where(type => _typeDataPropertyPaths.ContainsKey(type)).ToArray();

        protected override RaymarchedObjectType Cast(int enumValueFlag) => (RaymarchedObjectType)enumValueFlag;

        protected override Dictionary<RaymarchedObjectType, string> InitializeTypeDataPropertyPaths() =>
            new Dictionary<RaymarchedObject4DType, string>
            {
                [Hypercube]               = nameof(ObservableObject4DTypeShaderData.Hypercube),
                [Hypersphere]             = nameof(ObservableObject4DTypeShaderData.Hypersphere),
                [Hyperellipsoid]          = nameof(ObservableObject4DTypeShaderData.Hyperellipsoid),
                [Hypercapsule]            = nameof(ObservableObject4DTypeShaderData.Hypercapsule),
                [EllipsoidalHypercapsule] = nameof(ObservableObject4DTypeShaderData.EllipsoidalHypercapsule),
                [CubicalCylinder]         = nameof(ObservableObject4DTypeShaderData.CubicalCylinder),
                [SphericalCylinder]       = nameof(ObservableObject4DTypeShaderData.SphericalCylinder),
                [EllipsoidalCylinder]     = nameof(ObservableObject4DTypeShaderData.EllipsoidalCylinder),
                [ConicalCylinder]         = nameof(ObservableObject4DTypeShaderData.ConicalCylinder),
                [DoubleCylinder]          = nameof(ObservableObject4DTypeShaderData.DoubleCylinder),
                [DoubleEllipticCylinder]  = nameof(ObservableObject4DTypeShaderData.DoubleEllipticCylinder),
                [PrismicCylinder]         = nameof(ObservableObject4DTypeShaderData.PrismicCylinder),
                [SphericalCone]           = nameof(ObservableObject4DTypeShaderData.SphericalCone),
                [CylindricalCone]         = nameof(ObservableObject4DTypeShaderData.CylindricalCone),
                [ToroidalSphere]          = nameof(ObservableObject4DTypeShaderData.ToroidalSphere),
                [SphericalTorus]          = nameof(ObservableObject4DTypeShaderData.SphericalTorus),
                [DoubleTorus]             = nameof(ObservableObject4DTypeShaderData.DoubleTorus),
                [Tiger]                   = nameof(ObservableObject4DTypeShaderData.Tiger),
                [RegularDoublePrism]      = nameof(ObservableObject4DTypeShaderData.RegularDoublePrism)
            }.ToDictionary(pair => (RaymarchedObjectType)(int)pair.Key, pair => pair.Value);

        protected override Dictionary<RaymarchedObjectType, Action> InitializeTypeRelatedDataResetters()
        {
            Dictionary<RaymarchedObject4DType, SerializedProperty> typeDataProperties =
                _typeDataProperties.ToDictionary(pair => (RaymarchedObject4DType)(int)pair.Key, pair => pair.Value);

            return InitializeTypeRelatedDataResetters(typeDataProperties)
                .ToDictionary(pair => (RaymarchedObjectType)(int)pair.Key, pair => pair.Value);
        }

        private static Dictionary<RaymarchedObject4DType, Action> InitializeTypeRelatedDataResetters(
            Dictionary<RaymarchedObject4DType, SerializedProperty> typeDataProperties) => new()
        {
            [Hypercube]      = () => ResetHypercubeData(typeDataProperties[Hypercube]),
            [Hypersphere]    = () => ResetHypersphereData(typeDataProperties[Hypersphere]),
            [Hyperellipsoid] = () => ResetHyperellipsoidData(typeDataProperties[Hyperellipsoid]),
            [Hypercapsule]   = () => ResetHypercapsuleData(typeDataProperties[Hypercapsule]),
            [EllipsoidalHypercapsule] =
                () => ResetEllipsoidalHypercapsuleData(typeDataProperties[EllipsoidalHypercapsule]),
            [CubicalCylinder]     = () => ResetCubicalCylinderData(typeDataProperties[CubicalCylinder]),
            [SphericalCylinder]   = () => ResetSphericalCylinderData(typeDataProperties[SphericalCylinder]),
            [EllipsoidalCylinder] = () => ResetEllipsoidalCylinderData(typeDataProperties[EllipsoidalCylinder]),
            [ConicalCylinder]     = () => ResetCubicalCylinderData(typeDataProperties[ConicalCylinder]),
            [DoubleCylinder]      = () => ResetDoubleCylinderData(typeDataProperties[DoubleCylinder]),
            [DoubleEllipticCylinder] =
                () => ResetDoubleEllipticCylinderData(typeDataProperties[DoubleEllipticCylinder]),
            [PrismicCylinder]    = () => ResetPrismicCylinderData(typeDataProperties[PrismicCylinder]),
            [SphericalCone]      = () => ResetSphericalCylinderData(typeDataProperties[SphericalCone]),
            [CylindricalCone]    = () => ResetSphericalCylinderData(typeDataProperties[CylindricalCone]),
            [ToroidalSphere]     = () => ResetTorusData(typeDataProperties[ToroidalSphere]),
            [SphericalTorus]     = () => ResetTorusData(typeDataProperties[SphericalTorus]),
            [DoubleTorus]        = () => ResetDoubleTorusData(typeDataProperties[DoubleTorus]),
            [Tiger]              = () => ResetTigerData(typeDataProperties[Tiger]),
            [RegularDoublePrism] = () => ResetRegularDoublePrismData(typeDataProperties[RegularDoublePrism])
        };

        private static void ResetHypercubeData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(HypercubeShaderData.Dimensions)).vector4Value =
                HypercubeShaderData.Default.Dimensions;

        private static void ResetHypersphereData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(HypersphereShaderData.Diameter)).floatValue =
                HypersphereShaderData.Default.Diameter;

        private static void ResetHyperellipsoidData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(HyperellipsoidShaderData.Diameters)).vector4Value =
                HyperellipsoidShaderData.Default.Diameters;

        private static void ResetHypercapsuleData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(HypercapsuleShaderData.Height)).floatValue =
                HypercapsuleShaderData.Default.Height;

            ResetHypersphereData(property.FindPropertyRelative(nameof(HypercapsuleShaderData.Base)));
        }

        private static void ResetEllipsoidalHypercapsuleData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(EllipsoidalHypercapsuleShaderData.Height)).floatValue =
                EllipsoidalHypercapsuleShaderData.Default.Height;

            ResetHyperellipsoidData(property.FindPropertyRelative(nameof(EllipsoidalHypercapsuleShaderData.Base)));
        }

        private static void ResetCubicalCylinderData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(CubicalCylinderShaderData.Height)).floatValue =
                CubicalCylinderShaderData.Default.Height;

            ResetSphericalCylinderData(property
                .FindPropertyRelative(nameof(CubicalCylinderShaderData.SphericalCylinder)));
        }

        private static void ResetSphericalCylinderData(SerializedProperty property)
        {
            ResetHypersphereData(property.FindPropertyRelative(nameof(SphericalCylinderShaderData.Base)));

            property.FindPropertyRelative(nameof(SphericalCylinderShaderData.Trength)).floatValue =
                SphericalCylinderShaderData.Default.Trength;
        }

        private static void ResetEllipsoidalCylinderData(SerializedProperty property)
        {
            ResetHyperellipsoidData(property.FindPropertyRelative(nameof(EllipsoidalCylinderShaderData.Base)));

            property.FindPropertyRelative(nameof(EllipsoidalCylinderShaderData.Trength)).floatValue =
                EllipsoidalCylinderShaderData.Default.Trength;
        }

        private static void ResetDoubleCylinderData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(DoubleCylinderShaderData.Diameters)).vector2Value =
                DoubleCylinderShaderData.Default.Diameters;

        private static void ResetDoubleEllipticCylinderData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(DoubleEllipticCylinderShaderData.Diameters)).vector3Value =
                DoubleEllipticCylinderShaderData.Default.Diameters;

        private static void ResetPrismicCylinderData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(PrismicCylinderShaderData.VerticesCount)).intValue =
                PrismicCylinderShaderData.Default.VerticesCount;

            property.FindPropertyRelative(nameof(PrismicCylinderShaderData.Circumdiameter)).floatValue =
                PrismicCylinderShaderData.Default.Circumdiameter;

            property.FindPropertyRelative(nameof(PrismicCylinderShaderData.Length)).floatValue =
                PrismicCylinderShaderData.Default.Length;
        }

        private static void ResetDoubleTorusData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(DoubleTorusShaderData.MajorMajorDiameter)).floatValue =
                DoubleTorusShaderData.Default.MajorMajorDiameter;

            property.FindPropertyRelative(nameof(DoubleTorusShaderData.MajorMinorDiameter)).floatValue =
                DoubleTorusShaderData.Default.MajorMinorDiameter;

            property.FindPropertyRelative(nameof(DoubleTorusShaderData.MinorMinorDiameter)).floatValue =
                DoubleTorusShaderData.Default.MinorMinorDiameter;
        }

        private static void ResetTigerData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(TigerShaderData.MajorDiameters)).vector2Value =
                TigerShaderData.Default.MajorDiameters;

            property.FindPropertyRelative(nameof(TigerShaderData.MinorDiameter)).floatValue =
                TigerShaderData.Default.MinorDiameter;
        }

        private static void ResetRegularDoublePrismData(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(RegularDoublePrismShaderData.VerticesCount)).vector2IntValue =
                RegularDoublePrismShaderData.Default.VerticesCount;

            property.FindPropertyRelative(nameof(RegularDoublePrismShaderData.Circumdiameter)).vector2Value =
                RegularDoublePrismShaderData.Default.Circumdiameter;
        }
    }
}