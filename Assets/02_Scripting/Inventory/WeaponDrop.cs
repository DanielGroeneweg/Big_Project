using System.Collections;
using System.Collections.Generic;
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

    #region Track closest weapon
    static List<WeaponDrop> dropsInRange = new();
    static WeaponDrop closestDrop;
    static int lastUpdatedFrame = -1;
    static void RefreshClosest()
    {
        if (Time.frameCount == lastUpdatedFrame) return;
        lastUpdatedFrame = Time.frameCount;

        WeaponDrop closest = null;
        float minDist = float.MaxValue;
        foreach (var drop in dropsInRange)
        {
            float dist = Vector3.Distance(
                PlayerController.instance.transform.position,
                drop.transform.position
            );
            if (dist < minDist) { minDist = dist; closest = drop; }
        }
        closestDrop = closest;
    }
    void RemoveFromRange()
    {
        if (dropsInRange.Contains(this))
        {
            dropsInRange.Remove(this);
            slider.value = 0;
            holding = false;
            slider.gameObject.SetActive(false);
        }
    }
    private void OnDestroy()
    {
        dropsInRange.Remove(this);
    }
    #endregion
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
        RefreshClosest();

        float playerDist = Vector3.Distance(
            PlayerController.instance.transform.position,
            transform.position
        );

        if (playerDist <= proximityRange)
        {
            if (!dropsInRange.Contains(this))
                dropsInRange.Add(this);
        }
        else
        {
            RemoveFromRange();
        }

        // Only show slider on the closest drop
        bool isClosest = closestDrop == this;
        if (isClosest)
        {
            if (!slider.gameObject.activeSelf) slider.gameObject.SetActive(true);
        }
        else
        {
            if (slider.gameObject.activeSelf)
            {
                slider.value = 0;
                holding = false;
                slider.gameObject.SetActive(false);
            }
        }
    }
    public void OnE(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && closestDrop == this)
        {
            holding = true;
            StartCoroutine(Holding());
        }

        if (context.phase == InputActionPhase.Canceled)
        {
            holding = false;
            slider.value = 0;
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
        slider.value = 0;
    }
}