using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ElementToggle : MonoBehaviour
{
    public UnityEvent<Element> OnToggleSwitched;
    private ToggleGroup toggleGroup;
    // Start is called before the first frame update
    private void Start()
    {
        toggleGroup = this.GetComponent<ToggleGroup>();
    }
    
    public void ToggleSwitch(int index)
    {
        OnToggleSwitched.Invoke((Element)index);
    }
}
