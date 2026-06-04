using UnityEngine;
public class MouseLocker : MonoBehaviour
{
    void Start()
    {
        LockMouse();
    }
    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}