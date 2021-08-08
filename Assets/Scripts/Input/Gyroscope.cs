using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gyroscope : MonoBehaviour
{
    private Player player;
    [SerializeField] private float manaRechargeRange = 3.0f;
    [SerializeField] private Text gyroTxt;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent.gameObject.GetComponentInChildren<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.gyro.enabled)
            Input.gyro.enabled = true;

        gyroTxt.text = $"Gyroscope: {Input.gyro.rotationRate}";
        if (Input.gyro.rotationRate.x <= -manaRechargeRange)
            player.RechargeMana();
    }
}
