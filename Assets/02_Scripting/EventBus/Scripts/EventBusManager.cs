using UnityEngine;
public class EventBusManager : MonoBehaviour
{
    [SerializeField] HitPlayerEvent hitPlayerEvent;
    public HitPlayerEvent HitPlayerEvent {  get { return hitPlayerEvent; } }
}