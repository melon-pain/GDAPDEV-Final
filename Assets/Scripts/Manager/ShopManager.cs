using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private int ES_Regen_Limit = 5;
    [SerializeField] private int ES_Max_Limit = 5;
    [SerializeField] private int MP_Max_Limit = 5;
    [SerializeField] private int MP_Cost_Limit = 5;
    [SerializeField] private int EP_Damage_Limit = 5;
    [SerializeField] private int EP_Speed_Limit = 5;
    [SerializeField] private int EP_FireRate_Limit = 5;


    
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager_Upgrades.Instance == null)
        {
            Debug.LogWarning("There is no GameManager Upgrades");
            this.enabled = false;
        }
    }

    public void PurchaseUpgrade(int upgradeIndex)
    {
        switch (upgradeIndex)
        {
            case 0: if (GameManager_Upgrades.ES_Recharge < ES_Regen_Limit)
                    GameManager_Upgrades.ES_Recharge++; break; 
            case 1: if (GameManager_Upgrades.ES_Max < ES_Max_Limit)
                    GameManager_Upgrades.ES_Max++; break;
            case 2: if (GameManager_Upgrades.MP_Max < MP_Max_Limit)
                    GameManager_Upgrades.MP_Max++; break;
            case 3: if (GameManager_Upgrades.MP_Cost < MP_Cost_Limit)
                    GameManager_Upgrades.MP_Cost++; break;
            case 4: if (GameManager_Upgrades.EP_Damage < EP_Damage_Limit)
                    GameManager_Upgrades.EP_Damage++; break;
            case 5: if (GameManager_Upgrades.EP_Speed < EP_Speed_Limit)
                    GameManager_Upgrades.EP_Speed++; break;
            case 6: if (GameManager_Upgrades.EP_FireRate < EP_FireRate_Limit)
                    GameManager_Upgrades.EP_FireRate++; break;
            default: Debug.LogWarning("Upgrade Unavailable");
                break;
        }
        //Debug.Log($"Static Regen: {GameManager_Upgrades.ES_Recharge}");
        //Debug.Log($"Static Max: {GameManager_Upgrades.ES_Max}");
    }
}
