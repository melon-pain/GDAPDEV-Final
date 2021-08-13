using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    [SerializeField] private Text gameOver;
    [SerializeField] private Text time;
    [SerializeField] private Text calculateScore;
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
            float levelTimeScoreMultiplier = GameManager_Level.timeScoreMultiplier;
            float levelTimer = GameManager_Level.timer;
            int levelScore = GameManager_Level.score;
            int levelFinalScore = GameManager_Level.finalScore;
            int levelHighScore = GameManager_Level.highScore;

            if (GameManager_Level.levelClear)
                gameOver.text = "Level Cleared!";
            else
                gameOver.text = "Level Failed";
            time.text = $"Time Finished = {levelTimer.ToString("F2")} seconds";

            if (GameManager_Level.timer < 180 && GameManager_Level.levelClear)
            {
                score.text = $"Final Score = {levelScore} + {180 * levelTimeScoreMultiplier} - {(int)(levelTimer * levelTimeScoreMultiplier)} = {levelFinalScore}";
                calculateScore.text = "Final Score Calculation = Score + (180 * 5) - (Time * 5)";
            }
            else if (GameManager_Level.timer >= 300 && GameManager_Level.levelClear)
            {
                score.text = $"Final Score = {levelScore} - {180 * levelTimeScoreMultiplier} = {levelFinalScore}";
                calculateScore.text = "Final Score Calculation = Score - (Time * 5)";
            }
            else if (GameManager_Level.levelClear)
            {
                score.text = $"Final Score = {levelScore}";
                calculateScore.text = "Final Score Calculation = Score";
            }
            else if (!GameManager_Level.levelClear)
            {
                score.text = $"Final Score = {levelScore} - 500";
                calculateScore.text = "Final Score Calculation = Score - 500";
            }

            highScore.text = $"High Score = {levelHighScore}";
            essence.text = $"Essence gained = {GameManager_Level.essenceGained}";
        }
    }
}
