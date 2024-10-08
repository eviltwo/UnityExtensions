
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace eviltwo.UnityExtensions
{
    /// <summary>
    /// Limit the preferredWidth and preferredHeight by the maximum value. If the preferred values of other ILayoutElements are below the maximum, do nothing.
    /// </summary>
    public class LayoutSizeLimiter : UIBehaviour, ILayoutElement
    {
        [SerializeField]
        private bool _limitWidth = false;

        [SerializeField]
        private float _maxWidth = 100;

        [SerializeField]
        private bool _limitHeight = false;

        [SerializeField]
        private float _maxHeight = 100;

        [SerializeField]
        private int layoutPriority = 1;

        void ILayoutElement.CalculateLayoutInputHorizontal()
        {
        }

        void ILayoutElement.CalculateLayoutInputVertical()
        {
        }

        float ILayoutElement.minWidth => -1;

        float ILayoutElement.minHeight => -1;

        float ILayoutElement.preferredWidth
        {
            get
            {
                if (!_limitWidth)
                {
                    return -1;
                }
                var othersValue = GetOtherLayoutProperty(e => e.preferredWidth);
                if (othersValue < _maxWidth)
                {
                    return -1;
                }

                return _maxWidth;
            }
        }

        float ILayoutElement.preferredHeight
        {
            get
            {
                if (!_limitHeight)
                {
                    return -1;
                }
                var othersValue = GetOtherLayoutProperty(e => e.preferredHeight);
                if (othersValue < _maxHeight)
                {
                    return -1;
                }

                return _maxHeight;
            }
        }

        float ILayoutElement.flexibleWidth => -1;

        float ILayoutElement.flexibleHeight => -1;

        int ILayoutElement.layoutPriority => layoutPriority;

        private static List<ILayoutElement> _layoutElementsBuffer = new List<ILayoutElement>();

        private RectTransform _rectTransform;
        private RectTransform RectTransorm
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }
                return _rectTransform;
            }
        }

        /// <summary>
        /// Get property of other ILayoutElement.
        /// </summary>
        /// <remarks>
        /// Referenced: <see cref="LayoutUtility"/>
        /// </remarks>
        private float GetOtherLayoutProperty(System.Func<ILayoutElement, float> property)
        {
            _layoutElementsBuffer.Clear();
            GetComponents(_layoutElementsBuffer);
            var maxValue = 0f;
            var count = _layoutElementsBuffer.Count;
            for (int i = 0; i < count; i++)
            {
                var layoutElement = _layoutElementsBuffer[i];
                if (layoutElement == (ILayoutElement)this)
                {
                    continue;
                }
                if (layoutElement.layoutPriority > layoutPriority)
                {
                    continue;
                }
                var value = property(layoutElement);
                if (value < 0)
                {
                    continue;
                }
                if (value > maxValue)
                {
                    maxValue = value;
                }
            }
            _layoutElementsBuffer.Clear();
            return maxValue;
        }

        protected void SetDirty()
        {
            if (!IsActive())
                return;

            LayoutRebuilder.MarkLayoutForRebuild(RectTransorm);
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            SetDirty();
        }
#endif
    }
}
