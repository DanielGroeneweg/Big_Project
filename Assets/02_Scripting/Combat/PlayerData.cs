using UnityEngine;
[DefaultExecutionOrder(100)]
[RequireComponent(typeof(Health))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] Presenter damagePresenter;
    [SerializeField] Presenter[] healthPresenters;
    [SerializeField] Presenter[] staminaPresenters;
    [SerializeField] Presenter durabilityPresenter;
    Health health;
    Stamina stamina;
    private void Start()
    {
        health = GetComponent<Health>();
        stamina = GetComponent<Stamina>();
        if (health != null)
        {
            health.healthChangeEvent += ChangeHealth;
            health.damageEvent += Damage;
        }
        if (stamina != null) stamina.staminaChangeEvent += ChangeStamina;
        EventBusManager.instance.WeaponDurabilityEvent.Register(ChangeDurability);
    }
    private void OnDestroy()
    {
        if (health != null)
        {
            health.healthChangeEvent -= ChangeHealth;
            health.damageEvent -= Damage;
        }
        if (stamina != null) stamina.staminaChangeEvent -= ChangeStamina;
        EventBusManager.instance.WeaponDurabilityEvent.Unregister(ChangeDurability);
    }
    void ChangeHealth(HealthChangeData data)
    {
        foreach (Presenter healthPresenter in healthPresenters)
            healthPresenter.Present(data.minHealth, data.maxHealth, data.currentHealth);
    }
    void ChangeStamina(StaminaChangeData data)
    {
        foreach (Presenter staminaPresenter in staminaPresenters)
            staminaPresenter.Present(0, data.maxStamina, data.currentStamina);
    }
    void ChangeDurability(WeaponDurabilityEventData data)
    {
        durabilityPresenter.Present(0, data.maxDurability, data.durability);
    }
    void Damage()
    {
        damagePresenter.Present(0, 0, 0);
    }
}