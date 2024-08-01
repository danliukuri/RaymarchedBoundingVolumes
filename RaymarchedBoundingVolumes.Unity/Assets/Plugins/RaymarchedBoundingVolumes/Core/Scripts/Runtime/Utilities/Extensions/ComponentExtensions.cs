using System.Collections.Generic;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Utilities.Extensions
{
    public static class ComponentExtensions
    {
        public static bool HasComponent<T>(this Component component) where T : Component =>
            component.TryGetComponent(out Component anotherComponent);
        
        public static IEnumerable<Transform> GetChildren(this Transform transform)
        {
            for (var i = 0; i < transform.childCount; i++)
                yield return transform.GetChild(i);
        }
    }
}