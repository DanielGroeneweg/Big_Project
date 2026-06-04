using UnityEngine;
using NaughtyAttributes;
public class SingleBurstSpawner : EnemySpawner
{
    [Button("Spawn", EButtonEnableMode.Playmode)]
    public override void Spawn()
    {
        for(int i = 1; i <= spawnAmount; i++)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-spawnRadius, spawnRadius);
            pos.z += Random.Range(-spawnRadius, spawnRadius);
            EnemyController enemy = Instantiate(enemyPrefab, pos, Quaternion.identity);
            enemies.Add(enemy);
        }
    }
}
