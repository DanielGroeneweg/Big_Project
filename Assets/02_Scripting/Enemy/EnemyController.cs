using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    private Enemy enemy;

    public Enemy Enemy => enemy;
    public EnemyData EnemyData => enemyData;
    public int CurrentHP => enemy.currentHP;
    public int MaxHP => enemyData.maxHP;
    public bool IsDead => CurrentHP <= 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = enemyData.CreateEnemy();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
