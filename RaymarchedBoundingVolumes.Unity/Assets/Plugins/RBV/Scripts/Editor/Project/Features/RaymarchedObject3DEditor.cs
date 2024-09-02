using System;
using System.Collections.Generic;
using RBV.Data.Dynamic.ShaderData.ObjectType;
using RBV.Data.Static.Enumerations;
using RBV.Editor.Utilities.Extensions;
using RBV.Features;
using RBV.Utilities.Wrappers;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Features
{
    [CustomEditor(typeof(RaymarchedObject3D)), CanEditMultipleObjects]
    public class RaymarchedObject3DEditor : UnityEditor.Editor
    {
        private readonly string _observablePropertyValuePath =
            nameof(ObservableValue<RaymarchedObject>.Value).ToLower();

        private SerializedProperty _typeProperty;
        private SerializedProperty _typeValueProperty;
        private SerializedProperty _typeRelatedDataProperty;
        private SerializedProperty _transformProperty;

        private Dictionary<RaymarchedObject3DType, SerializedProperty> _typeRelatedDataProperties;
        private Dictionary<RaymarchedObject3DType, Action>             _typeRelatedDataResetters;
        private Dictionary<RaymarchedObject3DType, SerializedProperty> _typeRelatedDataValueProperties;

        private RaymarchedObject3DType _previousSelectedType;

        private void OnEnable() => Initialize();

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawProperties();
            serializedObject.ApplyModifiedProperties();
        }

        private void Initialize()
        {
            string sphereShaderDataPropertyPath =
                nameof(ObservableObject3DTypeShaderData.SphereShaderData).ToBackingFieldFormat();
            string cubeShaderDataPropertyPath =
                nameof(ObservableObject3DTypeShaderData.CubeShaderData).ToBackingFieldFormat();

            _typeProperty            = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.Type);
            _typeValueProperty       = _typeProperty.FindPropertyRelative(_observablePropertyValuePath);
            _typeRelatedDataProperty = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.TypeRelatedData);
            _transformProperty       = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.Transform);

            _typeRelatedDataProperties = new Dictionary<RaymarchedObject3DType, SerializedProperty>
            {
                {
                    RaymarchedObject3DType.Sphere,
                    _typeRelatedDataProperty.FindPropertyRelative(sphereShaderDataPropertyPath)
                        .FindPropertyRelative(_observablePropertyValuePath)
                },
                {
                    RaymarchedObject3DType.Cube,
                    _typeRelatedDataProperty.FindPropertyRelative(cubeShaderDataPropertyPath)
                        .FindPropertyRelative(_observablePropertyValuePath)
                }
            };

            InitializeTypeRelatedDataResetters();
        }

        private void InitializeTypeRelatedDataResetters()
        {
            _typeRelatedDataResetters = new Dictionary<RaymarchedObject3DType, Action>();
            SerializedProperty sphereDiameterProperty = _typeRelatedDataProperties[RaymarchedObject3DType.Sphere]
                .FindPropertyRelative(nameof(RaymarchedSphereShaderData.Diameter));

            SerializedProperty cubeDimensionsProperty = _typeRelatedDataProperties[RaymarchedObject3DType.Cube]
                .FindPropertyRelative(nameof(RaymarchedCubeShaderData.Dimensions));

            _typeRelatedDataResetters = new Dictionary<RaymarchedObject3DType, Action>
            {
                {
                    RaymarchedObject3DType.Sphere,
                    () => sphereDiameterProperty.floatValue = RaymarchedSphereShaderData.Default.Diameter
                },
                {
                    RaymarchedObject3DType.Cube,
                    () => cubeDimensionsProperty.vector3Value = RaymarchedCubeShaderData.Default.Dimensions
                }
            };
        }

        private void DrawProperties()
        {
            this.DrawScriptField<RaymarchedObject>();
            this.DrawEditorField();

            EditorGUILayout.PropertyField(_typeProperty);

            DrawTypeRelatedDataProperty();

            EditorGUILayout.PropertyField(_transformProperty);
        }

        private void DrawTypeRelatedDataProperty()
        {
            var type = (RaymarchedObject3DType)_typeValueProperty.enumValueIndex;
            if (type != _previousSelectedType)
                _typeRelatedDataResetters[_previousSelectedType].Invoke();
            _previousSelectedType = type;

            _typeRelatedDataProperty.isExpanded = EditorGUILayout.Foldout(_typeRelatedDataProperty.isExpanded,
                new GUIContent(_typeRelatedDataProperty.displayName), true);

            if (_typeRelatedDataProperty.isExpanded)
            {
                SerializedProperty typeRelatedProperty = _typeRelatedDataProperties[type];
                using (new EditorGUI.IndentLevelScope())
                    foreach (SerializedProperty child in typeRelatedProperty.GetDirectChildren())
                        EditorGUILayout.PropertyField(child, true);
            }
        }
    }
}