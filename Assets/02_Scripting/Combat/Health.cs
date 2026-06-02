using System;
using UnityEngine;
/// <summary>
/// A class that keeps track of an object's health
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    
    float health;

    // Event that is fired to notify health presenters
    public event Action<HealthChangeData> healthChangeEvent;

    // Event that is fired to handle this object dying
    public event Action deathEvent;
    public void Damage(int hp)
    {
        health = Mathf.Max(0, health - Mathf.Abs(hp));
        healthChangeEvent.Invoke(new HealthChangeData() { currentHealth = health, minHealth = 0, maxHealth = maxHealth});

        if (health <= 0) deathEvent.Invoke();
    }
    public void Heal(int hp)
    {
        health = Mathf.Min(maxHealth, health + Mathf.Abs(hp));
        healthChangeEvent.Invoke(new HealthChangeData() { currentHealth = health, minHealth = 0, maxHealth = maxHealth });
    }
}