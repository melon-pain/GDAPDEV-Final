using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyElementType
{
    Fire,
    Water,
    Electric,
    Ice
}

public class RegularTypeOne : MonoBehaviour
{
    EnergySystem energy;
    private float energyMax = 100f;
    private Bar energyBar;

    [SerializeField] private EnemyElementType elementType;
    [SerializeField] private GameObject model;
    private float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        energy = new EnergySystem(energyMax);
        energyBar = this.transform.gameObject.GetComponentInChildren<Bar>();
        rotationSpeed = Random.Range(50f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        model.transform.Rotate(new Vector3(0f, rotationSpeed, 0f) * Time.deltaTime);
    }

    private void OnDestroy()
    {
        //Should play animation
    }

    private void OnTriggerEnter(Collider other)
    {
        //Place here the specific type of element projectile
        DamageTaken(20f);
    }

    private void DamageTaken(float amount)
    {
        energy.Damage(amount);
        energyBar.UpdateBar(energy.GetEnergyPercent());
        if(energy.GetEnergy() < 0)
        {
            //Die
            Destroy(this.gameObject);
        }
    }
}
