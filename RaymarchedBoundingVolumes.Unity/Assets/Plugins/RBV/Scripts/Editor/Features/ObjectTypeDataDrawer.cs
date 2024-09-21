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
        private readonly GUIContent         _typeDataLabel;

        private readonly Dictionary<RaymarchedObjectType, SerializedProperty> _typeDataProperties;
        private readonly Dictionary<RaymarchedObjectType, Action>             _typeDataResetters;

        private RaymarchedObjectType _previousSelectedType;

        public ObjectTypeDataDrawer(SerializedProperty                                   typeDataProperty,
                                    Dictionary<RaymarchedObjectType, SerializedProperty> typeDataProperties,
                                    Dictionary<RaymarchedObjectType, Action>             typeDataResetters,
                                    RaymarchedObjectType                                 initialSelectedType)
        {
            _typeDataProperty     = typeDataProperty;
            _typeDataLabel        = new GUIContent(_typeDataProperty.displayName);
            _typeDataProperties   = typeDataProperties;
            _typeDataResetters    = typeDataResetters;
            _previousSelectedType = initialSelectedType;
        }

        public ObjectTypeDataDrawer DrawTypeDataProperty(RaymarchedObjectType type)
        {
            if (type != _previousSelectedType)
                _typeDataResetters[_previousSelectedType].Invoke();
            _previousSelectedType = type;

            _typeDataProperties[type].DrawProperty(_typeDataLabel, _typeDataProperty.depth);

            return this;
        }
    }
}