using UnityEngine;
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

    public void Recharge(float amount)
    {
        mana += amount;
        if (mana > manaMax)
            mana = manaMax;
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
