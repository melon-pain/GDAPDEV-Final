using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private Element element;
    [SerializeField] private float HP = 100.0f;
    public bool isDead { get; private set; } = false;

    [Header("Shooting")]
    [SerializeField] private EnemyProjectile enemyProjectile;
    [SerializeField] private float maxShootTime = 2.0f;

    private bool isShooting = false;
    private float shootTime = 0.0f;
    private float shootInterval = 0.0f;
    public CinemachineDollyCart cart;
    [HideInInspector] public float cartPos = 0.0f;
    public bool isMoving { get; private set; } = true;

    [Header("Body")]
    [SerializeField] private SkinnedMeshRenderer body;
    [SerializeField] private List<Color> bodyColors = new List<Color>(4);

    [Header("Core")]
    [SerializeField] private SkinnedMeshRenderer core;
    [ColorUsageAttribute(true, true)] [SerializeField] private List<Color> coreColors = new List<Color>(4);

    private GameObject player; //Player reference
    private Animator animator;


    // Start is called before the first frame update
    private void Start()
    {
        shootInterval = Random.Range(1.0f, 2.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        animator = this.GetComponent<Animator>();

        this.Activate();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDead)
            return;

        if (isMoving)
        {
            if (cart.m_Position >= cartPos)
            {
                isMoving = false;
                Destroy(cart);
            }
            return;
        }

        if (isShooting)
        {
            shootTime -= Time.deltaTime * shootInterval;
            if (shootTime <= 0.0f)
            {
                isShooting = false;
                animator.SetBool("IsShooting", false);
                enemyProjectile.particles.Stop();
                shootInterval = Random.Range(1.0f, 2.0f);
            }
        }
        else
        {
            shootTime += Time.deltaTime * shootInterval;
            if (shootTime >= maxShootTime)
            {
                isShooting = true;
                animator.SetBool("IsShooting", true);
                enemyProjectile.particles.Play();
                shootInterval = Random.Range(1.0f, 2.0f);
            }

            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(player.transform.position - this.transform.position), 0.5f);
        }
    }

    public void TakeDamage(Element attackingElement, float amount)
    {
        if (Elements.GetWeakness(this.element) == attackingElement)
        {
            this.HP -= amount;
            if (this.HP <= 0.0f)
            {
                isDead = true;
                isShooting = false;
                Deactivate();
            }
        }
    }

    private void Activate()
    {
        int rand = Random.Range(0, 4);
        this.element = (Element)rand;

        body.material.SetColor("_BaseColor", bodyColors[rand]);
        core.material.SetColor("_EmissionColor", coreColors[rand]);
        enemyProjectile.particleRenderer.material.SetColor("_EmissionColor", coreColors[rand]);
    }

    private void Deactivate()
    {
        player.GetComponent<PlayerShooting>().RemoveLockOn();
        this.gameObject.SetActive(false);
    }

    public Element GetElement()
    {
        return this.element;
    }

    public void SetPath(CinemachinePath path)
    {
        cart.m_Path = path;
    }
}
