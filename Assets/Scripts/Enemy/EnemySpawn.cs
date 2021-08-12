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
        for (int i = 0; i < 10; i++)
            Invoke("Spawn", 1);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void Spawn()
    {
        Enemy enemy = enemyPool.GetObjectFromPool().GetComponent<Enemy>();
        enemy.SetPath(paths[Random.Range(0, 2)]);
        enemy.cart.m_Position = 0.0f;
    }
}
