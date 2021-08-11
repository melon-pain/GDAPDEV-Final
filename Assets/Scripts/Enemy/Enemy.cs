using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isShooting = false;
    private float shootTime = 0.0f;
    private float maxShootTime = 2.0f;

    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isShooting)
        {
            shootTime -= Time.deltaTime;
            if (shootTime <= 0.0f)
            {
                isShooting = false;
                animator.SetBool("Is Shooting", false);
            }
        }
        else
        {
            shootTime += Time.deltaTime;

            if (shootTime >= maxShootTime)
            {
                isShooting = true;
                animator.SetBool("Is Shooting", true);
            }
        }
    }
}
