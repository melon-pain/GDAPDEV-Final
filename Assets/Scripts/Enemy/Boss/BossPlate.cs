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

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(Element attackingElement, float amount)
    {
        if (Elements.GetWeakness(this.element) == attackingElement)
        {
            amount = Mathf.Clamp(amount, 0.0f, this.HP);
            this.HP -= amount;
            OnTakeDamage.Invoke(amount);

            if (this.HP <= 0.0f)
            {
                player.GetComponent<PlayerShooting>().RemoveLockOn();
                Destroy(this.plate);
                Destroy(this.gameObject);
            }
        }
    }
}
