using System;
using System.Collections.Generic;
using RBV.Data.Dynamic;
using RBV.Editor.Utilities.Extensions;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Features
{
    public class ObjectTypeDataDrawer
    {
        private readonly SerializedProperty _typeDataProperty;

        private readonly Dictionary<RaymarchedObjectType, SerializedProperty> _typeDataProperties;
        private readonly Dictionary<RaymarchedObjectType, Action>             _typeDataResetters;

        private RaymarchedObjectType _previousSelectedType;

        public ObjectTypeDataDrawer(SerializedProperty                                   typeDataProperty,
                                    Dictionary<RaymarchedObjectType, SerializedProperty> typeDataProperties,
                                    Dictionary<RaymarchedObjectType, Action>             typeDataResetters,
                                    RaymarchedObjectType                                 initialSelectedType)
        {
            _typeDataProperty     = typeDataProperty;
            _typeDataProperties   = typeDataProperties;
            _typeDataResetters    = typeDataResetters;
            _previousSelectedType = initialSelectedType;
        }

        public ObjectTypeDataDrawer DrawTypeDataProperty(RaymarchedObjectType type)
        {
            if (type != _previousSelectedType)
                _typeDataResetters[_previousSelectedType].Invoke();

            _previousSelectedType = type;

            _typeDataProperty.isExpanded = EditorGUILayout.Foldout(_typeDataProperty.isExpanded,
                new GUIContent(_typeDataProperty.displayName), true);

            if (_typeDataProperty.isExpanded)
            {
                SerializedProperty typeRelatedProperty = _typeDataProperties[type];
                using (new EditorGUI.IndentLevelScope())
                    foreach (SerializedProperty child in typeRelatedProperty.GetDirectChildren())
                        EditorGUILayout.PropertyField(child, true);
            }

            return this;
        }
    }
}