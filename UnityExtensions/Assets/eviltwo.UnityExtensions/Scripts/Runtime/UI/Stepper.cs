using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace eviltwo.UnityExtensions
{
    public class Stepper : Selectable
    {
        [SerializeField]
        private int _value = 0;
        public int Value => _value;

        [SerializeField]
        public int MinValue = 0;

        [SerializeField]
        public int MaxValue = 10;

        public enum Axis
        {
            None = 0,
            Horizontal = 1,
            Vertical = 2,
        }

        [SerializeField]
        public Axis MoveAxis = Axis.Horizontal;

        [SerializeField]
        public bool ReverseMoveDirection = false;

        [SerializeField]
        public bool Loop = false;

        [SerializeField]
        private Button _decreaseButton = null;

        [SerializeField]
        private Button _increaseButton = null;

        [SerializeField]
        public UnityEvent<int> OnValueChanged = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (_decreaseButton != null)
            {
                _decreaseButton.onClick.AddListener(Decrease);
            }
            if (_increaseButton != null)
            {
                _increaseButton.onClick.AddListener(Increase);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            if (_decreaseButton != null)
            {
                _decreaseButton.onClick.RemoveListener(Decrease);
            }
            if (_increaseButton != null)
            {
                _increaseButton.onClick.RemoveListener(Increase);
            }
        }

        public override void OnMove(AxisEventData eventData)
        {
            var oldValue = _value;
            if (MoveAxis == Axis.Horizontal && eventData.moveDir == MoveDirection.Left
                || MoveAxis == Axis.Vertical && eventData.moveDir == MoveDirection.Down)
            {
                if (ReverseMoveDirection)
                {
                    Increase();
                }
                else
                {
                    Decrease();
                }
            }
            else if (MoveAxis == Axis.Horizontal && eventData.moveDir == MoveDirection.Right
                || MoveAxis == Axis.Vertical && eventData.moveDir == MoveDirection.Up)
            {
                if (ReverseMoveDirection)
                {
                    Decrease();
                }
                else
                {
                    Increase();
                }
            }

            if (oldValue == _value)
            {
                base.OnMove(eventData);
            }
        }

        public void SetValue(int value, bool withoutNotify = false)
        {
            if (Loop)
            {
                var size = MaxValue - MinValue + 1;
                _value = (value + size - MinValue) % size;
                _decreaseButton.interactable = true;
                _increaseButton.interactable = true;
            }
            else
            {
                _value = Mathf.Clamp(value, MinValue, MaxValue);
                _decreaseButton.interactable = _value > MinValue;
                _increaseButton.interactable = _value < MaxValue;
            }

            if (!withoutNotify)
            {
                OnValueChanged?.Invoke(_value);
            }
        }

        public void Decrease()
        {
            SetValue(_value - 1);
        }

        public void Increase()
        {
            SetValue(_value + 1);
        }
    }
}
