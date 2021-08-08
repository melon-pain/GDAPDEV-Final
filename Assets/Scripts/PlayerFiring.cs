using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    private float ticks = 0.1f;
    private const float distance = 3.0f;
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
            ticks = 0.0f;
        }
    }

    private void Fire()
    {
        Projectile projectile = pool.GetObjectFromPool().GetComponent<Projectile>();
        Debug.Log("Player: " + this.transform.forward);
        projectile.Activate(this.transform.position + (this.transform.forward * distance), this.transform.forward);
    }
}
