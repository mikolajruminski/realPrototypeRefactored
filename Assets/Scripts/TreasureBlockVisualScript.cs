using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBlockVisualScript : MonoBehaviour
{
    private const string DESTROY_SELF_ANIMATION = "TreasureBlockDestroy";
    [SerializeField] private TreasureBlockScript treasureBlockScript;
    private Animator animator;

    private void Awake()
    {
        treasureBlockScript = treasureBlockScript.GetComponent<TreasureBlockScript>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        treasureBlockScript.OnTriggerWithPlayer += TreasureBlockScript_OnTriggerWithPlayer;
    }

    private void TreasureBlockScript_OnTriggerWithPlayer(object sender, EventArgs e)
    {
        animator.Play(DESTROY_SELF_ANIMATION);
    }

    public void SelfDestroy()
    {
        treasureBlockScript.DestroySelf();
    }
}
