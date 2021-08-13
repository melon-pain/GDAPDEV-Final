using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_Cheat : MonoBehaviour
{
    public static GameManager_Cheat Instance;
    public static bool isInvincible = false;
    public static bool isInfiniteMana = false;

    [SerializeField] private Toggle invincibleToggle;
    [SerializeField] private Toggle infiniteManaToggle;


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

        invincibleToggle.isOn = isInvincible;
        infiniteManaToggle.isOn = isInfiniteMana;
    }

    public void OnInvincibleToggleChange()
    {
        isInvincible = invincibleToggle.isOn;
    }

    public void OnInfiniteManaToggleChange()
    {
        isInfiniteMana = infiniteManaToggle.isOn;
    }
     
}
