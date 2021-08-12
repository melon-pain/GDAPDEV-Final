using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage = 5.0f;
    public ParticleSystem particles { get; private set; } = null;
    public ParticleSystemRenderer particleRenderer { get; private set; } = null;
    // Start is called before the first frame update
    private void Start()
    {
        particles = this.GetComponent<ParticleSystem>();
        particleRenderer = particles.GetComponent<ParticleSystemRenderer>();
        particles.Stop();
    }
    private void OnParticleCollision(GameObject other)
    {
        Player player = other.GetComponent<Player>();
        if (player)
        {
            player.TakeDamage(this.damage);
        }
    }
}
