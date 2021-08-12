using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetActive : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;


    public void SetAllObjects(bool flag)
    {
        foreach(GameObject obj in targetObjects)
        {
            obj.SetActive(flag);
        }
    }
}
