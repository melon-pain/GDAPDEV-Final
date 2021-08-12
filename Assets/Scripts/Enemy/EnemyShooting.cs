using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private EnemyProjectile enemyProjectile;

    private bool isShooting = false;
    private float shootTime = 0.0f;
    private float shootInterval = 0.0f;
    private float maxShootTime = 2.0f;

    private GameObject player;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        shootInterval = Random.Range(1.0f, 2.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isShooting)
        {
            shootTime -= Time.deltaTime * shootInterval ;
            if (shootTime <= 0.0f)
            {
                isShooting = false;
                enemyProjectile.particles.Stop();
                shootInterval = Random.Range(1.0f, 2.0f);
            }
        }
        else
        {
            shootTime += Time.deltaTime * shootInterval;

            if (shootTime >= maxShootTime)
            {
                isShooting = true;
                enemyProjectile.particles.Play();
                shootInterval = Random.Range(1.0f, 2.0f);
            }

            else if (shootTime < maxShootTime * 0.5f)
            {
                this.transform.LookAt(player.transform);
            }
        }
    }

    
}
