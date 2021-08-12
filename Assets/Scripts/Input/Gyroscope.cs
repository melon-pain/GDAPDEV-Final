using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class Gyroscope : MonoBehaviour
{
    private Player player;
    [SerializeField] private float manaRechargeRange = 5.0f;
    [SerializeField] private Text gyroTxt;
    [Space(4.0f)] public UnityEvent OnGyroTiltDown;
    

    // Start is called before the first frame update
    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.Log("No gyro");
        }
        player = this.transform.parent.gameObject.GetComponentInChildren<Player>();
    }
    private void FixedUpdate()
    {
        if (!Input.gyro.enabled)
            Input.gyro.enabled = true;

        gyroTxt.text = $"Gyroscope: {Input.gyro.rotationRate}";
        if (Input.gyro.rotationRate.x <= -manaRechargeRange)
        {
            OnGyroTiltDown.Invoke();
            Debug.Log("Invoking Gyro");
        }
    }
}
