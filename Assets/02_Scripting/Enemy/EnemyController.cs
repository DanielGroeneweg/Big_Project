using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Presenter presenter;
    [SerializeField] private EnemyData enemyData;
    private Enemy enemy;

    public Enemy Enemy => enemy;
    public EnemyData EnemyData => enemyData;
    public float CurrentHP => enemy.currentHP;
    public int MaxHP => enemyData.maxHP;
    public bool IsDead => CurrentHP <= 0;

    Health health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = enemyData.CreateEnemy();

        health = GetComponent<Health>();
        if (health != null)
            health.healthChangeEvent += ChangeHealth;
    }
    private void OnDestroy()
    {
        if (health != null)
            health.healthChangeEvent -= ChangeHealth;
    }
    void ChangeHealth(HealthChangeData data)
    {
        presenter.Present(data.minHealth, data.maxHealth, data.currentHealth);
        enemy.currentHP = data.currentHealth;
    }
}