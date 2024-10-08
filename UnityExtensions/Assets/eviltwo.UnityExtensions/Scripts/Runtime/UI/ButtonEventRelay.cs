using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace eviltwo.UnityExtensions
{
    public class ButtonEventRelay : MonoBehaviour
    {
        [SerializeField]
        private Button _button = null;

        [SerializeField]
        public UnityEvent OnClick = null;

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        private void Awake()
        {
            _button.onClick.AddListener(OnClick.Invoke);
        }
    }
}
