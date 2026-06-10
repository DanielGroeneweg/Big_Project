using UnityEngine;
public class EventBusManager : MonoBehaviour
{
    public static EventBusManager instance;

    [SerializeField] EquipWeaponEvent equipWeaponEvent;
    [SerializeField] DropWeaponEvent dropWeaponEvent;
    public EquipWeaponEvent EquipWeaponEvent {  get { return equipWeaponEvent; } }
    public DropWeaponEvent DropWeaponEvent { get { return dropWeaponEvent; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}