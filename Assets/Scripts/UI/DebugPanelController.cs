using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanelController : MonoBehaviour
{
    public static DebugPanelController Instance;
    public static bool isPanelActive = false;
    [SerializeField] private GameObject debugPanel;
    [SerializeField] private Toggle toggle;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Destroying");
            Destroy(this.gameObject);
        }

        toggle.isOn = isPanelActive;
        debugPanel.SetActive(isPanelActive);
    }

    public void OnToggleChange()
    {
        isPanelActive = toggle.isOn;
        debugPanel.SetActive(toggle.isOn);
    }
}
