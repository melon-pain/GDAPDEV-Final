using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RegularTypeOne : MonoBehaviour
{
    EnergySystem energy;
    private float energyMax = 100f;
    private Bar energyBar;

    [SerializeField] private Element element;
    [SerializeField] private GameObject plate;
    [SerializeField] private List<Material> materials = new List<Material>();

    public UnityEvent OnDeath;

    private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        element = (Element)Random.Range(0, 4);
        energy = new EnergySystem(energyMax);
        energyBar = this.transform.gameObject.GetComponentInChildren<Bar>();
        rotationSpeed = Random.Range(50f, 100f);
        plate.GetComponent<MeshRenderer>().material = materials[(int)element];
    }

    private void OnValidate()
    {
        plate.GetComponent<MeshRenderer>().material = materials[(int)element];
    }

    // Update is called once per frame
    void Update()
    {
        plate.transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);
        if(Input.GetKeyDown(KeyCode.R))
        {
            DamageTaken(20f);
        }
        energyBar.UpdateBar(energy.GetEnergyPercent());
    }

    private void OnDestroy()
    {
        //Should play animation
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<Projectile>().GetElement() == Elements.GetWeakness(this.element))
            DamageTaken(20f);
    }

    private void DamageTaken(float amount)
    {
        energy.Damage(amount);

        if(energy.GetEnergy() <= 0)
        {
            //Die
            OnDeath.Invoke();
            Destroy(this.gameObject);
        }
    }
}
