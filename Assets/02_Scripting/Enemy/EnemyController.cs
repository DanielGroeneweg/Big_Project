using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] Presenter[] hpPresenters = new Presenter[0];
    [SerializeField] private EnemyData enemyData;
    private Enemy enemy;

    public Enemy Enemy => enemy;
    public EnemyData EnemyData => enemyData;
    public float CurrentHP => enemy.currentHP;
    public int MaxHP => enemyData.maxHP;
    public bool IsDead => CurrentHP <= 0;

    public Health health { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = enemyData.CreateEnemy();

        health = GetComponent<Health>();
        if (health != null)
        {
            health.healthChangeEvent += ChangeHealth;
            health.deathEvent += EnemyDeath;
        }
    }
    private void OnDestroy()
    {
        if (health != null)
            health.healthChangeEvent -= ChangeHealth;
    }
    void ChangeHealth(HealthChangeData data)
    {
        foreach (var hp in hpPresenters)
        hp.Present(data.minHealth, data.maxHealth, data.currentHealth);
        enemy.currentHP = data.currentHealth;
    }
    void EnemyDeath()
    {
        if (enemyData.weapon == null) return;
        DropWeaponEventData data = new DropWeaponEventData() { weapon = EnemyData.weapon, position = transform.position, droppedByEnemy = true, durability = enemyData.weapon.StartDurability };
        EventBusManager.instance.DropWeaponEvent.Raise(data);

        Destroy(gameObject);    
    }
}