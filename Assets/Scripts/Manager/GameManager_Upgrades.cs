using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Upgrades : MonoBehaviour
{
    public static GameManager_Upgrades Instance;
    //Energy Shield
    public static int ES_Recharge = 0;
    public static int ES_Max = 0;
    //Mana
    public static int MP_Max = 0;
    public static int MP_Cost = 0;
    //Elemental Projectile
    public static int EP_Damage = 0;
    public static int EP_Speed = 0;
    public static int EP_FireRate = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
