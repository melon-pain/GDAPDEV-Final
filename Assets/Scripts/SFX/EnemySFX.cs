using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySFX : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("SFX")]
    [SerializeField] AudioClip[] damaged;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void PlayDamaged()
    {
        if (damaged.Length > 0)
            audioSource.clip = damaged[Random.Range(0, damaged.Length)];
        audioSource.Play();
    }
}
