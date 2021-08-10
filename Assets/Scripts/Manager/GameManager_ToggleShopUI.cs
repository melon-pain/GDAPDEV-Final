using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleShopUI : MonoBehaviour
{
    //If scene has shop
    [SerializeField] private bool hasShop;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private string toggleShopButton;
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
        if(toggleShopButton == "")
        {
            Debug.LogWarning("Type the name of button used for toggling Shop UI");
            this.enabled = false;
        }
    }

    void CheckForShopUIToggleRequest()
    {
        if(Input.GetButtonUp(toggleShopButton) && !gameManager.isMenuOn && !gameManager.isGameOver && hasShop)
        {
            ToggleShopUI();
        }
    }

    void ToggleShopUI()
    {
        if(shopUI != null)
        {
            shopUI.SetActive(!shopUI.activeSelf);
            gameManager.isShopUIOn = !gameManager.isShopUIOn;
            gameManager.CallEventShopUIToggle();
        }
    }

}
