using UnityEngine;
using NaughtyAttributes;
public class WeaponDropTester : MonoBehaviour
{
    [SerializeField] WeaponItem weapon;
    [Button("DropWeapon", EButtonEnableMode.Playmode)]
    void Drop()
    {
        DropWeaponEventData data = new DropWeaponEventData() { weapon = weapon, position = transform.position };
        EventBusManager.instance.DropWeaponEvent.Raise(data);
    }
}