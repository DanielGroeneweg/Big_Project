using UnityEngine;

public class PlayerStatesData : StatesData
{
    [Header("References")]
    public Transform playerTransform;

    public WeaponType CurrentWeapon { get; private set; } = WeaponType.None;

    private void Start()
    {
        playerTransform = transform;
    }

    private void OnEnable()
    {
        EventBusManager.instance.EquipWeaponEvent.Register(OnWeaponEquipped);
        EventBusManager.instance.DropWeaponEvent.Register(OnWeaponDropped);
    }

    private void OnDisable()
    {
        EventBusManager.instance.EquipWeaponEvent.Unregister(OnWeaponEquipped);
        EventBusManager.instance.DropWeaponEvent.Unregister(OnWeaponDropped);
    }

    private void OnWeaponEquipped(EquipWeaponEventData data)
    {
        //if(data.weapon.ItemName=="")
    }

    private void OnWeaponDropped(DropWeaponEventData data)
    {
        //if (CurrentWeapon == data.weaponType)
            CurrentWeapon = WeaponType.None;
    }
}
