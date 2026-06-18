using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using UnityEngine.Events;
public class WaveSpawner : EnemySpawner
{
    [Serializable]
    class Wave { public float enemieCount; }
    [SerializeField] Wave[] waves = new Wave[0];
    [Tooltip("Checking this makes the next wave spawn after a period of time no matter if the previous wave has been defeated or not")]
    [SerializeField] bool useWaveTimer;
    [SerializeField] [ShowIf("useWaveTimer")] float waveTimer;
    [SerializeField] UnityEvent onFinalWaveDefeated;
    int waveCount;
    [Button("Spawn", EButtonEnableMode.Playmode)]
    public override void Spawn()
    {
        if (waveCount >= waves.Length)
        {
            onFinalWaveDefeated?.Invoke();
            return;
        }

        for (int i = 0; i < waves[waveCount].enemieCount; i++)
        {
            EnemyController prefab = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
            Vector3 pos = transform.position;
            pos.x += UnityEngine.Random.Range(-spawnRadius, spawnRadius);
            pos.z += UnityEngine.Random.Range(-spawnRadius, spawnRadius);
            EnemyController enemy = Instantiate(prefab, pos, Quaternion.identity);
            enemies.Add(enemy);
        }

        Invoke(nameof(AddEnemyEvents), 0.2f);
        if (useWaveTimer) StartCoroutine(WaveTimer());
        waveCount++;
    }
    void AddEnemyEvents()
    {
        foreach (EnemyController enemy in enemies)
        {
            enemy.health.deathEvent += RemoveEnemy;
        }
    }
    private void OnDestroy()
    {
        foreach (var enemy in enemies)
        {
            enemy.health.deathEvent -= RemoveEnemy;
        }
    }
    void RemoveEnemy()
    {
        for(int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
                continue;
            }

            if (enemies[i].health.died)
                enemies.RemoveAt(i);
        }

        if (enemies.Count == 0)
        {
            if (useWaveTimer) StopAllCoroutines();
            Spawn();
        }
    }
    IEnumerator WaveTimer()
    {
        yield return new WaitForSeconds(waveTimer);
        Spawn();
    }
    private void OnValidate()
    {
        foreach (Wave wave in waves)
        {
            if (wave.enemieCount <= 0) wave.enemieCount = 1;
        }
    }
}