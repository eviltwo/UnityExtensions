#if INPUT_SYSTEM
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

namespace eviltwo.UnityExtensions
{
    /// <summary>
    /// When input from button devices such as keyboard or gamepad is detected, the UI is automatically selected.
    /// When input from pointer devices such as a mouse is detected, the UI selection is automatically canceled.
    /// </summary>
    public class AutoSelectionForButtonDevice : MonoBehaviour
    {
        private enum Control
        {
            None = 0,
            Button,
            Pointer,
        }

        private Control _detectedControl;
        private Control _currentControl;

        /// <summary>
        /// This is a method for searching for the UI to select.
        /// It can be overridden by a custom class that implements <see cref="ISelectableFinder"/>.
        /// </summary>
        public ISelectableFinder SelectableFinder { get; set; } = new FirstSelectableFinder();

        private void Start()
        {
            InputSystem.onEvent
                .Where(e => e.type == StateEvent.Type || e.type == DeltaStateEvent.Type)
                .Call(e =>
                {
                    var control = e.EnumerateChangedControls().FirstOrDefault();
                    _detectedControl = control?.device is Pointer ? Control.Pointer : Control.Button;
                });
        }

        private void Update()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null || eventSystem.alreadySelecting)
            {
                return;
            }

            if (_detectedControl == _currentControl)
            {
                if (_currentControl == Control.Button && eventSystem.currentSelectedGameObject == null)
                {
                    SelectObject();
                }
            }
            else
            {
                _currentControl = _detectedControl;
                if (_currentControl == Control.Button)
                {
                    SelectObject();
                }
                else if (_currentControl == Control.Pointer)
                {
                    EventSystem.current.SetSelectedGameObject(null);
                }
            }
        }

        private void SelectObject()
        {
            var selectable = SelectableFinder?.Find(gameObject);
            selectable?.Select();
        }

        /// <summary>
        /// Find for the interactable Selectable that is closest to the root of the hierarchy.
        /// </summary>
        private class FirstSelectableFinder : ISelectableFinder
        {
            private static readonly List<Selectable> SelectableBuffer = new List<Selectable>();
            public Selectable Find(GameObject root)
            {
                root.GetComponentsInChildren(SelectableBuffer);
                var count = SelectableBuffer.Count;
                for (var i = 0; i < count; i++)
                {
                    var selectable = SelectableBuffer[i];
                    if (selectable.IsInteractable())
                    {
                        return selectable;
                    }
                }
                return null;
            }
        }
    }

    public interface ISelectableFinder
    {
        Selectable Find(GameObject root);
    }
}
#endif
