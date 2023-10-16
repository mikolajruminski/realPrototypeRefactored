using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlobAnimationScript : MonoBehaviour
{
    private const string IS_MOVING = "isMoving";
    private const string BLOB_DEATH = "BlobDeath";
    [SerializeField] private Transform enemyTransform;
    private EnemyAI enemyAI;
    private Enemy enemy;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = enemyTransform.GetComponent<Enemy>();
        enemyAI = enemyTransform.GetComponent<EnemyAI>();
    }
    void Start()
    {
        enemy.OnEnemyDeath += enemy_onEnemyDeath;
    }

    private void enemy_onEnemyDeath(object sender, EventArgs e)
    {
        animator.Play(BLOB_DEATH);
    }

    private void Update()
    {
        SetMovingBool();
    }
    private void SetMovingBool()
    {
        animator.SetBool(IS_MOVING, enemyAI.isMoving());
    }
}
