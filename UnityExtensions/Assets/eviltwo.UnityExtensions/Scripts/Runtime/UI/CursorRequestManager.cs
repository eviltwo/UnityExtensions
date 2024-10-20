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

        /// <summary>
        /// If true, active request has higher priority than inactive request.
        /// If false, inactive request has higher priority than active request.
        /// </summary>
        public bool PrioritizeActiveWhenConfrict = true;


        private readonly List<CusorActiveRequest> _cursorActiveRequests = new List<CusorActiveRequest>();

        /// <summary>
        /// Request active or inactive cursor.
        /// </summary>
        /// <param name="active">Cursor active</param>
        /// <param name="priority">Priority of the request. Higher value has higher priority.</param>
        /// <returns></returns>
        public CusorActiveRequest RequestActive(bool active, int priority)
        {
            var handle = new CusorActiveRequest(active, priority, this);
            _cursorActiveRequests.Add(handle);
            return handle;
        }

        internal void ReleasePause(CusorActiveRequest handle)
        {
            _cursorActiveRequests.Remove(handle);
        }

        public bool ShouldActive()
        {
            var highestPriority = int.MinValue;
            var active = true;
            foreach (var request in _cursorActiveRequests)
            {
                if (request.Priority > highestPriority)
                {
                    highestPriority = request.Priority;
                    active = request.IsActiveRequested;
                }
                else if (request.Priority == highestPriority && request.IsActiveRequested == PrioritizeActiveWhenConfrict)
                {
                    active = request.IsActiveRequested;
                }
            }
            return active;
        }

        /// <summary>
        /// Request active or inactive cursor. Dispose to revert the change.
        /// </summary>
        public class CusorActiveRequest : IDisposable
        {
            private readonly CursorRequestManager _manager;
            public readonly bool IsActiveRequested;
            public readonly int Priority;

            public CusorActiveRequest(bool isActiveRequested, int priority, CursorRequestManager manager)
            {
                IsActiveRequested = isActiveRequested;
                Priority = priority;
                _manager = manager;
            }

            public void Dispose()
            {
                _manager.ReleasePause(this);
            }
        }
    }
}
