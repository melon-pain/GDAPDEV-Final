using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Currency : MonoBehaviour
{
    public static GameManager_Currency Instance;
    public static int essence { get; private set; }
    void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static void AddEssence(int amount)
    {
        essence += amount;
    }

    public static void DeductEssence(int amount)
    {
        essence -= amount;
    }

    public static bool CheckIfPurchasable(int amount)
    {
        if (amount > essence)
            return true;
        else
            return false;
    }
}
