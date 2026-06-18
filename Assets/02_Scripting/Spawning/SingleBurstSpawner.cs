using UnityEngine;
using NaughtyAttributes;
public class SingleBurstSpawner : EnemySpawner
{
    [SerializeField] protected int spawnAmount;
    [Button("Spawn", EButtonEnableMode.Playmode)]
    public override void Spawn()
    {
        for(int i = 1; i <= spawnAmount; i++)
        {
            EnemyController prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Vector3 pos = transform.position;
            pos.x += Random.Range(-spawnRadius, spawnRadius);
            pos.z += Random.Range(-spawnRadius, spawnRadius);
            EnemyController enemy = Instantiate(prefab, pos, Quaternion.identity);
            enemies.Add(enemy);
        }
    }
}
