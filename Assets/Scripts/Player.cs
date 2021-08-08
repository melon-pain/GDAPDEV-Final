using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Element element = Element.Fire;

    EnergySystem energy;
    private float energyMax = 100f;
    private Bar energyBar;
    private float energyRechargeRate = 1.0f;

    ManaSystem mana;
    private float manaMax = 100f;
    private Bar manaBar;

    // Start is called before the first frame update
    void Start()
    {
        energy = new EnergySystem(energyMax);
        energyBar = GameObject.Find("Energy Bar").GetComponent<Bar>();
        mana = new ManaSystem(manaMax);
        manaBar = GameObject.Find("Mana Bar").GetComponent<Bar>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!energy.IsEnergyBroken())
        {
            energy.Recharge(energyRechargeRate * Time.deltaTime);
        }
        //Damaged
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(energy.IsEnergyBroken())
            {
                Die();
            }
            energy.Damage(10f);
            Debug.Log("Energy: " + energy.GetEnergy());
            Debug.Log("Energy Percent: " + energy.GetEnergyPercent());
        }
        //Consumed Mana
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        //Mana Recharge
        if(Input.GetKeyDown(KeyCode.C))
        {
            RechargeMana();
        }
        energyBar.UpdateBar(energy.GetEnergyPercent());
        energyBar.UpdateValue(energy.GetEnergy(), energyMax);
        manaBar.UpdateBar(mana.GetManaPercent());
        manaBar.UpdateValue(mana.GetMana(), manaMax);
    }

    public void Shoot()
    {
        if (mana.GetMana() > 0)
        {
            mana.Consume(15f);
        }
    }

    public void RechargeMana()
    {
        if(mana.GetMana() < manaMax)
        {
            mana.Recharge(manaMax);
            Debug.Log("Mana recharged.");
        }
    }

    private void Die()
    {
        //Display results
    }

    public void ChangeElement(Element newElement)
    {
        this.element = newElement;
    }

}
