using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector2 startingPosition;

    [SerializeField] private Color gizmoIdleColor;
    [SerializeField] private Color gizmoDetectedColor;
    [SerializeField] private Vector2 detectorSize = Vector2.one;
    [SerializeField] private Vector2 detectorOriginOffset = Vector2.zero;
    [SerializeField] private LayerMask playerLayerMask;
    private Player player;
    private Rigidbody2D enemyRB;
    private Enemy enemy;

    private bool awareOfPlayer;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        startingPosition = transform.position;
        enemyRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerDetectionBox();
    }
    private void PlayerDetectionBox()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)transform.position + detectorOriginOffset, detectorSize, 0, playerLayerMask);
        if (collider != null)

        {
            if (collider.TryGetComponent<Player>(out Player player))
            {
                awareOfPlayer = true;
                ChasePlayer(player);
            }
        }
        else
        {
            awareOfPlayer = false;
            ChasePlayer(null);
        }
    }

    private void ChasePlayer(Player player)
    {
        this.player = player;

        if (player != null)
        {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            enemyRB.velocity = new Vector2(directionToPlayer.x, 0) * enemy.GetSpeed();
        }
        else
        {
            enemyRB.velocity = Vector2.zero;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoIdleColor;

        if (awareOfPlayer)
        {
            Gizmos.color = gizmoDetectedColor;
        }
        else
        {
            Gizmos.color = gizmoIdleColor;
        }

        Gizmos.DrawCube((Vector2)transform.position + detectorOriginOffset, detectorSize);

    }
}
