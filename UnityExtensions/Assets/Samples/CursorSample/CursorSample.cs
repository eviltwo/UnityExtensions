using eviltwo.UnityExtensions;
using UnityEngine;

public class CursorSample : MonoBehaviour
{
    private void Update()
    {
        var shouldActiveCursor = CursorRequestManager.Instance.ShouldActive();
        Cursor.visible = shouldActiveCursor;
        Cursor.lockState = shouldActiveCursor ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
