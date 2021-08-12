using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_ToggleMenu : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject menu;

    // Start is called before the first frame update
    void Start()
    {
        //ToggleMenu();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMenuToggleRequest();
    }

    private void OnEnable()
    {
        SetInitialReferences();
        gameManager.GameOverEvent += ToggleMenu;
    }

    private void OnDisable()
    {
        gameManager.GameOverEvent -= ToggleMenu;
    }

    void SetInitialReferences()
    {
        gameManager = this.gameObject.GetComponent<GameManager>();
    }

    void CheckForMenuToggleRequest()
    {
        if(Input.GetKeyUp(KeyCode.Escape) && !gameManager.isGameOver && !gameManager.isShopUIOn)
        {
            ToggleMenu();
        }    
    }

    void ToggleMenu()
    {
        if(menu != null)
        {
            
            menu.SetActive(!menu.activeSelf);
            gameManager.isMenuOn = !gameManager.isMenuOn;
            if (gameManager.isGameOver)
            {
                menu.SetActive(false);
                gameManager.isMenuOn = false;
            }
            gameManager.CallEventMenuToggle();
        }
        else
        {
            Debug.LogWarning("Assign UI Menu to GameManager_ToggleMenu");
        }
    }

}
