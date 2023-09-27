using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlobAnimationScript : MonoBehaviour
{
    private const string IS_MOVING = "isMoving";
    [SerializeField] private EnemyAI enemy;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = enemy.gameObject.GetComponent<EnemyAI>();
    }

    private void Update()
    {
        SetMovingBool();
    }
    private void SetMovingBool()
    {
        animator.SetBool(IS_MOVING, enemy.isMoving());
    }
}
