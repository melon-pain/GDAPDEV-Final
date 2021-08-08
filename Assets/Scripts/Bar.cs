using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Image bar;
    private Text valueText;

    // Start is called before the first frame update
    void Start()
    {
        valueText = this.gameObject.GetComponentInChildren<Text>();
        bar = this.gameObject.GetComponent<Image>();
    }

    public void UpdateBar(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }

    public void UpdateValue(float value, float maxValue)
    {
        if(valueText != null)
        {
            valueText.text = $"{(int)value}/{(int)maxValue}";
        }
    }
}
