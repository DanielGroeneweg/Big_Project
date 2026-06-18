using NaughtyAttributes;
using UnityEngine;

public class RepeatingSpawner : EnemySpawner
{
    [SerializeField] protected int spawnAmount;
    [SerializeField] float spawnInterval;
    [SerializeField] bool hasMaxCycleAmount;
    [SerializeField][ShowIf("hasMaxCycleAmount")] int maxCycleAmount;
    int cycles = 0;
    [Button("Spawn", EButtonEnableMode.Playmode)]
    public override void Spawn()
    {
        InvokeRepeating(nameof(SpawnEnemies), 0, spawnInterval);
    }
    void SpawnEnemies()
    {
        if (hasMaxCycleAmount && cycles > maxCycleAmount) return;

        for (int i = 1; i <= spawnAmount; i++)
        {
            EnemyController prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector3 pos = transform.position;
            pos.x += Random.Range(-spawnRadius, spawnRadius);
            pos.z += Random.Range(-spawnRadius, spawnRadius);
            EnemyController enemy = Instantiate(prefab, pos, Quaternion.identity);
            enemies.Add(enemy);
        }

        if (hasMaxCycleAmount) cycles++;
    }
}
