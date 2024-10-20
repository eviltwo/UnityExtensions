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
            _cursorRequestManager.PrioritizeActiveWhenConfrict = true;
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
            var request = _cursorRequestManager.RequestActive(true, 0);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void DisposeActive()
        {
            var request = _cursorRequestManager.RequestActive(true, 0);
            request.Dispose();
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RequestInactive()
        {
            var request = _cursorRequestManager.RequestActive(false, 0);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void DisposeInactive()
        {
            var request = _cursorRequestManager.RequestActive(false, 0);
            request.Dispose();
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PriorizeActive()
        {
            _cursorRequestManager.PrioritizeActiveWhenConfrict = true;
            var request1 = _cursorRequestManager.RequestActive(true, 0);
            var request2 = _cursorRequestManager.RequestActive(false, 0);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }

        [Test]
        public void PriorizeInactive()
        {
            _cursorRequestManager.PrioritizeActiveWhenConfrict = false;
            var request1 = _cursorRequestManager.RequestActive(true, 0);
            var request2 = _cursorRequestManager.RequestActive(false, 0);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PriorizeActiveHigherPriority1()
        {
            _cursorRequestManager.PrioritizeActiveWhenConfrict = true;
            var request1 = _cursorRequestManager.RequestActive(true, 0);
            var request2 = _cursorRequestManager.RequestActive(false, 1);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void PriorizeActiveHigherPriority2()
        {
            _cursorRequestManager.PrioritizeActiveWhenConfrict = true;
            var request1 = _cursorRequestManager.RequestActive(true, 1);
            var request2 = _cursorRequestManager.RequestActive(false, 0);
            var result = _cursorRequestManager.ShouldActive();
            Assert.AreEqual(true, result);
        }
    }
}
