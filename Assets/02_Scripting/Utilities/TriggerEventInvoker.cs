using UnityEngine;
using UnityEngine.Events;
public class TriggerEventInvoker : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerStay;
    [SerializeField] UnityEvent onTriggerExit;
    private void OnTriggerEnter(Collider other)
    {
        onTriggerEnter?.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        onTriggerStay?.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        onTriggerExit?.Invoke();
    }
}