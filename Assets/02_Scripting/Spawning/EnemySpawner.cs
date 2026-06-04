using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
public abstract class EnemySpawner : MonoBehaviour
{
    [SerializeField] protected EnemyController enemyPrefab;
    [SerializeField] protected int spawnAmount;
    [SerializeField] protected float spawnRadius;
    protected List<EnemyController> enemies = new();
    public abstract void Spawn();
    [Button("Remove Enemies", EButtonEnableMode.Playmode)]
    public void Remove()
    {
        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Destroy(enemies[i].gameObject);
        }
        enemies.Clear();
    }
}