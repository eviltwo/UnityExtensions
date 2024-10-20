using System;
using System.Collections.Generic;

namespace eviltwo.UnityExtensions
{
    /// <summary>
    /// This class manages requests for cursor active and inactive, and returns the modified active value.
    /// Calling Dispose() on the request will revert the active changes.
    /// </summary>
    public class CursorRequestManager
    {
        private static CursorRequestManager _instance;
        public static CursorRequestManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CursorRequestManager();
                }
                return _instance;
            }
        }

        public bool PrioritizeActiveRequests = true;

        private readonly List<CusorActiveRequest> _cursorActiveRequests = new List<CusorActiveRequest>();

        public CusorActiveRequest RequestActive(bool active)
        {
            var handle = new CusorActiveRequest(active, this);
            _cursorActiveRequests.Add(handle);
            return handle;
        }

        internal void ReleasePause(CusorActiveRequest handle)
        {
            _cursorActiveRequests.Remove(handle);
        }

        public bool ShouldActive()
        {
            if (_cursorActiveRequests.Count == 0)
            {
                return true;
            }

            var hasPriorityActive = HasRequestAny(PrioritizeActiveRequests);
            return hasPriorityActive ? PrioritizeActiveRequests : !PrioritizeActiveRequests;
        }

        private bool HasRequestAny(bool activeTarget)
        {
            foreach (var request in _cursorActiveRequests)
            {
                if (request.IsActiveRequested == activeTarget)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Request active or inactive cursor. Dispose to revert the change.
        /// </summary>
        public class CusorActiveRequest : IDisposable
        {
            private readonly CursorRequestManager _manager;
            public readonly bool IsActiveRequested;

            public CusorActiveRequest(bool isActiveRequested, CursorRequestManager manager)
            {
                IsActiveRequested = isActiveRequested;
                _manager = manager;
            }

            public void Dispose()
            {
                _manager.ReleasePause(this);
            }
        }
    }
}
