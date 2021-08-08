using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Image bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = this.gameObject.GetComponent<Image>();
    }

    public void ChangeFill(float fillAmount)
    {
        bar.fillAmount = fillAmount;
    }
}
