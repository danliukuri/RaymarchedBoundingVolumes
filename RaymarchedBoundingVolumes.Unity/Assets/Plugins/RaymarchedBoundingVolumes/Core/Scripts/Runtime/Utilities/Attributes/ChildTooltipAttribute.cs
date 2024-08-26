using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Attributes
{
    public class ChildTooltipAttribute : PropertyAttribute
    {
        public string Tooltip   { get; }
        public string FieldName { get; }

        public ChildTooltipAttribute(string fieldName, string tooltip)
        {
            FieldName = fieldName;
            Tooltip   = tooltip;
        }
    }
}