using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace eviltwo.UnityExtensions
{
    public static class ToggleGroupExtension
    {
        private static List<Toggle> _toggleBuffer = new List<Toggle>();
        public static void GetChildToggles(this ToggleGroup toggleGroup, List<Toggle> results)
        {
            _toggleBuffer.Clear();
            toggleGroup.GetComponentsInChildren(_toggleBuffer);
            results.Clear();
            var groupedToggles = _toggleBuffer
                .Where(v => v.group == toggleGroup)
                .OrderBy(v => v.transform.GetSiblingIndex());
            results.AddRange(groupedToggles);
            _toggleBuffer.Clear();
        }
    }
}
