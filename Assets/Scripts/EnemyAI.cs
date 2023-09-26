using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private Transform boundaryX, boundaryX2;
    private Vector2 boundaryXPosition, boundaryX2Position, startingPosition;
    private int dirX;

    private enum State
    {
        ChangingDirection,
        Patrolling,
        Detected,
        Chasing,
    }
    private State state;

    [SerializeField] private Color gizmoIdleColor;
    [SerializeField] private Color gizmoDetectedColor;
    [SerializeField] private Vector2 detectorSize = Vector2.one;
    [SerializeField] private Vector2 detectorOriginOffset = Vector2.zero;
    [SerializeField] private LayerMask playerLayerMask;

    private Rigidbody2D enemyRB;
    private Enemy enemy;
    private Player detectedPlayer;

    private bool awareOfPlayer;
    private bool canDetectDirectionChange;
    private bool canMove;

    private void Awake()
    {
        state = State.Patrolling;
    }

    private void Start()
    {
        canDetectDirectionChange = true;
        canMove = true;
        SetBoundaries();

        dirX = RandomDirX();
        enemy = GetComponent<Enemy>();
        enemyRB = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.ChangingDirection:
                Debug.Log(state);
                StartCoroutine(WaitAndChangeDirection());
                state = State.Patrolling;
                break;
            case State.Patrolling:
                Debug.Log(state);
                EnemyMovement();
                ChangeDirectionWhenApproachingBoundaries();
                PlayerDetectionBox();

                if (detectedPlayer != null)
                {
                    state = State.Detected;
                }

                break;
            case State.Detected:

                StartCoroutine(WaitAndResumeMovement());
                state = State.Chasing;
                break;

            case State.Chasing:
                PlayerDetectionBox();
                ChasePlayer(detectedPlayer);
                break;


        }
    }
    private void PlayerDetectionBox()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)transform.position + detectorOriginOffset, detectorSize, 0, playerLayerMask);
        if (collider != null)

        {
            if (collider.TryGetComponent<Player>(out Player player))
            {
                awareOfPlayer = true;
                detectedPlayer = player;
            }
        }
        else
        {
            awareOfPlayer = false;
            detectedPlayer = null;
        }
    }

    private void ChasePlayer(Player player)
    {
        if (player != null)
        {
            if (canMove)
            {
                Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
                enemyRB.velocity = new Vector2(directionToPlayer.x, 0) * enemy.GetSpeed();

                ChangeDirectionWhenApproachingBoundaries();
            }

        }
        else
        {
            state = State.Patrolling;
        }
    }

    private void EnemyMovement()
    {
        if (canMove)
        {
            enemyRB.velocity = new Vector2(dirX * enemy.GetSpeed(), enemyRB.velocity.y);
        }
    }

    private int RandomDirX()
    {
        int x = Random.Range(-1, 2);

        if (x != 0)
        {
            return x;
        }
        else
        {
            return RandomDirX();
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

    private void SetBoundaries()
    {
        startingPosition = transform.position;
        boundaryXPosition = boundaryX.transform.position;
        boundaryX2Position = boundaryX2.transform.position;
    }
    private void ChangeDirectionWhenApproachingBoundaries()
    {
        if (transform.position.x < (boundaryXPosition.x + startingPosition.x) && canDetectDirectionChange)
        {
            state = State.ChangingDirection;
        }

        if (transform.position.x > (boundaryX2Position.x - startingPosition.x) && canDetectDirectionChange)
        {
            state = State.ChangingDirection;
        }
    }
    private IEnumerator WaitAndChangeDirection()
    {
        canDetectDirectionChange = false;
        dirX *= -1;
        yield return new WaitForSeconds(0.5f);
        canDetectDirectionChange = true;
    }

    private IEnumerator WaitAndResumeMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }


}
