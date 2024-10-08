#if INPUT_SYSTEM

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace eviltwo.UnityExtensions
{
    public class InputSystemAxisTrigger : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _axisInputAction = null;

        [SerializeField]
        public UnityEvent OnMovePositive = default;

        [SerializeField]
        public UnityEvent OnMoveNegative = default;

        private void OnEnable()
        {
            if (_axisInputAction != null && _axisInputAction.action != null)
            {
                _axisInputAction.action.Enable();
                _axisInputAction.action.performed += OnPerformed;
            }
        }

        private void OnDisable()
        {
            if (_axisInputAction != null && _axisInputAction.action != null)
            {
                _axisInputAction?.action.Disable();
                _axisInputAction.action.performed -= OnPerformed;
            }
        }

        private void OnPerformed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var value = context.ReadValue<float>();
                if (value > 0)
                {
                    OnMovePositive?.Invoke();
                }
                else if (value < 0)
                {
                    OnMoveNegative?.Invoke();
                }
            }
        }
    }
}

#endif // INPUT_SYSTEM