using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    private float ticks = 0.0f;
    private float fireRate = 5.0f;
    private const float distance = 3.0f;
    // Start is called before the first frame update
    private void Start()
    {
        ticks = 1.0f / fireRate;
    }

    // Update is called once per frame
    private void Update()
    {
        ticks += Time.deltaTime;
    }

    public void Fire()
    {
        if (ticks < 1.0f / fireRate)
            return;
        Projectile projectile = pool.GetObjectFromPool().GetComponent<Projectile>();
        Debug.Log("Player: " + this.transform.forward);
        projectile.Activate(this.transform.position + (this.transform.forward * distance), this.transform.forward);
        ticks = 0.0f;
    }
}
