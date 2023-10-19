using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerSoundScript : MonoBehaviour
{
    private AudioSource audioSource;
    private BombSpawner bombSpawner;

    private void Awake()
    {
        bombSpawner = GetComponent<BombSpawner>();
        audioSource = GetComponent<AudioSource>();

    }

    void Start()
    {
        bombSpawner.OnCanonShot += BombSpawner_OnCanonShot;
    }

    private void BombSpawner_OnCanonShot(object sender, EventArgs e)
    {
        audioSource.clip = SoundManager.Instance.ReturnAudioClipGameSO().launchBomb[0];
        audioSource.volume = SoundManager.Instance.ReturnGetVolume() * 0.5f;
        audioSource.Play();
    }


}
