using NUnit.Framework;

namespace eviltwo.UnityExtensions.Tests
{
    public class DataKeeperTest
    {
        private class DummyClass
        {
            public int Value;
        }

        [SetUp]
        public void SetUp()
        {
            DataKeeper.ClearAll();
        }

        [TestCase("key1", 1)]
        [TestCase("key2", 123)]
        public void SetAndGetData(string key, int value)
        {
            DataKeeper.SetData(key, new DummyClass { Value = value });
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>(key, out var result));
            Assert.AreEqual(value, result.Value);
        }

        [TestCase("key1", 1, 123)]
        [TestCase("key2", 2, 456)]
        public void SetAndGetDataTwice(string key, int value1, int value2)
        {
            DataKeeper.SetData(key, new DummyClass { Value = value1 });
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>(key, out var result));
            Assert.AreEqual(value1, result.Value);

            DataKeeper.SetData(key, new DummyClass { Value = value2 });
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>(key, out result));
            Assert.AreEqual(value2, result.Value);
        }

        [TestCase("key1", 1, "key2", 123)]
        [TestCase("key3", 2, "key4", 456)]
        public void SetAndGetMultipleData(string key1, int value1, string key2, int value2)
        {
            DataKeeper.SetData(key1, new DummyClass { Value = value1 });
            DataKeeper.SetData(key2, new DummyClass { Value = value2 });
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>(key1, out var result1));
            Assert.AreEqual(value1, result1.Value);
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>(key2, out var result2));
            Assert.AreEqual(value2, result2.Value);
        }

        [TestCase(1)]
        [TestCase(123)]
        public void SetAndGetDataWithoutKey(int value)
        {
            DataKeeper.SetData(new DummyClass { Value = value });
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>(out var result));
            Assert.AreEqual(value, result.Value);
        }

        [TestCase(1, 123)]
        [TestCase(2, 456)]
        public void SetAndGetDataTwiceWithoutKey(int value1, int value2)
        {
            DataKeeper.SetData(new DummyClass { Value = value1 });
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>(out var result));
            Assert.AreEqual(value1, result.Value);

            DataKeeper.SetData(new DummyClass { Value = value2 });
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>(out result));
            Assert.AreEqual(value2, result.Value);
        }

        public void ClearDataSingle()
        {
            DataKeeper.SetData("key1", new DummyClass { Value = 1 });
            DataKeeper.SetData("key2", new DummyClass { Value = 2 });
            DataKeeper.SetData("key3", new DummyClass { Value = 3 });
            DataKeeper.Clear("key2");
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>("key1", out _));
            Assert.IsFalse(DataKeeper.TryGetData<DummyClass>("key2", out _));
            Assert.IsTrue(DataKeeper.TryGetData<DummyClass>("key3", out _));
        }

        public void ClearDataAll()
        {
            DataKeeper.SetData("key1", new DummyClass { Value = 1 });
            DataKeeper.SetData("key2", new DummyClass { Value = 2 });
            DataKeeper.SetData("key3", new DummyClass { Value = 3 });
            DataKeeper.ClearAll();
            Assert.IsFalse(DataKeeper.TryGetData<DummyClass>("key1", out _));
            Assert.IsFalse(DataKeeper.TryGetData<DummyClass>("key2", out _));
            Assert.IsFalse(DataKeeper.TryGetData<DummyClass>("key3", out _));
        }
    }
}
