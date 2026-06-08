using UnityEngine;
public class EventBusManager : MonoBehaviour
{
    public static EventBusManager instance;

    [SerializeField] HitPlayerEvent hitPlayerEvent;
    [SerializeField] EquipWeaponEvent equipWeaponEvent;
    public HitPlayerEvent HitPlayerEvent {  get { return hitPlayerEvent; } }
    public EquipWeaponEvent EquipWeaponEvent {  get { return equipWeaponEvent; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}