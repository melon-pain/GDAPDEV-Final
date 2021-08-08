using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            RechargeMana();
        }
        energyBar.ChangeFill(energy.GetEnergyPercent());
        manaBar.ChangeFill(mana.GetManaPercent());
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

}
