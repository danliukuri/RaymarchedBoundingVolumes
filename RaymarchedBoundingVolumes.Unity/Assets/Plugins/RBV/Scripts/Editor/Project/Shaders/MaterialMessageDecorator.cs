using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Shaders
{
    public class MaterialMessageDecorator : MaterialPropertyDrawer
    {
        private readonly string      _keyword;
        private readonly string      _message;
        private readonly MessageType _type;

        public MaterialMessageDecorator(string message, float type) : this(default, message, type) { }

        public MaterialMessageDecorator(string keyword, string message, float type)
        {
            _keyword = keyword;
            _message = message;
            _type    = (MessageType)type;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor editor)
        {
            if (_keyword == default || editor.target is Material material && material.IsKeywordEnabled(_keyword))
                EditorGUILayout.HelpBox(_message, _type);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor) => default;
    }

    public class MaterialInfoDecorator : MaterialMessageDecorator
    {
        private const float Type = (float)MessageType.Info;
        public MaterialInfoDecorator(string message) : base(message, Type) { }
        public MaterialInfoDecorator(string keyword, string message) : base(keyword, message, Type) { }
    }
}