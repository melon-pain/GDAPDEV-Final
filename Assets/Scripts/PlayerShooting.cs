using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] private Element element = Element.Fire;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float fireRate = 5.0f;
    [SerializeField] private float manaCost = 1.0f;

    [Header("Crosshair")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject target;

    private bool isFiring = false;

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

        if (isFiring)
            Fire();
    }

    private void Fire()
    {
        if (ticks < 1.0f / fireRate || player.mana.GetMana() <= 0f)
            return;
        Projectile projectile = pool.GetObjectFromPool().GetComponent<Projectile>();

        Vector3 pos = this.transform.localPosition + (Vector3.forward * distance);
        Vector3 dir = target ? (target.transform.localPosition - pos).normalized : Vector3.forward;
        Debug.Log($"X: {dir.x} " + $"Y: {dir.y} " + $"Z: {dir.z}");
        projectile.Activate(this.element, pos, dir);
        player.mana.Consume(manaCost);
        ticks = 0.0f;
    }

    public void ChangeElement(Element newElement)
    {
        this.element = newElement;
    }

    public void LockOn(TapEventData tapEventData)
    {
        if (tapEventData.gameObject)
        {
            this.target = tapEventData.gameObject;
            crosshair.transform.parent = target.transform;
            crosshair.transform.localPosition = Vector3.zero;
        }
        else
        {
            RemoveLockOn();
        }
    }

    public void RemoveLockOn()
    {
        this.target = null;
        crosshair.transform.parent = this.transform;
        crosshair.transform.localPosition = Vector3.zero;
    }

    public void SetFiring(bool value)
    {
        isFiring = value;
    }
}
