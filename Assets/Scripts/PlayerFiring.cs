using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] private Element element = Element.Fire;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float fireRate = 5.0f;

    private Player player;
    private float ticks = 0.0f;
    private const float distance = 3.0f;
    // Start is called before the first frame update
    private void Start()
    {
        player = this.gameObject.GetComponent<Player>();
        ticks = 1.0f / fireRate;
    }

    // Update is called once per frame
    private void Update()
    {
        ticks += Time.deltaTime;
    }

    public void Fire()
    {
        if (ticks < 1.0f / fireRate || player.mana.GetMana() <= 0f)
            return;
        Projectile projectile = pool.GetObjectFromPool().GetComponent<Projectile>();
        Debug.Log("Player: " + this.transform.forward);
        projectile.Activate(this.element, this.transform.position + (this.transform.forward * distance), this.transform.forward);
        player.mana.Consume(15f);
        ticks = 0.0f;
    }

    public void ChangeElement(Element newElement)
    {
        this.element = newElement;
    }

}
