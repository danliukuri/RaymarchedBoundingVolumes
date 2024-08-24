namespace RaymarchedBoundingVolumes.Editor.Utilities.Extensions
{
    public static class StringExtensions
    {
        public const string BackingFieldFormat = "<{0}>k__BackingField";

        public static string ToBackingFieldFormat(this string propertyName) =>
            string.Format(BackingFieldFormat, propertyName);
    }
}