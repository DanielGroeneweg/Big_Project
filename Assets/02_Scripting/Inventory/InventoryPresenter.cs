using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class InventoryPresenter : MonoBehaviour
{
    [SerializeField] Image iconImage;
    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        EventBusManager.instance.EquipWeaponEvent.Register(SetItem);
    }
    private void OnDestroy()
    {
        EventBusManager.instance.EquipWeaponEvent.Unregister(SetItem);
    }
    public void SetItem(EquipWeaponEventData data)
    {
        if (data.weapon == null)
        {
            iconImage.gameObject.SetActive(false);
        }

        else
        {
            iconImage.gameObject.SetActive(true);
            iconImage.sprite = data.weapon.Icon;
        }
    }
}