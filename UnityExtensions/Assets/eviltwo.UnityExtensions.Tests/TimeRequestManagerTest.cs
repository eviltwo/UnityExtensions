using NUnit.Framework;

namespace eviltwo.UnityExtensions.Tests
{
    public class TimeRequestManagerTest
    {
        private TimeScaleRequestManager _timeScaleRequestManager;

        [SetUp]
        public void SetUp()
        {
            _timeScaleRequestManager = new TimeScaleRequestManager();
        }

        #region TestsForPause
        [Test]
        public void NotPausedByDefault()
        {
            Assert.AreEqual(false, _timeScaleRequestManager.IsPaused());
        }

        [Test]
        public void Pause()
        {
            var request = _timeScaleRequestManager.RequestPause();
            Assert.AreEqual(true, _timeScaleRequestManager.IsPaused());
            request.Dispose();
        }

        [Test]
        public void Unpause()
        {
            var request = _timeScaleRequestManager.RequestPause();
            request.Dispose();
            Assert.AreEqual(false, _timeScaleRequestManager.IsPaused());
        }

        [Test]
        public void PauseByScale()
        {
            var request = _timeScaleRequestManager.RequestScale(0.0f);
            Assert.AreEqual(true, _timeScaleRequestManager.IsPaused());
            request.Dispose();
        }
        #endregion

        #region TestsForScale
        [Test]
        public void DefaultScale()
        {
            Assert.AreEqual(1.0f, _timeScaleRequestManager.GetTimeScale());
        }

        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(1.0f)]
        [TestCase(2.0f)]
        public void Scale(float scale)
        {
            var request = _timeScaleRequestManager.RequestScale(scale);
            Assert.AreEqual(scale, _timeScaleRequestManager.GetTimeScale());
            request.Dispose();
        }

        [Test]
        public void Unscale()
        {
            var request = _timeScaleRequestManager.RequestScale(0.1f);
            request.Dispose();
            Assert.AreEqual(1.0f, _timeScaleRequestManager.GetTimeScale());
        }

        [TestCase(0.1f, 0.2f, 0.1f)]
        [TestCase(0.2f, 0.1f, 0.1f)]
        public void MultipleScale(float scale1, float scale2, float expected)
        {
            var request1 = _timeScaleRequestManager.RequestScale(scale1);
            var request2 = _timeScaleRequestManager.RequestScale(scale2);
            Assert.AreEqual(expected, _timeScaleRequestManager.GetTimeScale());
            request1.Dispose();
            request2.Dispose();
        }

        [TestCase(0.1f, 0.2f, 0.1f)]
        [TestCase(0.2f, 0.1f, 0.1f)]
        public void MultipleScale_Minimum(float scale1, float scale2, float expected)
        {
            var request1 = _timeScaleRequestManager.RequestScale(scale1);
            var request2 = _timeScaleRequestManager.RequestScale(scale2);
            Assert.AreEqual(expected, _timeScaleRequestManager.GetMinimumTimeScale());
            request1.Dispose();
            request2.Dispose();
        }

        [TestCase(0.1f, 0.2f, 0.2f)]
        [TestCase(0.2f, 0.1f, 0.2f)]
        public void MultipleScale_Maximum(float scale1, float scale2, float expected)
        {
            var request1 = _timeScaleRequestManager.RequestScale(scale1);
            var request2 = _timeScaleRequestManager.RequestScale(scale2);
            Assert.AreEqual(expected, _timeScaleRequestManager.GetMaximumTimeScale());
            request1.Dispose();
            request2.Dispose();
        }

        [Test]
        public void ScaleByPause()
        {
            var request = _timeScaleRequestManager.RequestPause();
            Assert.AreEqual(0.0f, _timeScaleRequestManager.GetTimeScale());
            request.Dispose();
        }
        #endregion
    }
}
