using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100.0f;
    [SerializeField] private float damage = 20.0f;
    [SerializeField] private Element element = Element.Electric;
    [SerializeField] private List<Material> materials = new List<Material>();

    private Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Update()
    {
        //this.transform.localPosition += (direction * speed * Time.deltaTime);
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
    }

    public void Activate(Element newElement, Vector3 position, Vector3 dir)
    {
        StopAllCoroutines();
        this.element = newElement;
        this.transform.position = position;
        this.transform.forward = dir;
        this.OnValidate();
        StartCoroutine(Deactivate());
    }
    
    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(3.0f);
        this.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<VisualEffect>().SendEvent("OnHit");
        this.GetComponent<MeshRenderer>().enabled = false;

        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(this.element, this.damage);
        }

        StopAllCoroutines();
        StartCoroutine(Deactivate());
    }

    private void OnValidate()
    {
        this.GetComponent<MeshRenderer>().material = materials[(int)element];
    }

    public Element GetElement()
    {
        return element;
    }

    public float GetDamage()
    {
        return damage;
    }
}
