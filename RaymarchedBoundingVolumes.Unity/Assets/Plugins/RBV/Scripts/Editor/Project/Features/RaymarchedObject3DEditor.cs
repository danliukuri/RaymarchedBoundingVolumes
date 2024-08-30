using System;
using System.Collections.Generic;
using RBV.Data.Dynamic.ShaderData.ObjectTypeRelated;
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

        private Dictionary<RaymarchedObjectType, SerializedProperty> _typeRelatedDataProperties;
        private Dictionary<RaymarchedObjectType, Action>             _typeRelatedDataResetters;
        private Dictionary<RaymarchedObjectType, SerializedProperty> _typeRelatedDataValueProperties;

        private RaymarchedObjectType _previousSelectedType;

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
                nameof(ObservableRaymarchedObjectTypeRelatedShaderData.SphereShaderData).ToBackingFieldFormat();
            string cubeShaderDataPropertyPath =
                nameof(ObservableRaymarchedObjectTypeRelatedShaderData.CubeShaderData).ToBackingFieldFormat();

            _typeProperty            = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.Type);
            _typeValueProperty       = _typeProperty.FindPropertyRelative(_observablePropertyValuePath);
            _typeRelatedDataProperty = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.TypeRelatedData);
            _transformProperty       = serializedObject.FindProperty(RaymarchedObject3D.FieldNames.Transform);

            _typeRelatedDataProperties = new Dictionary<RaymarchedObjectType, SerializedProperty>
            {
                {
                    RaymarchedObjectType.Sphere,
                    _typeRelatedDataProperty.FindPropertyRelative(sphereShaderDataPropertyPath)
                        .FindPropertyRelative(_observablePropertyValuePath)
                },
                {
                    RaymarchedObjectType.Cube,
                    _typeRelatedDataProperty.FindPropertyRelative(cubeShaderDataPropertyPath)
                        .FindPropertyRelative(_observablePropertyValuePath)
                }
            };

            InitializeTypeRelatedDataResetters();
        }

        private void InitializeTypeRelatedDataResetters()
        {
            _typeRelatedDataResetters = new Dictionary<RaymarchedObjectType, Action>();
            SerializedProperty sphereDiameterProperty = _typeRelatedDataProperties[RaymarchedObjectType.Sphere]
                .FindPropertyRelative(nameof(RaymarchedSphereShaderData.Diameter));

            SerializedProperty cubeDimensionsProperty = _typeRelatedDataProperties[RaymarchedObjectType.Cube]
                .FindPropertyRelative(nameof(RaymarchedCubeShaderData.Dimensions));

            _typeRelatedDataResetters = new Dictionary<RaymarchedObjectType, Action>
            {
                {
                    RaymarchedObjectType.Sphere,
                    () => sphereDiameterProperty.floatValue = RaymarchedSphereShaderData.Default.Diameter
                },
                {
                    RaymarchedObjectType.Cube,
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
            var type = (RaymarchedObjectType)_typeValueProperty.enumValueIndex;
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