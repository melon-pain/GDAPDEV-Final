using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerShooting : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] private Element element = Element.Fire;
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float fireRate = 5.0f;
    [SerializeField] private float manaCost = 5.0f;

    [Header("Beam")]
    [SerializeField] private GameObject beam;
    [SerializeField] private GameObject beamTarget;
    [SerializeField] private float beamCooldown = 5.0f;
    [SerializeField] private float beamFireDuration = 3.0f;

    [Header("Crosshair")]
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject target;
    [SerializeField] private RenderObjects renderCrosshair;

    [Header("Mana Modifier")]
    [SerializeField] private float MPCostMod = 0.5f;

    [Header("Elemental Projectile Modifier")]
    [SerializeField] private float EPDamageMod = 1.0f;
    [SerializeField] private float EPSpeed = 2.0f;
    [SerializeField] private float EPFireRate = 0.5f;

    [Header("Core")]
    [SerializeField] private MeshRenderer mesh;

    [Space(4.0f)] public UnityEvent OnSingleFire;
    [Space(4.0f)] public UnityEvent OnBeamReady;
    [Space(4.0f)] public UnityEvent OnBeamFire;

    private bool isFiring = false;
    private bool isFiringBeam = false;
    private bool isBeamReady = true;

    private Player player;
    private float ticks = 0.0f;
    private const float distance = 2.0f;

    // Start is called before the first frame update
    private void Start()
    {
        player = this.gameObject.GetComponent<Player>();

        if (GameManager_Upgrades.Instance != null)
        {
            manaCost -= GameManager_Upgrades.MP_Cost * MPCostMod;
            //Insert Damage Modifier here
            //Insert Speed Modifier here
            fireRate += GameManager_Upgrades.EP_FireRate * EPFireRate;
        }
            if (beam != null)
        {
            beam.SetActive(false);
        }
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

        //Vector3 forward = Quaternion.Euler(Quaternion.AngleAxis(180.0f, Vector3.right) * this.transform.localEulerAngles) * Vector3.forward;
        //Vector3 pos = this.transform.localPosition + (forward * distance);
        //Vector3 dir = target ? (target.transform.localPosition - pos).normalized : forward;

        Vector3 forward = this.transform.forward;
        Vector3 pos = this.transform.position + (forward * distance);
        Vector3 dir = target ? (target.transform.position - pos).normalized : forward;

        Projectile projectile = pool.GetObjectFromPool().GetComponent<Projectile>();
        projectile.Activate(this.element, pos, dir);
        player.mana.Consume(manaCost);
        OnSingleFire.Invoke();
        ticks = 0.0f;
    }

    public void FireBeam(DragEventData dragEventData)
    {
        if (!isFiringBeam && isBeamReady)
        {
            beam.SetActive(true);
            isFiringBeam = true;
            OnBeamFire.Invoke();
            isBeamReady = false;
            StartCoroutine(StopFiringBeam());
            StartCoroutine(BeamCooldownTimer());
        }

        Ray r = Camera.main.ScreenPointToRay(dragEventData.TargetFinger.position);
        //beamTarget.transform.position = new Vector3(dragEventData.TargetFinger.position.x, dragEventData.TargetFinger.position.y, 5.0f);

        //beam.GetComponent<Beam>().SetBeamStartPosition(r.origin);
        GameObject hitObject = dragEventData.HitObject;
        
        if (hitObject)
            beam.GetComponent<Beam>().SetBeamEndPosition(hitObject.transform.position);
        else
            beam.GetComponent<Beam>().SetBeamEndPosition(r.origin + r.direction * 20.0f);
        //beamTarget.transform.position = Vector3.ProjectOnPlane(beamTarget.transform.position, Vector3.forward);

    }

    public void ChangeElement(Element newElement)
    {
        this.element = newElement;
        beam.GetComponent<Beam>().SetBeamElement(newElement);
        switch (this.element)
        {
            case Element.Fire:
                mesh.material.color = Color.red;
                break;
            case Element.Water:
                mesh.material.color = Color.blue;
                break;
            case Element.Electric:
                mesh.material.color = Color.magenta;
                break;
            case Element.Ice:
                mesh.material.color = Color.cyan;
                break;
        }
    }

    public void LockOn(TapEventData tapEventData)
    {
        if (tapEventData.gameObject && (tapEventData.gameObject.tag == "Enemy" || tapEventData.gameObject.tag == "Boss Plate") )
        {
            this.target = tapEventData.gameObject;
            crosshair.transform.parent = target.transform;
            crosshair.transform.localPosition = Vector3.zero;
            renderCrosshair.SetActive(true);
            Debug.Log("Lock on!");
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
        crosshair.transform.localPosition = Vector3.forward;
        crosshair.transform.localRotation = Quaternion.identity;
        crosshair.transform.localScale = Vector3.one;
        renderCrosshair.SetActive(false);
    }

    public void SetFiring(bool value)
    {
        isFiring = value;
    }

    private IEnumerator StopFiringBeam()
    {
        yield return new WaitForSeconds(beamFireDuration);
        isFiringBeam = false;
        beam.SetActive(false);
    }

    private IEnumerator BeamCooldownTimer()
    {
        yield return new WaitForSeconds(beamCooldown);
        Debug.Log("Cooldown done");
        isBeamReady = true;
        OnBeamReady.Invoke();
    }
}
