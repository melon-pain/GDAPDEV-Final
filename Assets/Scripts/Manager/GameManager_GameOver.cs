using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_GameOver : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        SetInitialReferences();
        gameManager.GameOverEvent += TurnOnGameOverPanel;
    }

    private void OnDisable()
    {
        gameManager.GameOverEvent -= TurnOnGameOverPanel;
    }

    void SetInitialReferences()
    {
        gameManager = GetComponent<GameManager>();
    }

    void TurnOnGameOverPanel()
    {
        if(gameOverPanel != null)
        {
            if(gameManager.isMenuOn)
            {
                Debug.Log("Calling Event Menu Toggle");
                gameManager.CallEventMenuToggle();
            }
            gameOverPanel.SetActive(true);
        }
    }

}
