using System.Collections.Generic;

namespace eviltwo.UnityExtensions
{
    public class TaggedTimeRequestManager
    {
        private static TaggedTimeRequestManager _instance;
        public static TaggedTimeRequestManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TaggedTimeRequestManager();
                }
                return _instance;
            }
        }

        private readonly Dictionary<int, TimeRequestManager> _managers = new Dictionary<int, TimeRequestManager>();

        public TimePauseRequest RequestPause(string tag)
        {
            var manager = GetOrCreateManager(tag.GetHashCode());
            return manager.RequestPause();
        }

        public bool IsPaused(string tag)
        {
            if (_managers.TryGetValue(tag.GetHashCode(), out var manager))
            {
                return manager.IsPaused();
            }
            return false;
        }

        public TimeScaleRequest RequestScale(string tag, float scale)
        {
            var manager = GetOrCreateManager(tag.GetHashCode());
            return manager.RequestScale(scale);
        }

        public float GetTimeScale(string tag)
        {
            if (_managers.TryGetValue(tag.GetHashCode(), out var manager))
            {
                return manager.GetTimeScale();
            }
            return 1.0f;
        }

        public float GetMinimumTimeScale(string tag)
        {
            if (_managers.TryGetValue(tag.GetHashCode(), out var manager))
            {
                return manager.GetMinimumTimeScale();
            }
            return 1.0f;
        }

        public float GetMaximumTimeScale(string tag)
        {
            if (_managers.TryGetValue(tag.GetHashCode(), out var manager))
            {
                return manager.GetMaximumTimeScale();
            }
            return 1.0f;
        }

        private TimeRequestManager GetOrCreateManager(int hashCode)
        {
            if (!_managers.TryGetValue(hashCode, out var manager))
            {
                manager = new TimeRequestManager();
                _managers.Add(hashCode, manager);
            }
            return manager;
        }
    }
}
