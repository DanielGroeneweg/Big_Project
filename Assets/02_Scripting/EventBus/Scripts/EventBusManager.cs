using UnityEngine;
public class EventBusManager : MonoBehaviour
{
    public static EventBusManager instance;

    [SerializeField] HitPlayerEvent hitPlayerEvent;
    public HitPlayerEvent HitPlayerEvent {  get { return hitPlayerEvent; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}