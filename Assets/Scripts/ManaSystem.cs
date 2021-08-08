public class ManaSystem
{
    private float mana;
    private float manaMax;
    
    public ManaSystem(float manaMax)
    {
        this.manaMax = manaMax;
        mana = manaMax;
    }

    public float GetMana()
    {
        return mana;
    }

    public float GetManaPercent()
    {
        return mana / manaMax;
    }

    public void PassiveRegen(float regenRate)
    {
        mana += regenRate;
    }

    public void Consume(float amount)
    {
        mana -= amount;
        if(mana < 0)
        {
            mana = 0;
        }
    }

}
