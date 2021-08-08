using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPSCounter : MonoBehaviour
{
    private Text text;
    // Start is called before the first frame update
    private void Start()
    {
        Application.targetFrameRate = 60;
        text = this.GetComponent<Text>();
        InvokeRepeating("FPSCount", 1, 1);
    }

    private void FPSCount()
    {
        text.text = $"FPS: {1.0f / Time.unscaledDeltaTime }";
    }
}
