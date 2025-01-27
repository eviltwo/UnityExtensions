using UnityEngine;

namespace eviltwo.UnityExtensions.RuntimeCheck
{
    public class CallCountChecker : MonoBehaviour
    {
        private int _callCount;

        public void Call()
        {
            Debug.Log($"CallCountChecker called {++_callCount} times.");
        }
    }
}
