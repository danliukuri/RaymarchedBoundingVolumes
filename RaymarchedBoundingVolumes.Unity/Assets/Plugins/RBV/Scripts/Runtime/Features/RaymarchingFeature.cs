using UnityEngine;

namespace RBV.Features
{
    public abstract class RaymarchingFeature : MonoBehaviour
    {
        public bool IsActive => enabled && gameObject is { activeSelf: true, activeInHierarchy: true };
    }
}