using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string IS_MOVING = "isMoving";
    private const string CAN_MOVE = "canMove";
    private const string PLAYER_RB_VELOCITY_Y = "playerRBvelocity.y";
    private const string PLAYER_RB_VELOCITY_X = "playerRBvelocity.x";
    private const string IS_RECIVING_INPUT = "isRecivingInput";
    private const string IS_IN_AIR = "isInAir";
    private const string IS_DASHING = "isDashing";
    private const string BRAVO_CHANGING_DIRECTION = "CHANGING_DIRECTION";
    private const string BRAVO_HIT = "gettingHit";
    private const string IS_HANGING = "isHanging";
    private const string BRAVO_DEATH_STROKED = "playerDeath";
    private const string BRAVO_EXIT_ANIMATION = "levelEnd";

    private Animator animator;
    [SerializeField] private ParticleSystem dustParticle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        animator.updateMode = AnimatorUpdateMode.Normal;
        Player.Instance.OnDirectionChanged += Player_Instance_OnDirectionChanged;
        PlayerDamage.Instance.OnGetDamaged += PlayerDamage_Instance_OnGetDamaged;
        PlayerDamage.Instance.OnPlayerDeath += PlayerDamage_Instance_OnPlayerDeath;
        GameManager.Instance.LevelEnding += GameManager_Instance_LevelEnding;
    }

    private void GameManager_Instance_LevelEnding(object sender, EventArgs e)
    {
        animator.SetTrigger(BRAVO_EXIT_ANIMATION);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    private void PlayerDamage_Instance_OnPlayerDeath(object sender, EventArgs e)
    {
        animator.SetTrigger(BRAVO_DEATH_STROKED);
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;

    }

    private void PlayerDamage_Instance_OnGetDamaged(object sender, EventArgs e)
    {
        animator.SetTrigger(BRAVO_HIT);
    }

    private void Player_Instance_OnDirectionChanged(object sender, EventArgs e)
    {
        EmitDust();
        if (Player.Instance.IsMoving())
        {
            animator.Play(BRAVO_CHANGING_DIRECTION);
        }

    }

    private void Update()
    {
        animator.SetBool(IS_MOVING, Player.Instance.IsMoving());
        animator.SetFloat(PLAYER_RB_VELOCITY_Y, Player.Instance.GetPlayerRB().velocity.y);
        animator.SetFloat(PLAYER_RB_VELOCITY_X, Mathf.Abs(Player.Instance.GetPlayerRB().velocity.x));
        animator.SetBool(IS_RECIVING_INPUT, Player.Instance.isRecivingInput());
        animator.SetBool(IS_IN_AIR, Player.Instance.GroundCheck());
        animator.SetBool(IS_DASHING, Player.Instance.GetIsDashing());
        animator.SetBool(IS_HANGING, Player.Instance.IsHoldingLedge());
        animator.SetBool(CAN_MOVE, Player.Instance.canMove);
    }

    private void EmitDust()
    {
        dustParticle.Play();
    }
}
