using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Projectile : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private Element element = Element.Electric;
    [SerializeField] private float speed = 100.0f;
    [SerializeField] private float damage = 20.0f;

    [Header("Materials")]
    [SerializeField] private List<Material> materials = new List<Material>();

    [Header("VFX")]
    [SerializeField] private VisualEffect visualEffect;
    [ColorUsageAttribute(true, true)] [SerializeField] private List<Color> particleColors = new List<Color>(4);
    

    private bool isHit = false;

    // Start is called before the first frame update
    private void Start()
    {
    }

    private void Update()
    {
        if (!isHit)
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
        yield return new WaitForSeconds(2.0f);
        this.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.SetActive(false);
        isHit = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<VisualEffect>().SendEvent("OnHit");
        this.GetComponent<MeshRenderer>().enabled = false;
        isHit = true;

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
        this.visualEffect.SetVector4("Color", particleColors[(int)element]);
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
