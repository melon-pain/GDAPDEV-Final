using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private Element element;
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private List<Color> startColors;
    [SerializeField] private List<Color> endColors;
    
    [SerializeField] private Transform beamStart;
    [SerializeField] private Transform beamEnd;
    private LineRenderer lineRend;
    [SerializeField] private float damage = 1.0f;

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    private void Start()
    {
        lineRend = this.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Physics.Linecast(beamStart.position, beamEnd.position, out hit))
        {
            Debug.Log($"Line hit: {hit.collider.gameObject.name}");
            EnemyHit(hit.collider);
        }

        Debug.DrawLine(beamStart.position, beamEnd.position, Color.green);
        lineRend.SetPosition(0, beamStart.localPosition);
        lineRend.SetPosition(1, beamEnd.localPosition);
    }

    private void OnValidate()
    {
        this.gameObject.GetComponent<LineRenderer>().material = materials[(int)element];
        if(startColors.Count >= 4 && endColors.Count >= 4)
        {
            this.gameObject.GetComponent<LineRenderer>().startColor = startColors[(int)element];
            this.gameObject.GetComponent<LineRenderer>().endColor = endColors[(int)element];
        }
    }

    public void SetBeamElement(Element newElement)
    {
        element = newElement;
        this.gameObject.GetComponent<LineRenderer>().material = materials[(int)element];
        if (startColors.Count >= 4 && endColors.Count >= 4)
        {
            this.gameObject.GetComponent<LineRenderer>().startColor = startColors[(int)element];
            this.gameObject.GetComponent<LineRenderer>().endColor = endColors[(int)element];
        }
    }

    public void SetBeamEndPosition(Vector3 pos)
    {
        beamEnd.position = pos;
    }
    
    private void EnemyHit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(this.element, this.damage);
        }
        else if (collision.gameObject.tag == "Boss Plate")
        {
            BossPlate plate = collision.gameObject.GetComponent<BossPlate>();
            plate.TakeDamage(this.element, this.damage);
        }
    }
    
}
