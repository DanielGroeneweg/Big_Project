using UnityEngine;
using NaughtyAttributes;
public class WeaponDropTester : MonoBehaviour
{
    [SerializeField] bool simulateEnemyDeath;
    [SerializeField] WeaponItem weapon;
    [Button("DropWeapon", EButtonEnableMode.Playmode)]
    void Drop()
    {
        DropWeaponEventData data = new DropWeaponEventData() { weapon = weapon, position = transform.position, droppedByEnemy = simulateEnemyDeath };
        EventBusManager.instance.DropWeaponEvent.Raise(data);
    }
}