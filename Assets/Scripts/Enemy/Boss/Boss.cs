using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Boss : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float HP = 1000.0f;

    [Header("Plates")]
    [SerializeField] private List<GameObject> plates = new List<GameObject>();

    [Header("Shooting")]
    [SerializeField] private EnemyProjectile enemyProjectile;
    [SerializeField] private float maxShootTime = 2.0f;
    private bool isShooting = false;
    private float shootTime = 0.0f;
    private float shootInterval = 0.0f;

    [Header("Movement")]
    public CinemachineDollyCart cart;
    [HideInInspector] public float cartPos = 0.5f;
    public bool isMoving { get; private set; } = true;

    private GameObject player; //Player reference
    private Animator animator;

    public bool isDead { get; private set; } = false;
    private EnemySFX sfx;
    public UnityEvent OnDeath;

    // Start is called before the first frame update
    private void Start()
    {
        shootInterval = Random.Range(1.0f, 2.0f);
        player = GameObject.FindGameObjectWithTag("Player");
        animator = this.GetComponent<Animator>();
        sfx = this.GetComponent<EnemySFX>();

        foreach (GameObject plate in plates)
        {
            BossPlate bossPlate = plate.GetComponent<BossPlate>();
            bossPlate.OnTakeDamage.AddListener(TakeDamage);
        }
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

    public void TakeDamage(float amount)
    {
        HP -= amount;
        sfx.PlayDamaged();
        if (HP <= 0.0f)
        {
            isDead = true;
            isShooting = false;
            if (GameManager_Level.Instance != null)
            {
                GameManager_Level.AddBossKillCount();
            }
            OnDeath.Invoke();
            Destroy(this.gameObject);
        }
    }

    public void SetPath(CinemachinePath path)
    {
        cart.m_Path = path;
    }
}
