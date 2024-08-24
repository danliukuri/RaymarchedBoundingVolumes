using System;
using System.Collections.Generic;
using RaymarchedBoundingVolumes.Data.Dynamic.ShaderData.ObjectTypeRelated;
using RaymarchedBoundingVolumes.Data.Static;
using RaymarchedBoundingVolumes.Data.Static.Enumerations;
using RaymarchedBoundingVolumes.Editor.Utilities.Extensions;
using RaymarchedBoundingVolumes.Features;
using RaymarchedBoundingVolumes.Utilities.Wrappers;
using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Editor.Features
{
    [CustomEditor(typeof(RaymarchedObject)), CanEditMultipleObjects]
    public class RaymarchedObjectEditor : UnityEditor.Editor
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
            string typePropertyPath            = nameof(RaymarchedObject.Type).ToBackingFieldFormat();
            string typeRelatedDataPropertyPath = nameof(RaymarchedObject.TypeRelatedData).ToBackingFieldFormat();
            string transformPropertyPath       = nameof(RaymarchedObject.Transform).ToBackingFieldFormat();

            string sphereShaderDataPropertyPath =
                nameof(ObservableRaymarchedObjectTypeRelatedShaderData.SphereShaderData).ToBackingFieldFormat();
            string cubeShaderDataPropertyPath =
                nameof(ObservableRaymarchedObjectTypeRelatedShaderData.CubeShaderData).ToBackingFieldFormat();

            _typeProperty            = serializedObject.FindProperty(typePropertyPath);
            _typeValueProperty       = _typeProperty.FindPropertyRelative(_observablePropertyValuePath);
            _typeRelatedDataProperty = serializedObject.FindProperty(typeRelatedDataPropertyPath);
            _transformProperty       = serializedObject.FindProperty(transformPropertyPath);

            _typeRelatedDataProperties = new Dictionary<RaymarchedObjectType, SerializedProperty>
            {
                {
                    RaymarchedObjectType.Sphere, 
                    _typeRelatedDataProperty.FindPropertyRelative(sphereShaderDataPropertyPath)
                },
                {
                    RaymarchedObjectType.Cube,
                    _typeRelatedDataProperty.FindPropertyRelative(cubeShaderDataPropertyPath)
                }
            };

            InitializeTypeRelatedDataResetters();
        }

        private void InitializeTypeRelatedDataResetters()
        {
            _typeRelatedDataResetters = new Dictionary<RaymarchedObjectType, Action>();
            SerializedProperty sphereShaderDataValueProperty = _typeRelatedDataProperties[RaymarchedObjectType.Sphere]
                .FindPropertyRelative(_observablePropertyValuePath);
            SerializedProperty sphereDiameterProperty =
                sphereShaderDataValueProperty.FindPropertyRelative(nameof(RaymarchedSphereShaderData.Diameter));

            SerializedProperty cubeShaderDataValueProperty = _typeRelatedDataProperties[RaymarchedObjectType.Cube]
                .FindPropertyRelative(_observablePropertyValuePath);
            SerializedProperty cubeDimensionsProperty =
                cubeShaderDataValueProperty.FindPropertyRelative(nameof(RaymarchedCubeShaderData.Dimensions));

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
            EditorGUILayout.PropertyField(_typeProperty, new GUIContent(_typeProperty.displayName));

            DrawTypeRelatedDataProperty();

            EditorGUILayout.PropertyField(_transformProperty, new GUIContent(_transformProperty.displayName));
        }

        private void DrawTypeRelatedDataProperty()
        {
            var type = (RaymarchedObjectType)_typeValueProperty.enumValueIndex;
            if (type != _previousSelectedType)
                _typeRelatedDataResetters[_previousSelectedType].Invoke();
            _previousSelectedType = type;

            SerializedProperty typeRelatedProperty = _typeRelatedDataProperties[type];
            EditorGUILayout.PropertyField(typeRelatedProperty, new GUIContent(_typeRelatedDataProperty.displayName));
        }
    }
}