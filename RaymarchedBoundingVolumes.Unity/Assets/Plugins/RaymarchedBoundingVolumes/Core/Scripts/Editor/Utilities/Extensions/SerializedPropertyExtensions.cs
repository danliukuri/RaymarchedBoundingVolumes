using System.Collections.Generic;
using UnityEditor;

namespace RaymarchedBoundingVolumes.Editor.Utilities.Extensions
{
    public static class SerializedPropertyExtensions
    {
        public static IEnumerable<SerializedProperty> GetDirectChildren(this SerializedProperty property) =>
            property.GetChildren(false);

        private static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property) =>
            property.GetChildren(true);

        private static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property, bool isRecursive)
        {
            if (property is not { hasChildren: true })
                yield break;

            SerializedProperty iterator    = property.Copy();
            SerializedProperty endProperty = iterator.GetEndProperty(true);

            iterator.NextVisible(true);

            while (!SerializedProperty.EqualContents(iterator, endProperty))
            {
                yield return iterator.Copy();
                iterator.NextVisible(isRecursive);
            }
        }
    }
}