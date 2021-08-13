using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private ObjectPool enemyPool;
    private int enemyCount = 0;
    private int enemyInWaveCount = 0;

    [Header("Paths")]
    [SerializeField] private List<CinemachinePath> paths = new List<CinemachinePath>();

    public UnityEvent OnWavesFinished;

    // Start is called before the first frame update
    private void Start()
    {
        enemyCount = enemyPool.GetPoolCount();

        SpawnWave(Random.Range(2, 8));
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void SpawnWave(int enemiesToSpawn)
    {
        if (enemiesToSpawn > enemyCount)
            enemiesToSpawn = enemyCount;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Spawn(i, enemiesToSpawn);
        }

        enemyInWaveCount = enemiesToSpawn;
    }

    private void Spawn(int i, int max)
    {
        Enemy enemy = enemyPool.GetObjectFromPool().GetComponent<Enemy>();
        enemy.SetPath(paths[i % paths.Count]);
        enemy.cart.m_Position = 0.0f;
        enemy.cartPos = Mathf.Clamp(((float)i + 1.0f) / max, 0.2f, 0.8f);

        enemy.OnDeath.AddListener(ReduceEnemyCount);
    }

    private void ReduceEnemyCount()
    {
        enemyCount--;
        enemyInWaveCount--;
        if (enemyCount <= 0)
        {
            OnWavesFinished.Invoke();
        }
        else if (enemyInWaveCount <= 0)
        {
            SpawnWave(Random.Range(2, 8));
        }
    }
}
