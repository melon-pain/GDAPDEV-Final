using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemySpawn : MonoBehaviour
{
    [Header("Spawn")]
    [SerializeField] private ObjectPool enemyPool;

    [Header("Paths")]
    [SerializeField] private List<CinemachinePath> paths = new List<CinemachinePath>();

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < 4; i++)
            Spawn(i, 10);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void Spawn(int i, int max)
    {
        Enemy enemy = enemyPool.GetObjectFromPool().GetComponent<Enemy>();
        enemy.SetPath(paths[i % paths.Count]);
        enemy.cart.m_Position = 0.0f;
        enemy.cartPos = Mathf.Clamp(((float)i + 1.0f) / max, 0.2f, 0.8f);
    }
}
