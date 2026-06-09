using UnityEngine;
public class WeaponDrop : MonoBehaviour
{
    WeaponItem weapon;
    public void SpawnWeapon(WeaponItem weapon)
    {
        Weapon drop = Instantiate(weapon.WeaponPrefab, transform);
        drop.transform.localPosition = Vector3.zero;
        transform.localEulerAngles = new Vector3(90, 0, 0);
        drop.gameObject.layer = LayerMask.NameToLayer("WeaponDrop");
        gameObject.layer = LayerMask.NameToLayer("WeaponDrop");
        this.weapon = weapon;
    }
    public void PickUpWeapon()
    {
        EquipWeaponEventData data = new EquipWeaponEventData() { weapon = weapon };
        EventBusManager.instance.EquipWeaponEvent.Raise(data);
        Destroy(gameObject);
    }
}