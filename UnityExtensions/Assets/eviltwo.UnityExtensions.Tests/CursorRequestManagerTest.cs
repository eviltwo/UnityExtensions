using NUnit.Framework;

namespace eviltwo.UnityExtensions.Tests
{
    public class CursorRequestManagerTest
    {
        private CursorRequestManager _cursorRequestManager;

        [SetUp]
        public void SetUp()
        {
            _cursorRequestManager = new CursorRequestManager();
            _cursorRequestManager.PrioritizeActiveRequests = true;
        }

        [Test]
        public void EmptyRequest()
        {
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RequestActive()
        {
            var request = _cursorRequestManager.RequestActive(true);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DisposeActive()
        {
            var request = _cursorRequestManager.RequestActive(true);
            request.Dispose();
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RequestInactive()
        {
            var request = _cursorRequestManager.RequestActive(false);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void DisposeInactive()
        {
            var request = _cursorRequestManager.RequestActive(false);
            request.Dispose();
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PriorizeActive()
        {
            _cursorRequestManager.PrioritizeActiveRequests = true;
            var request1 = _cursorRequestManager.RequestActive(true);
            var request2 = _cursorRequestManager.RequestActive(false);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PriorizeInactive()
        {
            _cursorRequestManager.PrioritizeActiveRequests = false;
            var request1 = _cursorRequestManager.RequestActive(true);
            var request2 = _cursorRequestManager.RequestActive(false);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(false, result);
        }
    }
}
