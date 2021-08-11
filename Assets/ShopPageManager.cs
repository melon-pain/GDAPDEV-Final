using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pages;

    public void ChangePage(int index)
    {
        if(index < pages.Length)
        {
            foreach(GameObject page in pages)
            {
                page.SetActive(false);
            }
            pages[index].SetActive(true);
        }
    }
}
