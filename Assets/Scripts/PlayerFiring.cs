using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    private float ticks = 0.1f;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        ticks += Time.deltaTime;
        if (Input.GetMouseButton(0) && ticks >= 0.1f)
        {
            Fire();
            ticks -= 0.1f;
        }
    }

    private void Fire()
    {
        Projectile projectile = pool.GetObjectFromPool().GetComponent<Projectile>();
        projectile.Activate(this.transform.position + this.transform.forward * 2.0f, this.transform.forward);
        Debug.Log("Fire");
    }
}
