#if INPUT_SYSTEM
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace eviltwo.UnityExtensions
{
    public class CursorActiveControllerForGamepad : MonoBehaviour
    {
        [SerializeField]
        private PlayerInput _playerInput = null;

        [SerializeField]
        private bool _autoCorrectPlayerInputWhenNull = true;

        [SerializeField]
        private string[] _gamepadSchemeNames = new string[] { "Gamepad" };

        [SerializeField]
        private int _requestPriority = 0;

        private CursorActiveRequest _cursorRequest;

        private void Reset()
        {
            _playerInput = FindAnyObjectByType<PlayerInput>();
        }

        private void OnDisable()
        {
            if (_cursorRequest != null)
            {
                _cursorRequest.Dispose();
                _cursorRequest = null;
            }
        }

        private void Update()
        {
            if (_playerInput == null && _autoCorrectPlayerInputWhenNull)
            {
                _playerInput = FindAnyObjectByType<PlayerInput>();
            }
            if (_playerInput == null)
            {
                return;
            }

            var usingGamepad = Array.IndexOf(_gamepadSchemeNames, _playerInput.currentControlScheme) >= 0;
            if (usingGamepad)
            {
                if (_cursorRequest == null)
                {
                    _cursorRequest = CursorRequestManager.Instance.RequestActive(false, _requestPriority);
                }
            }
            else
            {
                if (_cursorRequest != null)
                {
                    _cursorRequest.Dispose();
                    _cursorRequest = null;
                }
            }
        }
    }
}
#endif // INPUT_SYSTEM
