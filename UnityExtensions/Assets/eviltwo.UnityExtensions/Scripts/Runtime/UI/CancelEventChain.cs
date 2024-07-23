using UnityEngine;
using UnityEngine.EventSystems;

namespace eviltwo.UnityExtensions
{
    /// <summary>
    /// Execute cancel event on parent objects.
    /// <code>
    /// Hierarchy example:
    /// Canvas
    ///   Panel (MyComponent : ICancelHandler)
    ///     Button (CancelEventChain)
    ///     Button (CancelEventChain)
    /// </code>
    /// </summary>
    public class CancelEventChain : MonoBehaviour, ICancelHandler
    {
        public void OnCancel(BaseEventData eventData)
        {
            var parent = transform.parent;
            if (parent == null)
            {
                return;
            }
            ExecuteEvents.ExecuteHierarchy(parent.gameObject, eventData, ExecuteEvents.cancelHandler);
        }
    }
}
