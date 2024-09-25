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
            [SmoothUnion] = nameof(ObservableOperationTypeShaderData.SmoothUnion)
        };

        protected override Dictionary<RaymarchingOperationType, Action> InitializeTypeRelatedDataResetters() => new()
        {
            [SmoothUnion] = () => _typeDataProperties[SmoothUnion]
                .FindPropertyRelative(nameof(RadiusDefinedOperationShaderData.Radius))
                .floatValue = RadiusDefinedOperationShaderData.Default.Radius
        };
    }
}