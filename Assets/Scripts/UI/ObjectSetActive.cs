using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSetActive : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;

    public void EnableObjectByIndex(int index)
    {
        targetObjects[index].SetActive(true);
    }

    public void DisableObjectByIndex(int index)
    {
        targetObjects[index].SetActive(false);
    }

    public void SetAllObjects(bool flag)
    {
        foreach(GameObject obj in targetObjects)
        {
            obj.SetActive(flag);
        }
    }
}
