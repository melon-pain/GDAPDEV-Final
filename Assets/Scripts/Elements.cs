using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Element
{
    Fire,
    Water,
    Electric,
    Ice
};

public class Elements : MonoBehaviour
{
    public static Element GetWeakness(Element defending)
    {
        switch (defending)
        {
            case Element.Fire:
                return Element.Water;
            case Element.Water:
                return Element.Electric;
            case Element.Electric:
                return Element.Ice;
            case Element.Ice:
                return Element.Fire;
            default:
                return Element.Fire;
        }
    }
}
