using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSounds : MonoBehaviour
{
    private Player player;
    private float footstepTimer;
    private float footstepTimerMax = .3f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if (footstepTimer < 0f)
        {
            footstepTimer = footstepTimerMax;


            if (player.IsMoving() && player.GroundCheck())
            {
                float volume = 1.5f;
                SoundManager.Instance.PlayFootstepSound(player.transform.position, volume);
            }

        }
    }
}
