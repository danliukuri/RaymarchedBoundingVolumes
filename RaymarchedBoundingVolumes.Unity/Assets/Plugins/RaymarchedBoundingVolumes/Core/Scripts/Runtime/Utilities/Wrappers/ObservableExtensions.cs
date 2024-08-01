using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Wrappers
{
    public static class ObservableExtensions
    {
        public static ObservableTransform<Vector3> SetValuesFrom(this ObservableTransform<Vector3> observableTransform,
            Transform transform)
        {
            observableTransform.Position.Value = transform.position;
            observableTransform.Rotation.Value = transform.rotation.eulerAngles;
            observableTransform.Scale   .Value = transform.localScale;

            return observableTransform;
        }
    }
}