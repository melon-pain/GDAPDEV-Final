using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    [SerializeField] private Text gameOver;
    [SerializeField] private Text time;
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
            time.text = $"Time Finished = {(int)GameManager_Level.timer}";
            score.text = $"Final Score = {GameManager_Level.score} - {(int)(GameManager_Level.timer * GameManager_Level.timeScoreMultiplier)} = {GameManager_Level.finalScore}";
            highScore.text = $"High Score = {GameManager_Level.highScore}";
            essence.text = $"Essence gained = {GameManager_Level.essenceGained}";
        }
    }
}
