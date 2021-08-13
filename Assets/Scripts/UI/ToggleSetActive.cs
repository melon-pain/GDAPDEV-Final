using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSetActive : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    private Toggle toggle;

    private void Start()
    {
        toggle = this.gameObject.GetComponent<Toggle>();
        toggle.isOn = targetObject.activeSelf;
       //OnToggleChange();
    }

    public void OnToggleChange()
    {
        targetObject.SetActive(toggle.isOn);
    }
}
