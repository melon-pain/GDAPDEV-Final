using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] bgm;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void PlayBGMIndex(int index)
    {
        if(audioSource != null)
        {
            if (index < bgm.Length && index > 0)
            {
                audioSource.clip = bgm[index];
                audioSource.Play();
            }
        }
    }
}
