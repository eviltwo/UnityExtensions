using UnityEngine;

namespace eviltwo.UnityExtensions
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
}
