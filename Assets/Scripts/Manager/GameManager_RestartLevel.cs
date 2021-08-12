using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_RestartLevel : MonoBehaviour
{
    private GameManager gameManager;

    private void OnEnable()
    {
        SetInitialReferences();
        gameManager.RestartLevelEvent += RestartLevel;
    }

    private void OnDisable()
    {
        gameManager.RestartLevelEvent -= RestartLevel;
    }

    void SetInitialReferences()
    {
        gameManager = GetComponent<GameManager>();
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

}
