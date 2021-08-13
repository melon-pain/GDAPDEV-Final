using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("SFX")]
    [SerializeField] AudioClip[] singleFire;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void PlaySingleFire()
    {
        if(singleFire.Length > 0)
            audioSource.clip = singleFire[Random.Range(0, singleFire.Length)];
        audioSource.Play();
    }
}
