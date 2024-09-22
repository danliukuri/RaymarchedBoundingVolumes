using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Shaders
{
    public class DrawIfOnDrawer : MaterialPropertyDrawer
    {
        private readonly string _shaderKeyword;

        public DrawIfOnDrawer(string keyword) => _shaderKeyword = keyword;

        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            if (IsKeywordEnabled(editor))
                editor.DefaultShaderProperty(position, prop, label.text);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor) =>
            IsKeywordEnabled(editor) ? MaterialEditor.GetDefaultPropertyHeight(prop) : default;

        private bool IsKeywordEnabled(MaterialEditor editor) =>
            editor.target is Material material && material.IsKeywordEnabled(_shaderKeyword);
    }
}