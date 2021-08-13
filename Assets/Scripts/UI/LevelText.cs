using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelText : MonoBehaviour
{
    [SerializeField] Text score;
    [SerializeField] Text timer;

    // Update is called once per frame
    void Update()
    {
        if(GameManager_Level.Instance != null)
        {
            score.text = $"Score: {GameManager_Level.score}";
            timer.text = $"Time: {(int)GameManager_Level.timer}";
        }
    }
}
