using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class WeaponDrop : MonoBehaviour
{
    [SerializeField] PlayerInput input;
    [SerializeField] Slider slider;
    [SerializeField] float proximityRange;
    [SerializeField] float holdingTime;
    bool holding = false;
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
    private void Update()
    {
        float playerDist = Vector3.Distance(PlayerController.instance.transform.position, transform.position);

        if (playerDist <= proximityRange)
        {
            if (!slider.gameObject.activeSelf) slider.gameObject.SetActive(true);
        }
        else if (slider.gameObject.activeSelf)
        {
            slider.value = 0;
            holding = false;
            slider.gameObject.SetActive(false);
        }
    }
    public void OnE(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            Debug.Log("starting");
            holding = true;
            StartCoroutine(Holding());
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            Debug.Log("stopping");
            holding = false;
        }
    }
    IEnumerator Holding()
    {
        float timePassed = 0;
        while (holding)
        {
            yield return null;
            timePassed += Time.deltaTime;
            slider.value = timePassed / holdingTime;

            if (timePassed >= holdingTime)
            {
                EquipWeaponEventData data = new EquipWeaponEventData() { weapon = weapon };
                EventBusManager.instance.EquipWeaponEvent.Raise(data);
                Destroy(gameObject);
            }
        }
    }
}