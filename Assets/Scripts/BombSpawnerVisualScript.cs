using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerVisualScript : MonoBehaviour
{
    private const string CANON_SHOT = "CanonShoot";
    private Animator animator;
    [SerializeField] private BombSpawner bombSpawner;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bombSpawner = bombSpawner.GetComponent<BombSpawner>();
    }

    private void Start() 
    {
        bombSpawner.OnCanonShot += bombSpawner_OnCanonShot;
    }

    private void bombSpawner_OnCanonShot(object sender, EventArgs e)
    {
        animator.Play(CANON_SHOT);
    }
}
