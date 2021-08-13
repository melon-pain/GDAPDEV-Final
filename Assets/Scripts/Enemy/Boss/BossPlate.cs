using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossPlate : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private Element element;
    [SerializeField] private float HP = 250.0f;

    [Header("Plate")]
    [SerializeField] private GameObject plate;

    public UnityEvent<float> OnTakeDamage;

    public void TakeDamage(Element attackingElement, float amount)
    {
        if (Elements.GetWeakness(this.element) == attackingElement)
        {
            amount = Mathf.Clamp(amount, 0.0f, this.HP);
            this.HP -= amount;
            OnTakeDamage.Invoke(amount);

            Debug.Log("Plate DMG");

            if (this.HP <= 0.0f)
            {
                Destroy(this.plate, 1);
                Destroy(this.gameObject, 1);
            }
        }
    }
}
