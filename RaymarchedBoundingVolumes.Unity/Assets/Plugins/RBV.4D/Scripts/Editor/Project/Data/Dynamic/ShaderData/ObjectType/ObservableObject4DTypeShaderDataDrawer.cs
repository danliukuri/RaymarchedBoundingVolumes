using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Editor.Features;
using RBV.FourDimensional.Data.Dynamic.ShaderData.ObjectType;
using RBV.FourDimensional.Data.Static.Enumerations;
using RBV.FourDimensional.Features;
using UnityEditor;
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
            [Hypersphere] = () => typeDataProperties[Hypersphere]
                .FindPropertyRelative(nameof(HypersphereShaderData.Diameter))
                .floatValue = HypersphereShaderData.Default.Diameter,
            [Hyperellipsoid] = () => typeDataProperties[Hyperellipsoid]
                .FindPropertyRelative(nameof(HyperellipsoidShaderData.Diameters))
                .vector4Value = HyperellipsoidShaderData.Default.Diameters,
            [Hypercapsule] = () =>
            {
                typeDataProperties[Hypercapsule]
                    .FindPropertyRelative(nameof(HypercapsuleShaderData.Height))
                    .floatValue = HypercapsuleShaderData.Default.Height;

                typeDataProperties[Hypercapsule]
                    .FindPropertyRelative(nameof(HypercapsuleShaderData.Diameter))
                    .floatValue = HypercapsuleShaderData.Default.Diameter;
            },
            [EllipsoidalHypercapsule] = () =>
            {
                typeDataProperties[EllipsoidalHypercapsule]
                    .FindPropertyRelative(nameof(EllipsoidalHypercapsuleShaderData.Height))
                    .floatValue = EllipsoidalHypercapsuleShaderData.Default.Height;

                typeDataProperties[EllipsoidalHypercapsule]
                    .FindPropertyRelative(nameof(EllipsoidalHypercapsuleShaderData.Diameters))
                    .vector4Value = EllipsoidalHypercapsuleShaderData.Default.Diameters;
            },
            [CubicalCylinder] = () =>
            {
                typeDataProperties[CubicalCylinder]
                    .FindPropertyRelative(nameof(CubicalCylinderShaderData.Diameter))
                    .floatValue = CubicalCylinderShaderData.Default.Diameter;

                typeDataProperties[CubicalCylinder]
                    .FindPropertyRelative(nameof(CubicalCylinderShaderData.Height))
                    .floatValue = CubicalCylinderShaderData.Default.Height;

                typeDataProperties[CubicalCylinder]
                    .FindPropertyRelative(nameof(CubicalCylinderShaderData.Trength))
                    .floatValue = CubicalCylinderShaderData.Default.Trength;
            },
            [SphericalCylinder] = () =>
            {
                typeDataProperties[SphericalCylinder]
                    .FindPropertyRelative(nameof(SphericalCylinderShaderData.Diameter))
                    .floatValue = SphericalCylinderShaderData.Default.Diameter;

                typeDataProperties[SphericalCylinder]
                    .FindPropertyRelative(nameof(SphericalCylinderShaderData.Trength))
                    .floatValue = SphericalCylinderShaderData.Default.Trength;
            },
            [EllipsoidalCylinder] = () =>
            {
                typeDataProperties[EllipsoidalCylinder]
                    .FindPropertyRelative(nameof(EllipsoidalCylinderShaderData.Diameters))
                    .vector4Value = EllipsoidalCylinderShaderData.Default.Diameters;

                typeDataProperties[EllipsoidalCylinder]
                    .FindPropertyRelative(nameof(EllipsoidalCylinderShaderData.Trength))
                    .floatValue = EllipsoidalCylinderShaderData.Default.Trength;
            },
            [ConicalCylinder] = () =>
            {
                typeDataProperties[ConicalCylinder]
                    .FindPropertyRelative(nameof(ConicalCylinderShaderData.Diameter))
                    .floatValue = ConicalCylinderShaderData.Default.Diameter;

                typeDataProperties[ConicalCylinder]
                    .FindPropertyRelative(nameof(ConicalCylinderShaderData.Height))
                    .floatValue = ConicalCylinderShaderData.Default.Height;

                typeDataProperties[ConicalCylinder]
                    .FindPropertyRelative(nameof(ConicalCylinderShaderData.Trength))
                    .floatValue = ConicalCylinderShaderData.Default.Trength;
            },
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
            [SphericalCone] = () =>
            {
                typeDataProperties[SphericalCone]
                    .FindPropertyRelative(nameof(SphericalConeShaderData.Diameter))
                    .floatValue = SphericalConeShaderData.Default.Diameter;

                typeDataProperties[SphericalCone]
                    .FindPropertyRelative(nameof(SphericalConeShaderData.Trength))
                    .floatValue = SphericalConeShaderData.Default.Trength;
            },
            [CylindricalCone] = () =>
            {
                typeDataProperties[CylindricalCone]
                    .FindPropertyRelative(nameof(CylindricalConeShaderData.Diameter))
                    .floatValue = CylindricalConeShaderData.Default.Diameter;

                typeDataProperties[CylindricalCone]
                    .FindPropertyRelative(nameof(CylindricalConeShaderData.Trength))
                    .floatValue = CylindricalConeShaderData.Default.Trength;
            },
            [ToroidalSphere] = () =>
            {
                typeDataProperties[ToroidalSphere]
                    .FindPropertyRelative(nameof(TorusShaderData.MajorDiameter))
                    .floatValue = TorusShaderData.Default.MajorDiameter;

                typeDataProperties[ToroidalSphere]
                    .FindPropertyRelative(nameof(TorusShaderData.MinorDiameter))
                    .floatValue = TorusShaderData.Default.MinorDiameter;
            },
            [SphericalTorus] = () =>
            {
                typeDataProperties[SphericalTorus]
                    .FindPropertyRelative(nameof(TorusShaderData.MajorDiameter))
                    .floatValue = TorusShaderData.Default.MajorDiameter;

                typeDataProperties[SphericalTorus]
                    .FindPropertyRelative(nameof(TorusShaderData.MinorDiameter))
                    .floatValue = TorusShaderData.Default.MinorDiameter;
            },
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
    }
}