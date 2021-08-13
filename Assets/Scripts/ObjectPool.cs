using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject original;
    [SerializeField, Min(0)] private int poolCount = 10;
    [SerializeField] private List<GameObject> objectPool = new List<GameObject>();

    private int currentObject = -1;

    // Start is called before the first frame update
    private void Start()
    {
        original.SetActive(false);
        for (int i = 0; i < poolCount; i++)
        {
            GameObject obj = GameObject.Instantiate(original, this.transform);
            objectPool.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        currentObject++;
        if (currentObject >= poolCount)
        {
            currentObject = 0;
        }
        objectPool[currentObject].SetActive(true);
        return objectPool[currentObject];
    }

    public int GetPoolCount()
    {
        return poolCount;
    }
}
