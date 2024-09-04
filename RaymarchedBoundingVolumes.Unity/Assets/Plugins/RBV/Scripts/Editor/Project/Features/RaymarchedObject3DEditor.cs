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
                { RaymarchedObject3DType.Sphere, nameof(ObservableObject3DTypeShaderData.SphereShaderData) },
                { RaymarchedObject3DType.Cube, nameof(ObservableObject3DTypeShaderData.CubeShaderData) }
            };

            _typeDataProperties = Enum.GetValues(typeof(RaymarchedObject3DType)).Cast<RaymarchedObject3DType>()
                .ToDictionary(type => type, type => _typeDataProperty
                    .FindPropertyRelative(typeDataPropertyPath[type].ToBackingFieldFormat())
                    .FindPropertyRelative(_observablePropertyValuePath));
        }

        private void InitializeTypeRelatedDataResetters() =>
            _typeDataResetters = new Dictionary<RaymarchedObject3DType, Action>
            {
                {
                    RaymarchedObject3DType.Sphere, () => _typeDataProperties[RaymarchedObject3DType.Sphere]
                        .FindPropertyRelative(nameof(RaymarchedSphereShaderData.Diameter))
                        .floatValue = RaymarchedSphereShaderData.Default.Diameter
                },
                {
                    RaymarchedObject3DType.Cube, () => _typeDataProperties[RaymarchedObject3DType.Cube]
                        .FindPropertyRelative(nameof(RaymarchedCubeShaderData.Dimensions))
                        .vector3Value = RaymarchedCubeShaderData.Default.Dimensions
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