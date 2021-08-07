using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerStat
{
    [SerializeField] private PlayerBar bar;

    [SerializeField] private float maxValue;
    [SerializeField] private float currValue;

    public float CurrValue
    {
        get
        {
            return currValue;
        }
        set
        {
            this.currValue = value;
            bar.Value = currValue;
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }
        set
        {
            this.MaxValue = value;
            bar.MaxValue = maxValue;
        }
    }

    public void Initialize()
    {
        this.MaxValue = maxValue;
        this.CurrValue = currValue;
    }

}
