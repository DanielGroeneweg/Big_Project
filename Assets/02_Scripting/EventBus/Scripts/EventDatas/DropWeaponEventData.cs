using UnityEngine;
public class DropWeaponEventData : GameEventData
{
    public WeaponItem weapon;
    public Vector3 position;
    public bool droppedByEnemy;
}