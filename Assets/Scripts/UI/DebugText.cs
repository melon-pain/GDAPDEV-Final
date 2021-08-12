using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugText : MonoBehaviour
{
    [SerializeField] private Text essence;

    private void Update()
    {
        SetText();
    }

    public void SetText()
    {
        if(GameManager_Currency.Instance != null)
            essence.text = $"Essence: {GameManager_Currency.essence}";
    }
}
