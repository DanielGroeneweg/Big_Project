using UnityEngine;
[DefaultExecutionOrder(100)]
[RequireComponent(typeof(Health))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] Presenter healthPresenter;
    [SerializeField] Presenter staminaPresenter;
    [SerializeField] Presenter durabilityPresenter;
    Health health;
    Stamina stamina;
    private void Start()
    {
        health = GetComponent<Health>();
        stamina = GetComponent<Stamina>();
        if (health != null) health.healthChangeEvent += ChangeHealth;
        if (stamina != null) stamina.staminaChangeEvent += ChangeStamina;
        EventBusManager.instance.WeaponDurabilityEvent.Register(ChangeDurability);
    }
    private void OnDestroy()
    {
        if (health != null) health.healthChangeEvent -= ChangeHealth;
        if (stamina != null) stamina.staminaChangeEvent -= ChangeStamina;
        EventBusManager.instance.WeaponDurabilityEvent.Unregister(ChangeDurability);
    }
    void ChangeHealth(HealthChangeData data)
    {
        healthPresenter.Present(data.minHealth, data.maxHealth, data.currentHealth);
    }
    void ChangeStamina(StaminaChangeData data)
    {
        staminaPresenter.Present(0, data.maxStamina, data.currentStamina);
    }
    void ChangeDurability(WeaponDurabilityEventData data)
    {
        durabilityPresenter.Present(0, data.maxDurability, data.durability);
    }
}