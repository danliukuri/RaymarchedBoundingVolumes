using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Data.Dynamic.ShaderData.OperationType;
using RBV.Data.Static.Enumerations;
using RBV.Editor.Features;
using RBV.Editor.Utilities.Extensions;
using RBV.Features;
using UnityEditor;
using static RBV.Data.Static.Enumerations.RaymarchingOperationType;

namespace RBV.Editor.Project.Data.Dynamic.ShaderData.OperationType
{
    [CustomPropertyDrawer(typeof(ObservableOperationTypeShaderData))]
    public class ObservableOperationTypeShaderDataDrawer : ObservableTypeDataDrawer<RaymarchingOperationType>
    {
        protected override bool DrawOnlyChildren() => true;

        protected override string GetTypePropertyPath() => nameof(RaymarchingOperation.Type).ToBackingFieldFormat();

        protected override RaymarchingOperationType[] GetTypeEnumeration() =>
            Enum.GetValues(typeof(RaymarchingOperationType)).Cast<RaymarchingOperationType>()
                .Where(type => _typeDataPropertyPaths.ContainsKey(type)).ToArray();

        protected override RaymarchingOperationType Cast(int enumValueFlag) => (RaymarchingOperationType)enumValueFlag;

        protected override Dictionary<RaymarchingOperationType, string> InitializeTypeDataPropertyPaths() => new()
        {
            [SmoothUnion]      = nameof(ObservableOperationTypeShaderData.SmoothUnion),
            [SmoothSubtract]   = nameof(ObservableOperationTypeShaderData.SmoothSubtract),
            [SmoothIntersect]  = nameof(ObservableOperationTypeShaderData.SmoothIntersect),
            [SmoothXor]        = nameof(ObservableOperationTypeShaderData.SmoothXor),
            [ChamferUnion]     = nameof(ObservableOperationTypeShaderData.ChamferUnion),
            [ChamferSubtract]  = nameof(ObservableOperationTypeShaderData.ChamferSubtract),
            [ChamferIntersect] = nameof(ObservableOperationTypeShaderData.ChamferIntersect),
            [ChamferXor]       = nameof(ObservableOperationTypeShaderData.ChamferXor),
            [StairsUnion]      = nameof(ObservableOperationTypeShaderData.StairsUnion),
            [StairsSubtract]   = nameof(ObservableOperationTypeShaderData.StairsSubtract),
            [StairsIntersect]  = nameof(ObservableOperationTypeShaderData.StairsIntersect),
            [StairsXor]        = nameof(ObservableOperationTypeShaderData.StairsXor),
            [Morph]            = nameof(ObservableOperationTypeShaderData.Morph)
        };

        protected override Dictionary<RaymarchingOperationType, Action> InitializeTypeRelatedDataResetters() => new()
        {
            [SmoothUnion]      = () => ResetRadiusDefinedOperationDataProperty(_typeDataProperties[SmoothUnion]),
            [SmoothSubtract]   = () => ResetRadiusDefinedOperationDataProperty(_typeDataProperties[SmoothSubtract]),
            [SmoothIntersect]  = () => ResetRadiusDefinedOperationDataProperty(_typeDataProperties[SmoothIntersect]),
            [SmoothXor]        = () => ResetRadiusDefinedXorOperationDataProperty(_typeDataProperties[SmoothXor]),
            [ChamferUnion]     = () => ResetRadiusDefinedOperationDataProperty(_typeDataProperties[ChamferUnion]),
            [ChamferSubtract]  = () => ResetRadiusDefinedOperationDataProperty(_typeDataProperties[ChamferSubtract]),
            [ChamferIntersect] = () => ResetRadiusDefinedOperationDataProperty(_typeDataProperties[ChamferIntersect]),
            [ChamferXor]       = () => ResetRadiusDefinedXorOperationDataProperty(_typeDataProperties[ChamferXor]),
            [StairsUnion]      = () => ResetColumnsOperationDataProperty(_typeDataProperties[StairsUnion]),
            [StairsSubtract]   = () => ResetColumnsOperationDataProperty(_typeDataProperties[StairsSubtract]),
            [StairsIntersect]  = () => ResetColumnsOperationDataProperty(_typeDataProperties[StairsIntersect]),
            [StairsXor]        = () => ResetColumnsXorOperationDataProperty(_typeDataProperties[StairsXor]),
            [Morph]            = () => ResetRatioDefinedOperationDataProperty(_typeDataProperties[Morph])
        };

        private void ResetRadiusDefinedOperationDataProperty(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(RadiusDefinedOperationShaderData.Radius)).floatValue =
                RadiusDefinedOperationShaderData.Default.Radius;

        private void ResetRadiusDefinedXorOperationDataProperty(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(RadiusDefinedXorOperationShaderData.OuterRadius)).floatValue =
                RadiusDefinedXorOperationShaderData.Default.OuterRadius;

            property.FindPropertyRelative(nameof(RadiusDefinedXorOperationShaderData.InnerRadius)).floatValue =
                RadiusDefinedXorOperationShaderData.Default.InnerRadius;
        }

        private void ResetColumnsOperationDataProperty(SerializedProperty property)
        {
            property.FindPropertyRelative(nameof(ColumnsOperationShaderData.Radius)).floatValue =
                ColumnsOperationShaderData.Default.Radius;

            property.FindPropertyRelative(nameof(ColumnsOperationShaderData.Count)).intValue =
                ColumnsOperationShaderData.Default.Count;
        }

        private void ResetColumnsXorOperationDataProperty(SerializedProperty property)
        {
            SerializedProperty outerStairsData =
                property.FindPropertyRelative(nameof(ColumnsXorOperationShaderData.Outer));
            SerializedProperty innerStairsData =
                property.FindPropertyRelative(nameof(ColumnsXorOperationShaderData.Inner));

            ResetColumnsOperationDataProperty(outerStairsData);
            ResetColumnsOperationDataProperty(innerStairsData);
        }

        private void ResetRatioDefinedOperationDataProperty(SerializedProperty property) =>
            property.FindPropertyRelative(nameof(RatioDefinedOperationShaderData.Ratio)).floatValue =
                RatioDefinedOperationShaderData.Default.Ratio;
    }
}