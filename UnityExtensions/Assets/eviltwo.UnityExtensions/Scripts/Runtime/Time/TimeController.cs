using System;
using UnityEngine;

namespace eviltwo.UnityExtensions
{
    [Obsolete("Use " + nameof(TimeRequestManager) + " instead.")]
    public class TimeController
    {
        private bool _enablePauseControl = true;
        public bool EnablePauseControl
        {
            get => _enablePauseControl;
            set
            {
                _enablePauseControl = value;
                UpdateTimeScaleForPause();
            }
        }

        private bool _paused = false;
        public bool Paused
        {
            get => _paused;
            set
            {
                _paused = value;
                UpdateTimeScaleForPause();
            }
        }

        private void UpdateTimeScaleForPause()
        {
            var actuallyPaused = _enablePauseControl && _paused;
            Time.timeScale = actuallyPaused ? 0f : 1f;
        }
    }
}
