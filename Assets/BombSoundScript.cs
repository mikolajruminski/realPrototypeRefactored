using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSoundScript : MonoBehaviour
{
    private BombBehaviour bombBehaviour;
    private AudioSource audioSource;

    private void Awake() 
    {
        bombBehaviour = GetComponent<BombBehaviour>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() 
    {
        bombBehaviour.OnBombDestroyed += BombBehaviour_OnBombDestroyed;
    }

    private void BombBehaviour_OnBombDestroyed(object sender, EventArgs e)
    {
        audioSource.clip = SoundManager.Instance.ReturnAudioClipGameSO().bombExplosion[0];
        audioSource.volume = SoundManager.Instance.ReturnGetVolume();
        audioSource.Play();
    }
}
