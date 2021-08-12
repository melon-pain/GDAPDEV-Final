using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_TogglePause : MonoBehaviour
{
    private GameManager gameManager;
    private bool isPaused;

    private void OnEnable()
    {
        SetInitialReferences();
        gameManager.MenuToggleEvent += TogglePause;
        //if (!gameManager.isGameOver)
        gameManager.ShopToggleEvent += TogglePause;
        
        //gameManager.GameOverEvent += TogglePause;
        //gameManager.RestartLevelEvent += TogglePause;
    }

    private void OnDisable()
    {
        gameManager.MenuToggleEvent -= TogglePause;
        //if (!gameManager.isGameOver)
            gameManager.ShopToggleEvent -= TogglePause;
        //gameManager.GameOverEvent -= TogglePause;
        //gameManager.RestartLevelEvent -= TogglePause;
    }

    void SetInitialReferences()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void TogglePause()
    {
        if(isPaused && (!gameManager.isGameOver))
        {
            Time.timeScale = 1.0f;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0.0f;
            isPaused = true;
        }
    }
}
