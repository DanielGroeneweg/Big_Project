using UnityEngine;
using NaughtyAttributes;
public class InventoryTester : MonoBehaviour
{
    [SerializeField] WeaponItem weapon;
    [Button("ChangeWeapon", EButtonEnableMode.Playmode)]
    void ChangeWeapon()
    {
        EquipWeaponEventData data = new EquipWeaponEventData() { weapon = weapon };
        EventBusManager.instance.EquipWeaponEvent.Raise(data);
    }
}