using UnityEngine;
using UnityEngine.UI;

namespace eviltwo.UnityExtensions
{
    /// <summary>
    /// This component enables navigation when the scrollbar is at either end.
    /// </summary>
    /// <remarks>
    /// For example, if the scrollbar is vertical, vertical navigation is disabled during scrolling, but downward navigation is enabled when scrolled to the bottom. Horizontal navigation is always enabled.
    /// </remarks>
    public class ScrollbarNavigationSwitcher : MonoBehaviour
    {
        private enum State
        {
            Min,
            Moving,
            Max,
        }

        [SerializeField]
        private Scrollbar _scrollbar = null;

        [SerializeField]
        private bool _reverseDirection = false;

        private readonly static Navigation _emptyNavigation = new Navigation
        {
            mode = Navigation.Mode.None
        };

        private Navigation _defaultNavigation;
        private State _lastState;

        private void Reset()
        {
            _scrollbar = GetComponent<Scrollbar>();
        }

        private void Awake()
        {
            if (_scrollbar == null)
            {
                _scrollbar = GetComponent<Scrollbar>();
            }
        }

        private void OnEnable()
        {
            _scrollbar.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _scrollbar.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void Start()
        {
            _defaultNavigation = _scrollbar.navigation;
            _lastState = GetState(_scrollbar);
            _scrollbar.navigation = GetCustomNavigation(_scrollbar, _lastState, _reverseDirection, _defaultNavigation);
        }

        private void OnValueChanged(float value)
        {
            var state = GetState(_scrollbar);
            if (state != _lastState)
            {
                _lastState = state;
                _scrollbar.navigation = GetCustomNavigation(_scrollbar, state, _reverseDirection, _defaultNavigation);
            }
        }

        private static State GetState(Scrollbar scrollbar)
        {
            if (scrollbar.value <= 0)
            {
                return State.Min;
            }
            else if (scrollbar.value >= 1)
            {
                return State.Max;
            }
            return State.Moving;
        }

        private static Navigation GetCustomNavigation(Scrollbar scrollbar, State state, bool reverse, in Navigation sampleNavigation)
        {
            if (state == State.Moving || sampleNavigation.mode == Navigation.Mode.None)
            {
                return _emptyNavigation;
            }

            var enableDown = false;
            var enableUp = false;
            var enableLeft = false;
            var enableRight = false;

            // Enable it if the direction is different.
            enableUp |= scrollbar.direction == Scrollbar.Direction.LeftToRight || scrollbar.direction == Scrollbar.Direction.RightToLeft;
            enableDown |= scrollbar.direction == Scrollbar.Direction.LeftToRight || scrollbar.direction == Scrollbar.Direction.RightToLeft;
            enableLeft |= scrollbar.direction == Scrollbar.Direction.BottomToTop || scrollbar.direction == Scrollbar.Direction.TopToBottom;
            enableRight |= scrollbar.direction == Scrollbar.Direction.BottomToTop || scrollbar.direction == Scrollbar.Direction.TopToBottom;

            // Enable it if the state is Min or Max.
            enableDown |= !reverse && scrollbar.direction == Scrollbar.Direction.BottomToTop && state == State.Min;
            enableDown |= reverse && scrollbar.direction == Scrollbar.Direction.TopToBottom && state == State.Max;

            enableUp |= !reverse && scrollbar.direction == Scrollbar.Direction.BottomToTop && state == State.Max;
            enableUp |= reverse && scrollbar.direction == Scrollbar.Direction.TopToBottom && state == State.Min;

            enableLeft |= !reverse && scrollbar.direction == Scrollbar.Direction.LeftToRight && state == State.Min;
            enableLeft |= reverse && scrollbar.direction == Scrollbar.Direction.RightToLeft && state == State.Max;

            enableRight |= !reverse && scrollbar.direction == Scrollbar.Direction.LeftToRight && state == State.Max;
            enableRight |= reverse && scrollbar.direction == Scrollbar.Direction.RightToLeft && state == State.Min;

            // Edit the navigation according to the enabled state.
            var customNavigation = new Navigation
            {
                mode = Navigation.Mode.Explicit
            };

            if (enableDown)
            {
                if ((sampleNavigation.mode & Navigation.Mode.Explicit) != 0)
                {
                    customNavigation.selectOnDown = sampleNavigation.selectOnDown;
                }
                if ((sampleNavigation.mode & Navigation.Mode.Vertical) != 0)
                {
                    customNavigation.selectOnDown = scrollbar.FindSelectable(Vector3.down);
                }
            }

            if (enableUp)
            {
                if ((sampleNavigation.mode & Navigation.Mode.Explicit) != 0)
                {
                    customNavigation.selectOnUp = sampleNavigation.selectOnUp;
                }
                if ((sampleNavigation.mode & Navigation.Mode.Vertical) != 0)
                {
                    customNavigation.selectOnUp = scrollbar.FindSelectable(Vector3.up);
                }
            }

            if (enableLeft)
            {
                if ((sampleNavigation.mode & Navigation.Mode.Explicit) != 0)
                {
                    customNavigation.selectOnLeft = sampleNavigation.selectOnLeft;
                }
                if ((sampleNavigation.mode & Navigation.Mode.Horizontal) != 0)
                {
                    customNavigation.selectOnLeft = scrollbar.FindSelectable(Vector3.left);
                }
            }

            if (enableRight)
            {
                if ((sampleNavigation.mode & Navigation.Mode.Explicit) != 0)
                {
                    customNavigation.selectOnRight = sampleNavigation.selectOnRight;
                }
                if ((sampleNavigation.mode & Navigation.Mode.Horizontal) != 0)
                {
                    customNavigation.selectOnRight = scrollbar.FindSelectable(Vector3.right);
                }
            }

            return customNavigation;
        }
    }
}
