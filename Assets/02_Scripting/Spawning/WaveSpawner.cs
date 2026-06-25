using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using UnityEngine.Events;
public class WaveSpawner : EnemySpawner
{
    [Serializable]
    public class WaveData
    {
        public int enemyCount;
        public float spawnDelay;
    }
    [Serializable]
    class Wave
    {
        public WaveData[] waves = new WaveData[0];
    }

    [SerializeField] Wave[] waves = new Wave[0];
    [Tooltip("Checking this makes the next wave spawn after a period of time no matter if the previous wave has been defeated or not")]
    [SerializeField] bool useWaveTimer;
    [SerializeField] [ShowIf("useWaveTimer")] float waveTimer;
    [SerializeField] UnityEvent onFinalWaveDefeated;
    int waveCount;
    bool waveFinished;
    [Button("Spawn", EButtonEnableMode.Playmode)]
    public override void Spawn()
    {
        if (waveCount >= waves.Length)
        {
            onFinalWaveDefeated?.Invoke();
            return;
        }

        waveFinished = false;

        StartCoroutine(SpawnWave());
    }
    IEnumerator SpawnWave()
    {
        foreach (WaveData wave in waves[waveCount].waves)
        {
            yield return new WaitForSeconds(wave.spawnDelay);

            for (int i = 0; i < wave.enemyCount; i++)
            {
                EnemyController prefab = enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)];
                Vector3 pos = transform.position;
                pos.x += UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                pos.z += UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                EnemyController enemy = Instantiate(prefab, pos, Quaternion.identity);
                enemies.Add(enemy);
            }

            Invoke(nameof(AddEnemyEvents), 0.5f);
            if (useWaveTimer) StartCoroutine(WaveTimer());
            waveCount++;
            waveFinished = true;
        }
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

        if (waveFinished && enemies.Count == 0)
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
            foreach (WaveData data in wave.waves)
            {
                if (data.enemyCount <= 0) data.enemyCount = 1;
            }
        }
    }
}