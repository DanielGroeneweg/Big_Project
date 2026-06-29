using UnityEngine;
using UnityEngine.Events;
public class AwakeEventInvoker : MonoBehaviour
{
    [SerializeField] UnityEvent onAwake;
    private void Awake()
    {
        onAwake?.Invoke();
    }
}