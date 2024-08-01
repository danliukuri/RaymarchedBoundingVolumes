using UnityEditor;
using UnityEngine;

namespace RaymarchedBoundingVolumes.Editor
{
    [InitializeOnLoad]
    public class UndoRedoCallbacksFixer
    {
        private static readonly string[] _methodNames = { "OnTransformChildrenChanged" };

        static UndoRedoCallbacksFixer() => Undo.undoRedoPerformed += OnUndoRedoPerformed;

        private static void OnUndoRedoPerformed()
        {
            foreach (Transform transform in Object.FindObjectsOfType<Transform>())
                if (transform.hasChanged)
                {
                    foreach (string methodName in _methodNames)
                        transform.SendMessage(methodName, SendMessageOptions.DontRequireReceiver);
                    transform.hasChanged = false;
                }
        }
    }
}