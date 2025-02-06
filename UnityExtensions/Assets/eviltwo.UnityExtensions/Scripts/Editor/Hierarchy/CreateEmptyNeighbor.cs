using UnityEditor;
using UnityEngine;

namespace eviltwo.UnityExtensions.Editor
{
    public static class CreateEmptyNeighbor
    {
        [MenuItem("GameObject/Create Empty Neighbor", false, 0)]
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
            gameObject.transform.localScale = activeTransform.localScale;
            gameObject.transform.SetSiblingIndex(Selection.activeGameObject.transform.GetSiblingIndex() + 1);
            Selection.activeGameObject = gameObject;
        }

        [MenuItem("GameObject/Create Empty Neighbor", true)]
        private static bool Validate()
        {
            return Selection.activeGameObject != null;
        }
    }
}
