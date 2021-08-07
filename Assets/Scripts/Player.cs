using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerStat energy;

    // Start is called before the first frame update
    private void Awake()
    {
        energy.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            energy.CurrValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            energy.CurrValue += 10;
        }

    }
}
