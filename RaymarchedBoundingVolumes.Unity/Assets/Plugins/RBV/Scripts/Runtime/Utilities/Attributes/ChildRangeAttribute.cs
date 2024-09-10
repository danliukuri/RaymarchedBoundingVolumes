using UnityEngine;

namespace RBV.Utilities.Attributes
{
    public class ChildRangeAttribute : PropertyAttribute
    {
        public string[] PropertyNames { get; set; }

        public float Min { get; set; }
        public float Max { get; set; }

        public ChildRangeAttribute(float min, float max, params string[] propertyNames)
        {
            Min           = min;
            Max           = max;
            PropertyNames = propertyNames;
        }
    }
}