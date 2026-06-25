using UnityEngine;
using UnityEngine.InputSystem;
public class ObjectStateChanger : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] MouseLocker mouseLocker;
    [SerializeField] PlayerInput playerInput;
    public void OnSettings()
    {
        target.SetActive(!target.activeSelf);
        if (playerInput != null)
            playerInput.enabled = !target.activeSelf;

        if (target.activeSelf)
            mouseLocker.UnlockMouse();
        
        else
            mouseLocker.LockMouse();
    }
}