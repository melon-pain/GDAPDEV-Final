using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    [Header("Upgrade Limit")]
    [SerializeField] private int ES_Recharge_Limit = 5;
    [SerializeField] private int ES_Max_Limit = 5;
    [SerializeField] private int MP_Max_Limit = 5;
    [SerializeField] private int MP_Cost_Limit = 5;
    [SerializeField] private int EP_Damage_Limit = 5;
    [SerializeField] private int EP_Speed_Limit = 5;
    [SerializeField] private int EP_FireRate_Limit = 5;

    [Header("UI")]
    [SerializeField] private Text textCurrency;
    [SerializeField] private Text[] textInfo = new Text[7];

    public static int ES_Recharge_Price { get; private set; } = 10;
    public static int ES_Max_Price      { get; private set; } = 10;
    public static int MP_Max_Price      { get; private set; } = 10;
    public static int MP_Cost_Price     { get; private set; } = 10;
    public static int EP_Damage_Price   { get; private set; } = 10;
    public static int EP_Speed_Price    { get; private set; } = 10;
    public static int EP_FireRate_Price { get; private set; } = 10;

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        if (GameManager_Upgrades.Instance == null)
        {
            Debug.LogWarning("There is no GameManager Upgrades");
            this.enabled = false;
        }
        for(int i = 0; i < textInfo.Length; i++)
        {
            UpdateTextInfo(i);
        }
    }

    public void PurchaseUpgrade(int upgradeIndex)
    {
        switch (upgradeIndex)
        {
            case 0: if (GameManager_Upgrades.ES_Recharge < ES_Recharge_Limit)
                    if (GameManager_Currency.CheckIfPurchasable(ES_Recharge_Price))
                    {
                        GameManager_Upgrades.ES_Recharge++;
                        GameManager_Currency.DeductEssence(ES_Recharge_Price);
                        ES_Recharge_Price += 2 * GameManager_Upgrades.ES_Recharge;
                    }
                 break; 
            case 1: if (GameManager_Upgrades.ES_Max < ES_Max_Limit)
                    if (GameManager_Currency.CheckIfPurchasable(ES_Max_Price))
                    {
                        GameManager_Upgrades.ES_Max++;
                        GameManager_Currency.DeductEssence(ES_Max_Price);
                        ES_Max_Price += 2 * GameManager_Upgrades.ES_Max;
                    }
                 break;
            case 2: if (GameManager_Upgrades.MP_Max < MP_Max_Limit)
                    if (GameManager_Currency.CheckIfPurchasable(MP_Max_Price))
                    {
                        GameManager_Upgrades.MP_Max++;
                        GameManager_Currency.DeductEssence(MP_Max_Price);
                        MP_Max_Price += 2 * GameManager_Upgrades.MP_Max;
                    }
                 break;
            case 3: if (GameManager_Upgrades.MP_Cost < MP_Cost_Limit)
                    if (GameManager_Currency.CheckIfPurchasable(MP_Cost_Price))
                    {
                        GameManager_Upgrades.MP_Cost++;
                        GameManager_Currency.DeductEssence(MP_Cost_Price);
                        MP_Cost_Price += 2 * GameManager_Upgrades.MP_Cost;
                    }
                 break;
            case 4: if (GameManager_Upgrades.EP_Damage < EP_Damage_Limit)
                    if (GameManager_Currency.CheckIfPurchasable(EP_Damage_Price))
                    {
                        GameManager_Upgrades.EP_Damage++;
                        GameManager_Currency.DeductEssence(EP_Damage_Price);
                        EP_Damage_Price += 2 * GameManager_Upgrades.EP_Damage;
                    }
                break;
            case 5: if (GameManager_Upgrades.EP_Speed < EP_Speed_Limit)
                    if (GameManager_Currency.CheckIfPurchasable(EP_Speed_Price))
                    {
                        GameManager_Upgrades.EP_Speed++;
                        GameManager_Currency.DeductEssence(EP_Speed_Price);
                        EP_Speed_Price += 2 * GameManager_Upgrades.EP_Speed;
                    }
                break;
            case 6: if (GameManager_Upgrades.EP_FireRate < EP_FireRate_Limit)
                    if (GameManager_Currency.CheckIfPurchasable(EP_FireRate_Price))
                    {
                        GameManager_Upgrades.EP_FireRate++;
                        GameManager_Currency.DeductEssence(EP_FireRate_Price);
                        EP_FireRate_Price += 2 * GameManager_Upgrades.EP_FireRate;
                    }
                break;
            default: Debug.LogWarning("Upgrade Unavailable");
                break;
        }
        UpdateTextInfo(upgradeIndex);
    }

    void UpdateTextInfo(int textIndex)
    {
        int currentLevel = 0;
        int price = 0;
        switch (textIndex)
        {
            case 0:
                currentLevel = GameManager_Upgrades.ES_Recharge;
                price = ES_Recharge_Price; break;
            case 1:
                currentLevel = GameManager_Upgrades.ES_Max;
                price = ES_Max_Price; break;
            case 2:
                currentLevel = GameManager_Upgrades.MP_Max;
                price = MP_Max_Price; break;
            case 3:
                currentLevel = GameManager_Upgrades.MP_Cost;
                price = MP_Cost_Price; break;
            case 4:
                currentLevel = GameManager_Upgrades.EP_Damage;
                price = EP_Damage_Price; break;
            case 5:
                currentLevel = GameManager_Upgrades.EP_Speed;
                price = EP_Speed_Price; break;
            case 6:
                currentLevel = GameManager_Upgrades.EP_FireRate;
                price = EP_FireRate_Price; break;
            default:
                currentLevel = 0;
                price = 0; break;
        }
        textCurrency.text = $"Essence: {GameManager_Currency.essence}";
        if (currentLevel == 5)
            textInfo[textIndex].text = $"Current Level:\n{currentLevel} MAX\n Price:\nN/A";
        else
            textInfo[textIndex].text = $"Current Level:\n{currentLevel}\n Price:\n{price}";
    }
}
