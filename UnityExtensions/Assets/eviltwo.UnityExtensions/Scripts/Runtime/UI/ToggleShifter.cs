using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace eviltwo.UnityExtensions
{
    public class ToggleShifter : MonoBehaviour
    {
        [SerializeField]
        private ToggleGroup _toggleGroup = null;

        [SerializeField]
        private bool _loop = false;

        private static List<Toggle> _toggleBuffer = new List<Toggle>();

        private void Reset()
        {
            _toggleGroup = GetComponent<ToggleGroup>();
        }

        public void ShiftNext()
        {
            if (TryGetNextToggle(out var nextToggle))
            {
                nextToggle.isOn = true;
            }
        }

        public void ShiftPrevious()
        {
            if (TryGetPreviousToggle(out var previousToggle))
            {
                previousToggle.isOn = true;
            }
        }

        public bool TryGetNextToggle(out Toggle result)
        {
            return TryGetNeighborToggle(1, out result);
        }

        public bool TryGetPreviousToggle(out Toggle result)
        {
            return TryGetNeighborToggle(-1, out result);
        }

        public bool TryGetNeighborToggle(int diff, out Toggle result)
        {
            var activeToggle = _toggleGroup.GetFirstActiveToggle();
            if (activeToggle == null)
            {
                Debug.LogWarning("No active toggle found.");
                result = null;
                return false;
            }

            _toggleGroup.GetChildToggles(_toggleBuffer);
            var activeIndex = _toggleBuffer.IndexOf(activeToggle);
            if (activeIndex == -1)
            {
                Debug.LogWarning("Active toggle not found in the toggle group.");
                result = null;
                return false;
            }

            var nextIndex = activeIndex + diff;
            if (_loop)
            {
                nextIndex = (nextIndex + _toggleBuffer.Count) % _toggleBuffer.Count;
            }
            else if (nextIndex < 0 || nextIndex >= _toggleBuffer.Count)
            {
                result = null;
                return false;
            }

            result = _toggleBuffer[nextIndex];
            return true;
        }
    }
}
