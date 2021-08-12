using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleShopUI : MonoBehaviour
{
    //If scene has shop
    [SerializeField] private bool hasShop;
    [SerializeField] private GameObject shopUI;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        SetInitialReferences();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForShopUIToggleRequest();
    }

    void SetInitialReferences()
    {
        gameManager = GetComponent<GameManager>();
    }

    void CheckForShopUIToggleRequest()
    {
        if (Input.GetKeyUp(KeyCode.U) && !gameManager.isMenuOn && hasShop)
        {
            ToggleShopUI();
        }
    }

    public void ToggleShopUI()
    {
        if(shopUI != null)
        {
            shopUI.SetActive(!shopUI.activeSelf);
            gameManager.isShopUIOn = !gameManager.isShopUIOn;
            gameManager.CallEventShopUIToggle();
        }
    }

}
