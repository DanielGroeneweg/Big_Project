using UnityEngine;
using UnityEngine.Events;
public class TriggerEventInvoker : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerStay;
    [SerializeField] UnityEvent onTriggerExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        onTriggerEnter?.Invoke();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") return;
        onTriggerStay?.Invoke();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        onTriggerExit?.Invoke();
    }
}