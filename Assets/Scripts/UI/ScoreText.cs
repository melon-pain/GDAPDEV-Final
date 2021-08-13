using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] Text score; 

    // Update is called once per frame
    void Update()
    {
        if(GameManager_Level.Instance != null)
        {
            score.text = $"Score: {GameManager_Level.score}";
        }
    }
}
