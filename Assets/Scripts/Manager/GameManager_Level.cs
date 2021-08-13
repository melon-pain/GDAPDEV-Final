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
    public static int essenceGained;

    private const int enemyScore = 100;
    private const int enemyEssenceDrop = 2;
    public static float timeScoreMultiplier = 0.5f;


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

    private void AddEssenceToCurrency(int amount)
    {
        Debug.Log("Added Currency");
        GameManager_Currency.AddEssence(enemyKillCount * enemyEssenceDrop);
    }

    public void Results()
    {
        isResults = true;
        finalScore = score - (int) (timer * timeScoreMultiplier);
        
        if (finalScore < 0)
            finalScore = 0;

        if(highScore < finalScore)
            highScore = finalScore;
        
        if(levelClear == true)
        {
            essenceGained += enemyKillCount * enemyEssenceDrop;
            AddEssenceToCurrency(essenceGained);
        }
    }

}
