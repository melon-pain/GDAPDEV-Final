using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LevelClear
{
    Fail,
    Success
}

public class GameManager_Level : MonoBehaviour
{
    public static GameManager_Level Instance;
    private LevelClear levelClear;
    public static int score { get; private set; }
    public int enemiesKilled = 0;
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
    }

    public static void AddScore(int amount)
    {
        score += amount;
    }

    public void AddEssenceToCurrency(int amount)
    {
        if(levelClear == LevelClear.Success)
        {
            GameManager_Currency.AddEssence(enemiesKilled * enemyEssenceDrop);
        }
    }
}
