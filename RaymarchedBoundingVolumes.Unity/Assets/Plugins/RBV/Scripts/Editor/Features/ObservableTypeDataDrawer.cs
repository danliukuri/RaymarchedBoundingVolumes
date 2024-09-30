using System;
using System.Collections.Generic;
using System.Linq;
using RBV.Editor.Utilities.Extensions;
using RBV.Utilities.Extensions;
using RBV.Utilities.Wrappers;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Features
{
    public abstract class ObservableTypeDataDrawer<T> : PropertyDrawer
    {
        private readonly string _observablePropertyValuePath = nameof(ObservableValue<T>.Value).ToLower();

        private SerializedProperty _typeProperty;
        private GUIContent         _typeDataLabel;

        protected Dictionary<T, string>             _typeDataPropertyPaths;
        protected Dictionary<T, SerializedProperty> _typeDataProperties;
        private   Dictionary<T, Action>             _typeDataResetters;

        private bool _isInitialized;
        private T    _previousSelectedType;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _isInitialized.IfNotInvoke(() => Initialize(property)).IfNotSet(true);
            Initialize(property);
            using (new EditorGUI.PropertyScope(position, label, property))
                DrawProperty(property);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
            -EditorGUIUtility.standardVerticalSpacing;

        private ObservableTypeDataDrawer<T> Initialize(SerializedProperty property)
        {
            _typeDataLabel = new GUIContent(property.displayName);

            _typeProperty = property.serializedObject.FindProperty(GetTypePropertyPath())
                .FindPropertyRelative(_observablePropertyValuePath);

            _typeDataPropertyPaths = InitializeTypeDataPropertyPaths();
            T[] typeEnumeration = GetTypeEnumeration();
            _previousSelectedType = typeEnumeration.First();

            _typeDataProperties = typeEnumeration.ToDictionary(type => type, type => property
                .FindPropertyRelative(_typeDataPropertyPaths[type].ToBackingFieldFormat())
                .FindPropertyRelative(_observablePropertyValuePath));

            _typeDataResetters = InitializeTypeRelatedDataResetters();

            return this;
        }

        private void DrawProperty(SerializedProperty typeDataProperty)
        {
            if (_typeProperty.hasMultipleDifferentValues)
                return;

            T type = Cast(_typeProperty.enumValueFlag);

            if (!type.Equals(_previousSelectedType) &&
                _typeDataResetters.TryGetValue(_previousSelectedType, out Action resetter))
                resetter.Invoke();
            _previousSelectedType = type;

            if (_typeDataProperties.TryGetValue(type, out SerializedProperty property))
                if (DrawOnlyChildren())
                    property.DrawChildren(typeDataProperty.depth);
                else
                    property.DrawProperty(_typeDataLabel, typeDataProperty.depth);
        }

        protected virtual bool DrawOnlyChildren() => false;

        protected abstract string GetTypePropertyPath();

        protected abstract T[] GetTypeEnumeration();

        protected abstract T Cast(int enumValueFlag);

        protected abstract Dictionary<T, string> InitializeTypeDataPropertyPaths();

        protected abstract Dictionary<T, Action> InitializeTypeRelatedDataResetters();
    }
}