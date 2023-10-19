using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootstepSound : MonoBehaviour
{
    private EnemyAI enemyAI;
    private AudioSource audioSource;
    private float footstepTimer;
    private float footstepTimerMax = 0.8f;
    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;


            if (enemyAI.isMoving())
            {

                audioSource.clip = SoundManager.Instance.ReturnAudioClipGameSO().enemyRun[Random.Range(0, SoundManager.Instance.ReturnAudioClipGameSO().enemyRun.Length)];
                audioSource.volume = SoundManager.Instance.ReturnGetVolume();
                audioSource.Play();

            }

        }
    }
}
