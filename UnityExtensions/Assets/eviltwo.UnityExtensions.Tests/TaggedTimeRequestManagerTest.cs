using NUnit.Framework;

namespace eviltwo.UnityExtensions.Tests
{
    public class TaggedTimeRequestManagerTest
    {
        private TaggedTimeRequestManager _taggedTimeRequestManager;

        [SetUp]
        public void SetUp()
        {
            _taggedTimeRequestManager = new TaggedTimeRequestManager();
        }

        //
        // Tests for Pause
        //
        [Test]
        public void NotPausedByDefault()
        {
            Assert.AreEqual(false, _taggedTimeRequestManager.IsPaused("abc"));
        }

        [Test]
        public void Pause()
        {
            var request = _taggedTimeRequestManager.RequestPause("abc");
            Assert.AreEqual(true, _taggedTimeRequestManager.IsPaused("abc"));
            request.Dispose();
        }

        [Test]
        public void Unpause()
        {
            var request = _taggedTimeRequestManager.RequestPause("abc");
            request.Dispose();
            Assert.AreEqual(false, _taggedTimeRequestManager.IsPaused("abc"));
        }

        [Test]
        public void PauseDifferentTags()
        {
            var request = _taggedTimeRequestManager.RequestPause("abc");
            Assert.AreEqual(false, _taggedTimeRequestManager.IsPaused("def"));
            request.Dispose();
        }

        //
        // Tests for Scale
        //
        [Test]
        public void NotScaledByDefault()
        {
            Assert.AreEqual(1.0f, _taggedTimeRequestManager.GetTimeScale("abc"));
        }

        [TestCase(0.0f)]
        [TestCase(0.1f)]
        [TestCase(1.0f)]
        [TestCase(2.0f)]
        public void Scale(float scale)
        {
            var request = _taggedTimeRequestManager.RequestScale("abc", scale);
            Assert.AreEqual(scale, _taggedTimeRequestManager.GetTimeScale("abc"));
            request.Dispose();
        }

        [Test]
        public void Unscale()
        {
            var request = _taggedTimeRequestManager.RequestScale("abc", 0.1f);
            request.Dispose();
            Assert.AreEqual(1.0f, _taggedTimeRequestManager.GetTimeScale("abc"));
        }

        [Test]
        public void ScaleDifferentTags()
        {
            var request = _taggedTimeRequestManager.RequestScale("abc", 0.0f);
            Assert.AreEqual(1.0f, _taggedTimeRequestManager.GetTimeScale("def"));
            request.Dispose();
        }
    }
}
