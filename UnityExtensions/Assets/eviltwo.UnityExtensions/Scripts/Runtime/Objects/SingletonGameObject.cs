using System.Collections.Generic;
using UnityEngine;

namespace eviltwo.UnityExtensions
{
    public class SingletonGameObject : MonoBehaviour
    {
        [SerializeField]
        private string _key = "UniqueKey";

        private static Dictionary<string, GameObject> _instances = new Dictionary<string, GameObject>();

        private void Awake()
        {
            if (_instances.TryGetValue(_key, out var instance) && instance != null)
            {
                // The object will not be destroyed in the current frame, so it will be deactivated.
                gameObject.SetActive(false);
                Destroy(gameObject);
                return;
            }

            _instances.Add(_key, gameObject);
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
}
