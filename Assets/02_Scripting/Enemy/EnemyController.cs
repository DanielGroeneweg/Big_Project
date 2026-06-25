using System.Collections;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    [SerializeField] Presenter[] hpPresenters = new Presenter[0];
    [SerializeField] Presenter[] deathPresenters = new Presenter[0];
    [SerializeField] AudioClip[] heys = new AudioClip[0];
    [SerializeField] float heyInterval;
    [SerializeField] float heyChance;
    [SerializeField] private EnemyData enemyData;
    private Enemy enemy;
    public Enemy Enemy => enemy;
    public EnemyData EnemyData => enemyData;
    public float CurrentHP => enemy.currentHP;
    public int MaxHP => enemyData.maxHP;
    public bool IsDead => CurrentHP <= 0;
    bool died = false;
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

        StartCoroutine(RepeatedHey());
    }
    IEnumerator RepeatedHey()
    {
        while (CurrentHP > 0)
        {
            if (Random.Range(0f, 1f) <= heyChance)
            {
                AudioClip clip = heys[Random.Range(0, heys.Length)];
                SoundManager.instance.PlaySound(clip, transform.position, true, Random.Range(0.9f, 1.1f));
            }

            yield return new WaitForSeconds(heyInterval);
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
        if (enemyData.weapon == null || died) return;

        DropWeaponEventData data = new DropWeaponEventData() { weapon = EnemyData.weapon, position = transform.position, droppedByEnemy = true, durability = enemyData.weapon.StartDurability };
        EventBusManager.instance.DropWeaponEvent.Raise(data);
        died = true;

        Destroy(gameObject,2f);    
    }
}