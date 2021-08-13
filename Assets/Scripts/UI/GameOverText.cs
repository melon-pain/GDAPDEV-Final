using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    [SerializeField] private Text gameOver;
    [SerializeField] private Text highScore;
    [SerializeField] private Text score;
    [SerializeField] private Text essence;

    private void Update()
    {
        SetText();
    }

    public void SetText()
    {
        if(GameManager_Level.Instance != null)
        {
            if (GameManager_Level.levelClear)
                gameOver.text = "Level Cleared!";
            else
                gameOver.text = "Level Failed";
            highScore.text = $"High Score:\n {GameManager_Level.highScore}";
            score.text = $"Score:\n {GameManager_Level.score}";
            essence.text = $"Essence gained:\n{GameManager_Level.essenceGained}";
        }
    }
}
