using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Image bar;
    private Text valueText;
    [SerializeField] private float lerpSpeed = 6f;

    [SerializeField] private Color fullColor;
    [SerializeField] private Color lowColor;

    [SerializeField] private bool lerpColors;

    // Start is called before the first frame update
    void Start()
    {
        valueText = this.gameObject.GetComponentInChildren<Text>();
        bar = this.gameObject.GetComponent<Image>();
        if(lerpColors)
        {
            bar.color = fullColor;
        }
    }

    public void UpdateBar(float fillAmount)
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

        if(lerpColors)
            bar.color = Color.Lerp(lowColor, fullColor, fillAmount);
    }

    public void UpdateValue(float value, float maxValue)
    {
        if(valueText != null)
        {
            valueText.text = $"{(int)value}/{(int)maxValue}";
        }
    }
}
