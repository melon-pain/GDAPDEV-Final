using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Level : MonoBehaviour
{
    GameManager gameManager;
    public static GameManager_Level Instance;

    public static int score { get; private set; }
    public static int highScore { get; private set; } = 0;
    public static int finalScore { get; private set; }
    public static float timer { get; private set; }
    public static bool levelClear;
    
    private bool isResults;

    public static int enemyKillCount;
    public static int bossKillCount;
    public static int essenceGained;

    private const int enemyScore = 100;
    private const int bossScore = 2000;
    private const int enemyEssenceDrop = 2;
    private const int bossEssenceDrop = 40;
    public static float timeScoreMultiplier = 5.0f;


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
        isResults = false;
        score = 0;
        finalScore = 0;
        enemyKillCount = 0;
        essenceGained = 0;
        timer = 0f;
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

    private void Update()
    {
        if(!isResults)
            timer += Time.deltaTime;
    }

    public static void AddKillCount()
    {
        enemyKillCount++;
        score += enemyScore;
    }

    public static void AddBossKillCount()
    {
        bossKillCount++;
        score += bossScore;
    }

    private void AddEssenceToCurrency(int amount)
    {
        GameManager_Currency.AddEssence(amount);
    }

    public void Results()
    {
        isResults = true;
        if (timer < 180 && levelClear)
            finalScore = score + (int)(180 * timeScoreMultiplier) - (int)(timer * timeScoreMultiplier);
        else if (timer >= 300 && levelClear)
            finalScore = score - (int)(timer * timeScoreMultiplier);
        else if (levelClear)
            finalScore = score;
        else if (!levelClear)
            finalScore = score - 500;

        if (finalScore < 0)
            finalScore = 0;

        if(highScore < finalScore)
            highScore = finalScore;

        if (levelClear == true)
        {
            essenceGained += (enemyKillCount * enemyEssenceDrop) + (bossKillCount * bossEssenceDrop);
            Debug.Log($"Essence gained: {essenceGained}");
            AddEssenceToCurrency(essenceGained);
        }
    }

}
