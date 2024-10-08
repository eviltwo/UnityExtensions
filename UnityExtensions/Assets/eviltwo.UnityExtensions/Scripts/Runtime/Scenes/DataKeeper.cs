using System.Collections.Generic;

namespace eviltwo.UnityExtensions
{
    /// <summary>
    /// Keeps data between scenes.
    /// </summary>
    public static class DataKeeper
    {
        private static Dictionary<string, object> _contents = new Dictionary<string, object>();

        public static void SetData(string key, object contnet)
        {
            _contents[key] = contnet;
        }

        public static string DummyKey = "DUMMY_";
        public static void SetData<T>(T content)
        {
            var key = DummyKey + typeof(T).FullName;
            _contents[key] = content;
        }

        public static bool TryGetData<T>(out T result)
        {
            foreach (var value in _contents.Values)
            {
                if (value is T)
                {
                    result = (T)value;
                    return true;
                }
            }

            result = default;
            return false;
        }

        public static bool TryGetData<T>(string key, out T result)
        {
            if (_contents.TryGetValue(key, out var value) && value is T)
            {
                result = (T)value;
                return true;
            }

            result = default;
            return false;
        }

        public static void Clear(string key)
        {
            _contents.Remove(key);
        }

        private static List<string> _removeKeyBuffer = new List<string>();
        public static void Clear<T>()
        {
            _removeKeyBuffer.Clear();
            foreach (var key in _contents.Keys)
            {
                if (_contents[key] is T)
                {
                    _removeKeyBuffer.Add(key);
                }
            }
            foreach (var key in _removeKeyBuffer)
            {
                _contents.Remove(key);
            }
        }

        public static void ClearAll()
        {
            _contents.Clear();
        }
    }
}
