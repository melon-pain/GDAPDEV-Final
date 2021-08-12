using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleOptionsUI : MonoBehaviour
{
    //If scene has shop
    [SerializeField] private bool hasOptions;
    [SerializeField] private GameObject optionsUI;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //SetInitialReferences();
    }

    private void OnEnable()
    {
        SetInitialReferences();
        gameManager.GameOverEvent += ToggleOptionsUI;
    }

    private void OnDisable()
    {
        gameManager.GameOverEvent -= ToggleOptionsUI;
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
        if (Input.GetKeyUp(KeyCode.O) && !gameManager.isMenuOn && hasOptions)
        {
            ToggleOptionsUI();
        }
    }

    public void DisableOptionsUI()
    {
        if(gameManager.isOptionsUIOn)
        {
            optionsUI.SetActive(false);
            gameManager.isOptionsUIOn = false;
            gameManager.CallEventOptionsUIToggle();
        }
    }

    public void ToggleOptionsUI()
    {
        if(optionsUI != null)
        {
            if (gameManager.isGameOver)
            {
                DisableOptionsUI();
                return;
            }

            optionsUI.SetActive(!optionsUI.activeSelf);
            gameManager.isOptionsUIOn = !gameManager.isOptionsUIOn;
            
            gameManager.CallEventOptionsUIToggle();
        }
    }

}
