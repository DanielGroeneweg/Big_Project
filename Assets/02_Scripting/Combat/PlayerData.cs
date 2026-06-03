using UnityEngine;
[RequireComponent(typeof(Health))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] Presenter healthPresenter;
    Health health;
    private void Start()
    {
        health = GetComponent<Health>();
        if (health != null) health.healthChangeEvent += ChangeHealth;
    }
    private void OnDestroy()
    {
        if (health != null) health.healthChangeEvent -= ChangeHealth;
    }
    public void ChangeHealth(HealthChangeData data)
    {
        healthPresenter.Present(data.minHealth, data.maxHealth, data.currentHealth);
    }
}