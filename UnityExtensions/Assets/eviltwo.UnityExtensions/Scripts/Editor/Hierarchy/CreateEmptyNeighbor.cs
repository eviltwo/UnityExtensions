using UnityEditor;
using UnityEngine;

namespace eviltwo.UnityExtensions.Editor
{
    public static class CreateEmptyNeighbor
    {
        [MenuItem("GameObject/Create Empty Neighbor", priority = 0, secondaryPriority = 4)]
        private static void Execute()
        {
            if (Selection.activeGameObject == null)
            {
                return;
            }

            var activeTransform = Selection.activeGameObject.transform;
            var gameObject = new GameObject("GameObject");
            gameObject.transform.SetParent(activeTransform.parent);
            gameObject.transform.SetPositionAndRotation(activeTransform.position, activeTransform.rotation);
            gameObject.transform.SetSiblingIndex(Selection.activeGameObject.transform.GetSiblingIndex() + 1);
            Undo.RegisterCreatedObjectUndo(gameObject, "Create Empty Neighbor");
            Selection.activeGameObject = gameObject;
        }

        [MenuItem("GameObject/Create Empty Neighbor", true)]
        private static bool Validate()
        {
            return Selection.activeGameObject != null;
        }
    }
}
