using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Editor.Features;
using RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static.Enumerations;
using RBV.FourDimensional.Features;
using UnityEditor;
using UnityEngine;
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

        private Dictionary<RaymarchedObject4DType, Action> InitializeTypeRelatedDataResetters(
            Dictionary<RaymarchedObject4DType, SerializedProperty> typeDataProperties) => new()
        {
            [Hypercube] = () => typeDataProperties[Hypercube]
                .FindPropertyRelative(nameof(HypercubeShaderData.Dimensions))
                .vector4Value = HypercubeShaderData.Default.Dimensions,
            [Hypersphere]    = () => ResetHypersphereData(typeDataProperties[Hypersphere]),
            [Hyperellipsoid] = () => ResetHyperellipsoidData(typeDataProperties[Hyperellipsoid]),
            [Hypercapsule] = () =>
            {
                typeDataProperties[Hypercapsule]
                    .FindPropertyRelative(nameof(HypercapsuleShaderData.Height))
                    .floatValue = HypercapsuleShaderData.Default.Height;

                ResetHypersphereData(typeDataProperties[Hypercapsule]
                    .FindPropertyRelative(nameof(HypercapsuleShaderData.Base)));
            },
            [EllipsoidalHypercapsule] = () =>
            {
                typeDataProperties[EllipsoidalHypercapsule]
                    .FindPropertyRelative(nameof(EllipsoidalHypercapsuleShaderData.Height))
                    .floatValue = EllipsoidalHypercapsuleShaderData.Default.Height;

                ResetHyperellipsoidData(typeDataProperties[EllipsoidalHypercapsule]
                    .FindPropertyRelative(nameof(EllipsoidalHypercapsuleShaderData.Base)));
            },
            [CubicalCylinder]   = () => ResetCubicalCylinderData(typeDataProperties[CubicalCylinder]),
            [SphericalCylinder] = () => ResetSphericalCylinderData(typeDataProperties[SphericalCylinder]),
            [EllipsoidalCylinder] = () =>
            {
                ResetHyperellipsoidData(typeDataProperties[EllipsoidalCylinder]
                    .FindPropertyRelative(nameof(EllipsoidalCylinderShaderData.Base)));

                typeDataProperties[EllipsoidalCylinder]
                    .FindPropertyRelative(nameof(EllipsoidalCylinderShaderData.Trength))
                    .floatValue = EllipsoidalCylinderShaderData.Default.Trength;
            },
            [ConicalCylinder] = () => ResetCubicalCylinderData(typeDataProperties[ConicalCylinder]),
            [DoubleCylinder] = () => typeDataProperties[DoubleCylinder]
                .FindPropertyRelative(nameof(DoubleCylinderShaderData.Diameters))
                .vector2Value = DoubleCylinderShaderData.Default.Diameters,
            [DoubleEllipticCylinder] = () => typeDataProperties[DoubleEllipticCylinder]
                .FindPropertyRelative(nameof(DoubleEllipticCylinderShaderData.Diameters))
                .vector3Value = DoubleEllipticCylinderShaderData.Default.Diameters,
            [PrismicCylinder] = () =>
            {
                typeDataProperties[PrismicCylinder]
                    .FindPropertyRelative(nameof(PrismicCylinderShaderData.VerticesCount))
                    .intValue = PrismicCylinderShaderData.Default.VerticesCount;

                typeDataProperties[PrismicCylinder]
                    .FindPropertyRelative(nameof(PrismicCylinderShaderData.Circumdiameter))
                    .floatValue = PrismicCylinderShaderData.Default.Circumdiameter;

                typeDataProperties[PrismicCylinder]
                    .FindPropertyRelative(nameof(PrismicCylinderShaderData.Length))
                    .floatValue = PrismicCylinderShaderData.Default.Length;
            },
            [SphericalCone]   = () => ResetSphericalCylinderData(typeDataProperties[SphericalCone]),
            [CylindricalCone] = () => ResetSphericalCylinderData(typeDataProperties[CylindricalCone]),
            [ToroidalSphere]  = () => ResetTorusData(typeDataProperties[ToroidalSphere]),
            [SphericalTorus]  = () => ResetTorusData(typeDataProperties[SphericalTorus]),
            [DoubleTorus] = () =>
            {
                typeDataProperties[DoubleTorus]
                    .FindPropertyRelative(nameof(DoubleTorusShaderData.MajorMajorDiameter))
                    .floatValue = DoubleTorusShaderData.Default.MajorMajorDiameter;

                typeDataProperties[DoubleTorus]
                    .FindPropertyRelative(nameof(DoubleTorusShaderData.MajorMinorDiameter))
                    .floatValue = DoubleTorusShaderData.Default.MajorMinorDiameter;

                typeDataProperties[DoubleTorus]
                    .FindPropertyRelative(nameof(DoubleTorusShaderData.MinorMinorDiameter))
                    .floatValue = DoubleTorusShaderData.Default.MinorMinorDiameter;
            },
            [Tiger] = () =>
            {
                typeDataProperties[Tiger]
                    .FindPropertyRelative(nameof(TigerShaderData.MajorDiameters))
                    .vector2Value = TigerShaderData.Default.MajorDiameters;

                typeDataProperties[Tiger]
                    .FindPropertyRelative(nameof(TigerShaderData.MinorDiameter))
                    .floatValue = TigerShaderData.Default.MinorDiameter;
            },
            [RegularDoublePrism] = () =>
            {
                typeDataProperties[RegularDoublePrism]
                    .FindPropertyRelative(nameof(RegularDoublePrismShaderData.VerticesCount))
                    .vector2IntValue = RegularDoublePrismShaderData.Default.VerticesCount;

                typeDataProperties[RegularDoublePrism]
                    .FindPropertyRelative(nameof(RegularDoublePrismShaderData.Circumdiameter))
                    .vector2Value = RegularDoublePrismShaderData.Default.Circumdiameter;
            }
        };

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

        private static void ResetHyperellipsoidData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(HyperellipsoidShaderData.Diameters)).vector4Value =
                HyperellipsoidShaderData.Default.Diameters;

        private static void ResetHypersphereData(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(HypersphereShaderData.Diameter)).floatValue =
                HypersphereShaderData.Default.Diameter;
    }
}