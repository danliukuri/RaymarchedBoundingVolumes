﻿using System;
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
            [StairsIntersect]  = nameof(ObservableOperationTypeShaderData.StairsIntersect)
        };

        protected override Dictionary<RaymarchingOperationType, Action> InitializeTypeRelatedDataResetters() => new()
        {
            [SmoothUnion] = () => _typeDataProperties[SmoothUnion]
                .FindPropertyRelative(nameof(RadiusDefinedOperationShaderData.Radius))
                .floatValue = RadiusDefinedOperationShaderData.Default.Radius,
            [SmoothSubtract] = () => _typeDataProperties[SmoothSubtract]
                .FindPropertyRelative(nameof(RadiusDefinedOperationShaderData.Radius))
                .floatValue = RadiusDefinedOperationShaderData.Default.Radius,
            [SmoothIntersect] = () => _typeDataProperties[SmoothIntersect]
                .FindPropertyRelative(nameof(RadiusDefinedOperationShaderData.Radius))
                .floatValue = RadiusDefinedOperationShaderData.Default.Radius,
            [SmoothXor] = () =>
            {
                _typeDataProperties[SmoothXor]
                    .FindPropertyRelative(nameof(RadiusDefinedXorOperationShaderData.OuterRadius))
                    .floatValue = RadiusDefinedXorOperationShaderData.Default.OuterRadius;

                _typeDataProperties[SmoothXor]
                    .FindPropertyRelative(nameof(RadiusDefinedXorOperationShaderData.InnerRadius))
                    .floatValue = RadiusDefinedXorOperationShaderData.Default.InnerRadius;
            },
            [ChamferUnion] = () => _typeDataProperties[ChamferUnion]
                .FindPropertyRelative(nameof(RadiusDefinedOperationShaderData.Radius))
                .floatValue = RadiusDefinedOperationShaderData.Default.Radius,
            [ChamferSubtract] = () => _typeDataProperties[ChamferSubtract]
                .FindPropertyRelative(nameof(RadiusDefinedOperationShaderData.Radius))
                .floatValue = RadiusDefinedOperationShaderData.Default.Radius,
            [ChamferIntersect] = () => _typeDataProperties[ChamferIntersect]
                .FindPropertyRelative(nameof(RadiusDefinedOperationShaderData.Radius))
                .floatValue = RadiusDefinedOperationShaderData.Default.Radius,
            [ChamferXor] = () =>
            {
                _typeDataProperties[ChamferXor]
                    .FindPropertyRelative(nameof(RadiusDefinedXorOperationShaderData.OuterRadius))
                    .floatValue = RadiusDefinedXorOperationShaderData.Default.OuterRadius;

                _typeDataProperties[ChamferXor]
                    .FindPropertyRelative(nameof(RadiusDefinedXorOperationShaderData.InnerRadius))
                    .floatValue = RadiusDefinedXorOperationShaderData.Default.InnerRadius;
            },
            [StairsUnion] = () =>
            {
                _typeDataProperties[StairsUnion]
                    .FindPropertyRelative(nameof(ColumnsOperationShaderData.Radius))
                    .floatValue = ColumnsOperationShaderData.Default.Radius;

                _typeDataProperties[StairsUnion]
                    .FindPropertyRelative(nameof(ColumnsOperationShaderData.Count))
                    .intValue = ColumnsOperationShaderData.Default.Count;
            },
            [StairsSubtract] = () =>
            {
                _typeDataProperties[StairsSubtract]
                    .FindPropertyRelative(nameof(ColumnsOperationShaderData.Radius))
                    .floatValue = ColumnsOperationShaderData.Default.Radius;

                _typeDataProperties[StairsSubtract]
                    .FindPropertyRelative(nameof(ColumnsOperationShaderData.Count))
                    .intValue = ColumnsOperationShaderData.Default.Count;
            },
            [StairsIntersect] = () =>
            {
                _typeDataProperties[StairsIntersect]
                    .FindPropertyRelative(nameof(ColumnsOperationShaderData.Radius))
                    .floatValue = ColumnsOperationShaderData.Default.Radius;

                _typeDataProperties[StairsIntersect]
                    .FindPropertyRelative(nameof(ColumnsOperationShaderData.Count))
                    .intValue = ColumnsOperationShaderData.Default.Count;
            }
        };
    }
}