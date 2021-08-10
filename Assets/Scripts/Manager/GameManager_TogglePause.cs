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
        gameManager.ShopToggleEvent += TogglePause;
    }

    private void OnDisable()
    {
        gameManager.MenuToggleEvent -= TogglePause;
        gameManager.ShopToggleEvent -= TogglePause;
    }

    void SetInitialReferences()
    {
        gameManager = GetComponent<GameManager>();
    }

    void TogglePause()
    {
        if(isPaused)
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
