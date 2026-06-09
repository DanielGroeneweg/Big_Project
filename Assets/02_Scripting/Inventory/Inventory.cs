using System.Collections;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    WeaponItem weapon;
    [SerializeField]
    WeaponItem defaultWeapon;
    public WeaponItem Weapon => weapon;
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        EventBusManager.instance.EquipWeaponEvent.Register(SetWeapon);

    }
    private void OnDestroy()
    {
        EventBusManager.instance.EquipWeaponEvent.Unregister(SetWeapon);
    }
    void SetWeapon(EquipWeaponEventData data)
    {
        weapon = data.weapon;

        if (data.weapon == null)
        {
            weapon = defaultWeapon;

            EquipWeaponEventData newData = new EquipWeaponEventData() { weapon = defaultWeapon };
            StartCoroutine(EventBusManager.instance.EquipWeaponEvent.Raise(newData, 0.1f));
        }
    }
}