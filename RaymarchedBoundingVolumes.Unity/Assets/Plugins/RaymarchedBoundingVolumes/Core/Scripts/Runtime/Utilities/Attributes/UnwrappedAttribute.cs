using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Attributes
{
    public class UnwrappedAttribute : PropertyAttribute
    {
        private const string DefaultWrappedValueName = "value";

        public string FieldName { get; }

        public UnwrappedAttribute(string fieldName = DefaultWrappedValueName) => FieldName = fieldName;
    }
}