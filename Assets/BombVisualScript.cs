using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombVisualScript : MonoBehaviour
{
    private const string BOMB_EXPLOSION = "BombExplosion";
    private Animator animator;
    [SerializeField] private BombBehaviour bombBehaviour;


    private void Awake() 
    {
        animator = GetComponent<Animator>();
        bombBehaviour = bombBehaviour.GetComponent<BombBehaviour>();
    }

    private void Start() 
    {
        bombBehaviour.OnBombDestroyed += BombBehaviour_OnBombDestroyed;
    }

    private void BombBehaviour_OnBombDestroyed(object sender, EventArgs e)
    {
        animator.Play(BOMB_EXPLOSION);
    }

    public void DestroySelf() 
    {
        bombBehaviour.DestroySelf();
    }
}
