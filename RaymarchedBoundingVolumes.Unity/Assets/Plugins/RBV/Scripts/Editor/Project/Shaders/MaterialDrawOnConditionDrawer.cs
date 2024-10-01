using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RBV.Editor.Project.Shaders
{
    public abstract class MaterialDrawOnConditionDrawer : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor)
        {
            if (editor.target is Material material && IsConditionMet(material))
                DrawGUI(position, prop, label, editor);
        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor) =>
            editor.target is Material material && IsConditionMet(material)
                ? CalculatePropertyHeight(prop, label, editor)
                : default;

        protected virtual float CalculatePropertyHeight(MaterialProperty prop, string label, MaterialEditor editor) =>
            MaterialEditor.GetDefaultPropertyHeight(prop);

        protected abstract bool IsConditionMet(Material material);

        protected virtual void DrawGUI(Rect position, MaterialProperty prop, GUIContent label, MaterialEditor editor) =>
            editor.DefaultShaderProperty(position, prop, label.text);
    }

    public class MaterialDrawIfOnDrawer : MaterialDrawOnConditionDrawer
    {
        private readonly string _shaderKeyword;

        public MaterialDrawIfOnDrawer(string keyword) => _shaderKeyword = keyword;

        protected override bool IsConditionMet(Material material) => material.IsKeywordEnabled(_shaderKeyword);
    }

    public class MaterialDrawIfOffDrawer : MaterialDrawOnConditionDrawer
    {
        private readonly string _shaderKeyword;

        public MaterialDrawIfOffDrawer(string keyword) => _shaderKeyword = keyword;

        protected override bool IsConditionMet(Material material) => !material.IsKeywordEnabled(_shaderKeyword);
    }

    public class MaterialDrawIfAnyOnDrawer : MaterialDrawOnConditionDrawer
    {
        private readonly string[] _shaderKeywords;

        public MaterialDrawIfAnyOnDrawer(params string[] keywords) => _shaderKeywords = keywords;

        protected override bool IsConditionMet(Material material) => _shaderKeywords.Any(material.IsKeywordEnabled);
    }
}