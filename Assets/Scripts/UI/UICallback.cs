using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICallback : MonoBehaviour
{
    public GameObject colorPanel;
    public GameObject inputFieldLabel;
    public GameObject dropdownLabel;
    public GameObject toggleLabel;

    //For color slider
    private float r = 0;
    private float g = 0;
    private float b = 0;

    //Input Field
    public void OnInputTextChange(InputField iF)
    {
        inputFieldLabel.GetComponent<Text>().text = iF.text;
    }

    public void OnInputTextDone(InputField iF)
    {
        inputFieldLabel.GetComponent<Text>().text = iF.text;
    }
    //Slider
    public void OnSliderColorChange(Slider sl)
    {
        if (sl.name == "R")
            r = sl.value;
        else if (sl.name == "G")
            g = sl.value;
        else if (sl.name == "B")
            b = sl.value;

        colorPanel.GetComponent<Image>().color = new Color(r, g, b, 255);
    }
    //Dropdown
    public void OnDropChanges(Dropdown dd)
    {
        dropdownLabel.GetComponent<Text>().text = dd.options[dd.value].text;
    }
    //Toggle
    public void OnToggleChange(Toggle t)
    {
        toggleLabel.GetComponent<Text>().text = t.name;
    }



}
