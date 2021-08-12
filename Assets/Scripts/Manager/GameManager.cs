using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameManagerEventHandler();
    public event GameManagerEventHandler MenuToggleEvent;
    public event GameManagerEventHandler ShopToggleEvent;
    public event GameManagerEventHandler OptionsToggleEvent;
    public event GameManagerEventHandler RestartLevelEvent;
    public event GameManagerEventHandler GoToMenuSceneEvent;
    public event GameManagerEventHandler GameOverEvent;

    public bool isGameOver;
    public bool isShopUIOn;
    public bool isMenuOn;
    public bool isOptionsUIOn;
    
    public void CallEventMenuToggle()
    {
        if(MenuToggleEvent != null)
        {
            MenuToggleEvent();
        }
    }

    public void CallEventShopUIToggle()
    {
        if(ShopToggleEvent != null)
        {
            ShopToggleEvent();
        }
    }

    public void CallEventOptionsUIToggle()
    {
        if (OptionsToggleEvent != null)
        {
            OptionsToggleEvent();
        }
    }


    public void CallEventRestartLevel()
    {
        if (RestartLevelEvent != null)
        {
            RestartLevelEvent();
        }
    }
    public void CallEventMenuScene()
    {
        if (GoToMenuSceneEvent != null)
        {
            GoToMenuSceneEvent();
        }
    }
    public void CallEventGameOver()
    {
        if (GameOverEvent != null)
        {
            isGameOver = true;
            GameOverEvent();
        }
    }
}


