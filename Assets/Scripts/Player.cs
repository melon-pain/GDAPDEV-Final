using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public EnergySystem energy;
    private float energyMax = 100f;
    private Bar energyBar;
    private float energyRechargeRate = 1.0f;

    public ManaSystem mana;
    private float manaMax = 100f;
    private Bar manaBar;

    [Header("Energy Shield Modifier")]
    [SerializeField] private float ESRechargeMod = 0.5f;
    [SerializeField] private float ESMaxMod = 10.0f;
    
    [Header("Mana Modifier")]
    [SerializeField] private float MPMaxMod = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager_Upgrades.Instance != null)
        {
            energyRechargeRate += GameManager_Upgrades.ES_Recharge * ESRechargeMod;
            energyMax += GameManager_Upgrades.ES_Max * ESMaxMod;
            manaMax += GameManager_Upgrades.MP_Max * MPMaxMod;
            Debug.Log($"Energy: {energyMax}");
        }
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
            GetDamage();
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

    public void RechargeMana()
    {
        if(mana.GetMana() < manaMax)
        {
            mana.Recharge(manaMax);
            Debug.Log("Mana recharged.");
        }
    }

    public void GetDamage()
    {
        // Invulnerable while rolling
        if (this.GetComponent<PlayerMovement>().isRolling)
            return;

        if (energy.IsEnergyBroken())
        {
            Die();
            return;
        }
        energy.Damage(10f);
        if(energy.IsEnergyBroken())
        {
            GameObject fatalText = GameObject.Find("Fatal Text");
            if(fatalText != null)
                fatalText.GetComponent<Text>().enabled = true;
        }
    }

    public void TakeDamage(float amount)
    {
        // Invulnerable while rolling
        if (this.GetComponent<PlayerMovement>().isRolling)
            return;

        if (energy.IsEnergyBroken())
        {
            Die();
            return;
        }
        energy.Damage(amount);
        if (energy.IsEnergyBroken())
        {
            GameObject fatalText = GameObject.Find("Fatal Text");
            if (fatalText != null)
                fatalText.GetComponent<Text>().enabled = true;
        }
    }

    private void Die()
    {
        //Display results
        Debug.Log("Player Dead!");
    }

}
