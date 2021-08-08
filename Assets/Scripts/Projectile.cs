using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100.0f;
    [SerializeField] private Element element = Element.Electric;
    [SerializeField] private List<Material> materials = new List<Material>();

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Update()
    {
        this.transform.localPosition += (this.transform.forward * speed * Time.deltaTime);
    }

    public void Activate(Element newElement, Vector3 position, Vector3 forward)
    {
        StopAllCoroutines();
        this.element = newElement;
        this.transform.position = position;
        this.transform.forward = forward;
        this.OnValidate();
        StartCoroutine(Deactivate());
    }
    
    private IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(4.0f);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StopAllCoroutines();
        this.gameObject.SetActive(false);
        Debug.Log("Projectile collided!");
    }

    private void OnValidate()
    {
        this.GetComponent<MeshRenderer>().material = materials[(int)element];
    }
}
