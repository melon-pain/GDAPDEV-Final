using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void Activate(Vector3 position, Vector3 direction)
    {
        StopAllCoroutines();
        this.transform.position = position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(direction * speed, ForceMode.Impulse);

        StartCoroutine(Deactivate());
    }
    
    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(5.0f);
        this.gameObject.SetActive(false);
    }
}
