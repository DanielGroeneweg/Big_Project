using UnityEngine;

public class PlayerStatesData : StatesData
{
    [Header("References")]
    public Transform playerTransform;
    public GameObject sicklePrefabInstance;
    public GameObject clubPrefabInstance;
    public GameObject spearPrefabInstance;
    public Transform pickUpHand;
    public bool isPickingUpGnome;
    public bool isThrowingGnome;
    public WeaponType CurrentWeapon { get; private set; } = WeaponType.None;
    public bool playerAttacked;
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

    private WeaponType GetWeaponType(WeaponItem weapon)
    {
        if (weapon == null)
        {
            Debug.Log("null weapon, probably because of destroyed weapon");
            return WeaponType.None;
        }

        switch (weapon.ItemName)
        {
            case "Sickle": return WeaponType.Sickle;
            case "Spear": return WeaponType.Spear;
            case "Club": return WeaponType.Club;
            default:
                return WeaponType.None;
        }
    }

    private void OnWeaponEquipped(EquipWeaponEventData data)
    {
        CurrentWeapon = GetWeaponType(data.weapon);
    }

    private void OnWeaponDropped(DropWeaponEventData data)
    {
        WeaponType droppedType = GetWeaponType(data.weapon);
        if (CurrentWeapon == droppedType)
            CurrentWeapon = WeaponType.None;
    }
    public void ActivateWeaponVisual(WeaponType type)
    {
        sicklePrefabInstance.SetActive(type == WeaponType.Sickle);
        clubPrefabInstance.SetActive(type == WeaponType.Club);
        spearPrefabInstance.SetActive(type == WeaponType.Spear);
    }
}
