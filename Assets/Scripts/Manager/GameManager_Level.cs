using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Level : MonoBehaviour
{
    GameManager gameManager;
    public static GameManager_Level Instance;

    public static int score { get; private set; }
    public static int highScore { get; private set; } = 0;
    public static bool levelClear;
    public static int enemyKillCount;
    public static int essenceGained;

private const int enemyScore = 20;
    private const int enemyEssenceDrop = 2;

    void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        levelClear = false;
        score = 0;
        enemyKillCount = 0;
        essenceGained = 0;
        SetInitialReferences();
        gameManager.GameOverEvent += Results;
    }

    private void OnDisable()
    {
        gameManager.GameOverEvent -= Results;
    }

    private void SetInitialReferences()
    {
        gameManager = GetComponent<GameManager>();
    }

    public static void AddKillCount()
    {
        enemyKillCount++;
        score += enemyScore;
    }

    private void AddEssenceToCurrency(int amount)
    {
        Debug.Log("Added Currency");
        GameManager_Currency.AddEssence(enemyKillCount * enemyEssenceDrop);
    }

    public void Results()
    {
        if(highScore < score)
        {
            highScore = score;
        }
        if(levelClear == true)
        {
            essenceGained += enemyKillCount * enemyEssenceDrop;
            AddEssenceToCurrency(essenceGained);
        }
    }

}
