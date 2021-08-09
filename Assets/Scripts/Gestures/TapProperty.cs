using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TapProperty
{
    [SerializeField] private float tapTime = 0.7f;
    [SerializeField] private float tapDistance = 0.1f;

    public float GetTapTime()
    {
        return tapTime;
    }

    public float GetTapDistance()
    {
        return tapDistance;
    }
}
