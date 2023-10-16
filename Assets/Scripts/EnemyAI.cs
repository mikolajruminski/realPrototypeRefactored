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
        BackIntoBoundaries,
        ChangingDirection,
        Patrolling,
        Detected,
        Chasing,
        ChaseEnded,
    }
    private State state;

    [SerializeField] private Color gizmoIdleColor;
    [SerializeField] private Color gizmoDetectedColor;
    [SerializeField] private Vector2 detectorSize = Vector2.one;
    [SerializeField] private Vector2 detectorOriginOffset = Vector2.zero;
    [SerializeField] private LayerMask playerLayerMask, groundLayerMask;
    [SerializeField] private Transform groundCheck;


    private Rigidbody2D enemyRB;
    private Enemy enemy;
    private Player detectedPlayer;

    private bool awareOfPlayer;
    private bool canDetectDirectionChange;
    private bool canMove;
    private bool isFacingRight = true;

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

    private void Update()
    {
        if (GameManager.Instance.GetIsGameActive())
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }
    }

    private void FixedUpdate()
    {
        FlipSprite();

        switch (state)
        {
            case State.BackIntoBoundaries:

                Vector2 boundaryOffset = new Vector2(3, 0);
                if (Vector2.Distance(transform.position, boundaryX2Position) < Vector2.Distance(transform.position, boundaryXPosition))
                {
                    transform.position = Vector2.MoveTowards(transform.position, boundaryX2Position - boundaryOffset, enemy.GetSpeed() * Time.deltaTime);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, boundaryXPosition + boundaryOffset, enemy.GetSpeed() * Time.deltaTime);
                }

                if (IsInBoundaries())
                {
                    state = State.Patrolling;
                    dirX *= -1;
                }
                break;
            case State.ChangingDirection:
                StartCoroutine(WaitAndChangeDirection());
                state = State.Patrolling;
                break;
            case State.Patrolling:
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
            case State.ChaseEnded:
                GetBackIntoBoundaries();
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
            if (canMove && GroundCheck())
            {
                float speedChaseMultiplier = 1.5f;
                Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
                enemyRB.velocity = new Vector2(directionToPlayer.x, 0) * enemy.GetSpeed() * speedChaseMultiplier;

            }

            if (!GroundCheck())
            {
                state = State.ChaseEnded;
            }

        }
        else
        {
            state = State.ChaseEnded;
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
        if (transform.position.x < boundaryXPosition.x && canDetectDirectionChange)
        {
            state = State.ChangingDirection;
        }

        if (transform.position.x > boundaryX2Position.x && canDetectDirectionChange)
        {
            state = State.ChangingDirection;
        }
    }

    private void GetBackIntoBoundaries()
    {
        if (transform.position.x > boundaryX2Position.x)
        {
            state = State.BackIntoBoundaries;
        }
        else if (transform.position.x < boundaryXPosition.x)
        {
            state = State.BackIntoBoundaries;
        }
        else
        {
            state = State.Patrolling;
        }
    }

    private bool IsInBoundaries()
    {
        return (transform.position.x < boundaryX2Position.x && transform.position.x > boundaryXPosition.x);
    }
    private bool GroundCheck()
    {
        float groundCheckDetectionRadius = 0.2f;
        return Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckDetectionRadius, groundLayerMask);
    }

    public bool isMoving()
    {
        return enemyRB.velocity.x != 0;
    }

    private void FlipSprite()
    {
        if (dirX > 0 && isMoving() && !isFacingRight)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            isFacingRight = !isFacingRight;
        }
        if (dirX < 0 && isMoving() && isFacingRight)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;

            isFacingRight = !isFacingRight;
        }
    }
    private IEnumerator WaitAndChangeDirection()
    {
        int minWaitTime = 1;
        int maxWaitTime = 4;
        canMove = false;
        canDetectDirectionChange = false;
        yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        dirX *= -1;
        canMove = true;
        yield return new WaitForSeconds(0.5f);
        canDetectDirectionChange = true;
    }

    private IEnumerator WaitAndResumeMovement()
    {
        canMove = false;
        if (enemyRB.velocity.x > 0)
        {
            Vector3 vel = enemyRB.velocity;
            float velX = vel.x;
            velX -= Time.deltaTime;

            enemyRB.velocity = vel;
            yield return null;

        }

        yield return new WaitForSeconds(1f);
        canMove = true;
    }

}
