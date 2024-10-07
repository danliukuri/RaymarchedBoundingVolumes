using System.IO;
using RBV.Data.Static;
using UnityEditor;
using UnityEngine;
using static RBV.Data.Static.PathConstants;
using static RBV.Heatmapping.Editor.Utilities.Extensions.PathExtensions;

namespace RBV.Heatmapping.Editor.Features
{
#if !UNITY_2021_2_OR_NEWER
    [InitializeOnLoad]
    internal sealed class HeatmappingSettings
#else
    internal sealed class HeatmappingSettings
#endif
    {
        private const string PreferencePathRelative     = "Raymarched Bounding Volumes/Heatmapping";
        private const string PreferencePath             = "Preferences"          + Separator + PreferencePathRelative;
        private const string HeatmappingTexturePathKey  = PreferencePathRelative + Separator + "TexturePath";
        private const string HeatmappingEnabledKey      = PreferencePathRelative + Separator + "Enabled";
        private const string DefaultTexturePathRelative = "Art/Sprites/Heatmaps/HitmapMagma.png";

        private static readonly string _defaultTexturePath =
            Path.Combine(HeatmappingPackageRootPathRelativeToProjectOrDefault(), DefaultTexturePathRelative);

        private static readonly int _heatmapTexturePropertyID = Shader.PropertyToID("_RbvHeatmapTexture");

        private static Texture2D _heatmapTexture = InitializeHeatmapTexture();

        private static Texture2D HeatmapTexture
        {
            get => _heatmapTexture;
            set
            {
                if (value != default)
                {
                    _heatmapTexture    = value;
                    value.hideFlags    = HideFlags.DontSaveInEditor;
                    HeatmapTexturePath = AssetDatabase.GetAssetPath(value);
                    Shader.SetGlobalTexture(_heatmapTexturePropertyID, value);
                }
            }
        }

        private static string HeatmapTexturePath
        {
            get => EditorPrefs.GetString(HeatmappingTexturePathKey, _defaultTexturePath);
            set => EditorPrefs.SetString(HeatmappingTexturePathKey, value);
        }

        private static bool IsHeatmappingEnabled
        {
            get => EditorPrefs.GetBool(HeatmappingEnabledKey, default);
            set
            {
                EditorPrefs.SetBool(HeatmappingEnabledKey, value);
                Shader.SetKeyword(PackageRelatedShaderKeywords.RbvHeatmappingOn, value);
            }
        }

        private static Texture2D InitializeHeatmapTexture()
        {
            var heatmapTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(
                AssetDatabase.IsValidFolder(HeatmapTexturePath) ? HeatmapTexturePath : _defaultTexturePath);
            heatmapTexture.hideFlags = HideFlags.DontSaveInEditor;
            Shader.SetGlobalTexture(_heatmapTexturePropertyID, heatmapTexture);
            return heatmapTexture;
        }

#if UNITY_2019_1_OR_NEWER
        [SettingsProvider]
        private static SettingsProvider CreateHeatmappingSettingsProvider() => new(PreferencePath, SettingsScope.User)
        {
            guiHandler = searchContext => OnGUI(),
            keywords   = SettingsProvider.GetSearchKeywordsFromGUIContentProperties<Styles>()
        };
#else
        [PreferenceItem(PreferencePathRelative)]
#endif
        private static void OnGUI()
        {
            const float width   = 500f;
            const float padding = 3f;

            DrawEnabledToggle();
            DrawTexture(width, padding);
            DrawTexturePreview(width, padding);
        }

        private static void DrawTexture(float width, float padding)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            EditorGUI.LabelField(rect, Styles.Texture);
            float localPadding = padding * Mathf.Sqrt(0.5f);
            rect.xMin  += localPadding + EditorGUIUtility.labelWidth;
            rect.width =  width        - EditorGUIUtility.labelWidth - padding - (padding - localPadding);

            EditorGUI.BeginChangeCheck();
            var newTexture = (Texture2D)EditorGUI.ObjectField(rect, HeatmapTexture, typeof(Texture2D), false);
            if (EditorGUI.EndChangeCheck())
                HeatmapTexture = newTexture;
        }


        private static void DrawTexturePreview(float width, float padding)
        {
            float aspectRatio = (float)HeatmapTexture.height / HeatmapTexture.width;
            float height      = width                        * aspectRatio;

            GUILayout.BeginScrollView(Vector2.zero, false, false,
                GUILayout.Width(width + padding), GUILayout.Height(height));

            Rect textureRect = EditorGUILayout.GetControlRect();
            textureRect.width  = width;
            textureRect.height = height;

            GUILayout.BeginArea(textureRect);
            GUI.DrawTexture(textureRect, HeatmapTexture, ScaleMode.ScaleToFit);
            GUILayout.EndArea();

            GUILayout.EndScrollView();
        }

        private static void DrawEnabledToggle()
        {
            bool newHeatmappingEnabledState = EditorGUILayout.Toggle(Styles.Enabled, IsHeatmappingEnabled);
            if (EditorGUI.EndChangeCheck())
                IsHeatmappingEnabled = newHeatmappingEnabledState;
        }

        private class Styles
        {
            public static GUIContent Enabled { get; } = new("Enabled");
            public static GUIContent Texture { get; } = new("Texture");
        }
    }
}