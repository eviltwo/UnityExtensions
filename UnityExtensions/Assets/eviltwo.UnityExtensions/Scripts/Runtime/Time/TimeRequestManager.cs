using System;
using System.Collections.Generic;
using UnityEngine;

namespace eviltwo.UnityExtensions
{
    /// <summary>
    /// This class manages requests for time pause and time scale, and returns the modified scale value.
    /// Calling Dispose() on the request will revert the time changes.
    /// </summary>
    public class TimeRequestManager
    {
        private static TimeRequestManager _instance;
        public static TimeRequestManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimeRequestManager();
                }
                return _instance;
            }
        }

        private readonly List<TimePauseRequest> _pauseRequests = new List<TimePauseRequest>();

        public TimePauseRequest RequestPause()
        {
            var handle = new TimePauseRequest(this);
            _pauseRequests.Add(handle);
            return handle;
        }

        internal void ReleasePause(TimePauseRequest handle)
        {
            _pauseRequests.Remove(handle);
        }

        private readonly List<TimeScaleRequest> _scaleRequests = new List<TimeScaleRequest>();

        public TimeScaleRequest RequestScale(float scale)
        {
            var handle = new TimeScaleRequest(this);
            handle.Scale = scale;
            _scaleRequests.Add(handle);
            return handle;
        }

        internal void ReleaseScale(TimeScaleRequest handle)
        {
            _scaleRequests.Remove(handle);
        }

        public bool IsPaused()
        {
            return GetTimeScale() == 0.0f;
        }

        public float GetTimeScale()
        {
            return GetMinimumTimeScale();
        }

        public float GetMinimumTimeScale()
        {
            if (_pauseRequests.Count > 0)
            {
                return 0.0f;
            }

            if (_scaleRequests.Count == 0)
            {
                return 1.0f;
            }

            var minScale = float.MaxValue;
            for (int i = 0; i < _scaleRequests.Count; i++)
            {
                minScale = Mathf.Min(minScale, _scaleRequests[i].Scale);
            }
            return Mathf.Max(minScale, 0);
        }

        public float GetMaximumTimeScale()
        {
            if (_pauseRequests.Count > 0)
            {
                return 0.0f;
            }

            if (_scaleRequests.Count == 0)
            {
                return 1.0f;
            }

            var maxScale = float.MinValue;
            for (int i = 0; i < _scaleRequests.Count; i++)
            {
                maxScale = Mathf.Max(maxScale, _scaleRequests[i].Scale);
            }
            return Mathf.Max(maxScale, 0);
        }
    }

    /// <summary>
    /// Pause the time. Dispose to resume.
    /// </summary>
    public class TimePauseRequest : IDisposable
    {
        private readonly TimeRequestManager _manager;

        public TimePauseRequest(TimeRequestManager manager)
        {
            _manager = manager;
        }

        public void Dispose()
        {
            _manager.ReleasePause(this);
        }
    }

    /// <summary>
    /// Scale the time. Dispose to reset.
    /// </summary>
    public class TimeScaleRequest : IDisposable
    {
        private readonly TimeRequestManager _manager;

        public float Scale { get; set; } = 1.0f;

        public TimeScaleRequest(TimeRequestManager manager)
        {
            _manager = manager;
        }

        public void Dispose()
        {
            _manager.ReleaseScale(this);
        }
    }
}
