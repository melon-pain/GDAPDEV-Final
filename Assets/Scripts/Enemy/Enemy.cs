using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region Stats
    [Header("Enemy Stats")]
    [SerializeField] private Element element;
    [SerializeField] private float HP = 100.0f;
    private bool isDead = false;
    #endregion Stats

    #region Shooting
    [Header("Shooting")]
    [SerializeField] private EnemyProjectile enemyProjectile;
    [SerializeField] private float maxShootTime = 2.0f;

    private bool isShooting = false;
    private float shootTime = 0.0f;
    private float shootInterval = 0.0f;
    #endregion Shooting

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

            this.transform.LookAt(player.transform);
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
                Deactivate();
            }
        }
    }

    private void nValidate()
    {
        Activate();
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
        this.gameObject.SetActive(false);
    }

    public Element GetElement()
    {
        return this.element;
    }
}
