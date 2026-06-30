using UnityEngine;
public class EventBusManager : MonoBehaviour
{
    public static EventBusManager instance;

    [SerializeField] EquipWeaponEvent equipWeaponEvent;
    [SerializeField] DropWeaponEvent dropWeaponEvent;
    [SerializeField] WeaponDurabilityEvent weaponDurabilityEvent;
    [SerializeField] PlayerAttackEvent playerAttackEvent;
    public EquipWeaponEvent EquipWeaponEvent {  get { return equipWeaponEvent; } }
    public DropWeaponEvent DropWeaponEvent { get { return dropWeaponEvent; } }
    public WeaponDurabilityEvent WeaponDurabilityEvent { get { return  weaponDurabilityEvent; } }
    public PlayerAttackEvent PlayerAttackEvent { get { return playerAttackEvent; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}