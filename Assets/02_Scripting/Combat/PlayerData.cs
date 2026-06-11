using UnityEngine;
[RequireComponent(typeof(Health))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] Presenter healthPresenter;
    [SerializeField] Presenter staminaPresenter;
    Health health;
    Stamina stamina;
    private void Start()
    {
        health = GetComponent<Health>();
        stamina = GetComponent<Stamina>();
        if (health != null) health.healthChangeEvent += ChangeHealth;
        if (stamina != null) stamina.staminaChangeEvent += ChangeStamina;
    }
    private void OnDestroy()
    {
        if (health != null) health.healthChangeEvent -= ChangeHealth;
        if (stamina != null) stamina.staminaChangeEvent -= ChangeStamina;
    }
    void ChangeHealth(HealthChangeData data)
    {
        healthPresenter.Present(data.minHealth, data.maxHealth, data.currentHealth);
    }
    void ChangeStamina(StaminaChangeData data)
    {
        staminaPresenter.Present(0, data.maxStamina, data.currentStamina);
    }
}