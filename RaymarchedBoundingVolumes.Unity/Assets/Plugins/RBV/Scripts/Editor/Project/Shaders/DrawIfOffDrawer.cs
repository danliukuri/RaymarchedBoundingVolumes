using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Shaders
{
    public class DrawIfOffDrawer : MaterialPropertyDrawer
    {
        private readonly string _shaderKeyword;

        public DrawIfOffDrawer(string keyword) => _shaderKeyword = keyword;

        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            if (IsKeywordDisabled(editor))
                editor.DefaultShaderProperty(position, prop, label.text);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor) =>
            IsKeywordDisabled(editor) ? MaterialEditor.GetDefaultPropertyHeight(prop) : default;

        private bool IsKeywordDisabled(MaterialEditor editor) =>
            editor.target is Material material && !material.IsKeywordEnabled(_shaderKeyword);
    }
}