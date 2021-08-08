enum EnergyState
{
    Active,
    Broken
}

public class EnergySystem
{
    private float energy;
    private float energyMax;

    EnergyState energyState;
    
    public EnergySystem(float energyMax)
    {
        this.energyMax = energyMax;
        energy = energyMax;
        energyState = EnergyState.Active;
    }

    public float GetEnergy()
    {
        return energy;
    }

    public float GetEnergyPercent()
    {
        return energy / energyMax;
    }

    public void Recharge(float amount)
    {
        energy += amount;
        if (energy > energyMax)
            energy = energyMax;
    }

    public bool IsEnergyBroken()
    {
        return (energyState == EnergyState.Broken);
    }

    public void Damage(float damageValue)
    {
        energy -= damageValue;

        if (energy < 0) 
            energy = 0;
        if(energy == 0 && energyState != EnergyState.Broken)
            energyState = EnergyState.Broken;
    }
}
