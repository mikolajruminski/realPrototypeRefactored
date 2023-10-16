using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnDirectionChanged;
    [SerializeField] private GameInput gameInput;
    private Rigidbody2D playerRB;

    [Header("Movement")]
    [SerializeField] private float targetSpeed = 7f;
    [SerializeField] private float playerAcceleration = 0.25f;
    [SerializeField] private float playerDeacceleratin = 0.25f;
    private float currentSpeed;

    [Header("Dashing")]
    [SerializeField] private float dashMultiplier = 2f;
    [SerializeField] private float dashTimeDuration = 0.5f;
    [SerializeField] private float dashTimeCooldown = 2f;

    [Header("Layer Masks + Interactable Check")]

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform interactableCheck;
    [SerializeField] private LayerMask ladderMask;

    [Header("GroundCheck size")]
    [SerializeField] private Vector2 groundCheckSize = Vector2.zero;
    [SerializeField] Transform groundCheck;

    [Header("Jumping")]
    private float jumpingForce = 10f;
    private float fallMultiplier = 4f;
    private float playerGravity;

    [Header("Knockbacking")]
    [SerializeField] private float knockbackForce = 15f;
    private bool isKnockbacking;

    //interactable variables
    private float distance;
    private float raycastDistance;
    private RaycastHit2D lastRaycastHit2D;

    //
    private bool canDoubleJump = false;
    private bool canDash = true;
    private bool isDashing;
    public bool canMove;
    private bool isFacingRight = true;
    private bool isGoingRight = true;
    private bool isHoldingLedge = false;

    private Vector2 lastMovingDir;
    private IInteractable lastInteractable;


    [Header("Climbing variables")]

    [HideInInspector] public bool ledgeDetection;
    [SerializeField] private Vector3 ledgePositionOffset;
    private bool canClimb = false;
    private Vector2 climbingPosition;

    //Knockup effect from stomping an enemy, script in the "Head Stomper" component
    private void Awake()
    {
        Instance = this;
        gameInput = gameInput.GetComponent<GameInput>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        canMove = true;
        playerGravity = playerRB.gravityScale;
        gameInput.OnJumpPerformed += gameInput_OnJumpPerformed;
        gameInput.OnJumpReleased += gameInput_OnJumpReleased;
        gameInput.OnDashPerformed += gameInput_OnDashPerformed;
        gameInput.OnInteractPerformed += gameInput_onInteractPerformed;
    }

    private void Update()
    {
        if (isKnockbacking || isKnockbacking || !GameManager.Instance.GetIsGameActive())
        {
            return;
        }

        DoubleJumpReset();
        FlipSprite();
        HandleInteraction();
        CheckForLedge();
    }

    void FixedUpdate()
    {
        if (isDashing || isKnockbacking || !GameManager.Instance.GetIsGameActive())
        {
            return;
        }

        HandleMovememnt();
        BetterJumping();
    }

    //Interacting

    private void gameInput_onInteractPerformed(object sender, EventArgs e)
    {
        if (lastInteractable != null)
        {
            if (lastInteractable.isInteractable && lastInteractable.isActive)
            {
                lastInteractable.Interact();
            }
        }

    }

    private void HandleInteraction()
    {
        RaycastHit2D raycastHit2D = RayCastInteractable();

        if (lastRaycastHit2D.collider != null)
        {
            distance = Vector3.Distance(transform.position, lastRaycastHit2D.collider.gameObject.transform.position);
        }

        if (raycastHit2D.collider != null && raycastHit2D.collider.TryGetComponent(out IInteractable interactable))
        {
            lastInteractable = interactable;
            if (lastInteractable.isActive)
            {
                lastInteractable.ShowInteractableButton();
            }

            lastRaycastHit2D = raycastHit2D;
        }

        if (raycastHit2D.collider == null && lastInteractable != null)
        {
            lastInteractable.HideInteractableButton();
        }
        /*
                if (lastInteractable != null)
                {
                    if (lastRaycastHit2D.collider.TryGetComponent(out IInteractable interactable1))
                    {
                        if (interactable1 == lastInteractable)
                        {
                            lastInteractable.ShowInteractableButton();
                        }
                        else
                        {
                            lastInteractable.HideInteractableButton();
                        }
                    }
                    else
                    {
                        lastInteractable.HideInteractableButton();
                    }

                }
        */

        /*
                if (raycastHit2D.collider != null && raycastHit2D.collider.TryGetComponent(out IInteractable interactable))
                {
                    lastInteractable = interactable;
                    lastRaycastHit2D = raycastHit2D;

                    if (distance < minInteractDistance && lastInteractable.isActive)
                    {
                        lastInteractable.ShowInteractableButton();
                    }


                }
                if (distance != 0)
                {
                    if (distance > maxInteractDistance)
                    {
                        lastInteractable.HideInteractableButton();
                    }
                }


                if (lastInteractable != null && raycastHit2D == true)
                {
                    if (lastInteractable != raycastHit2D.collider.GetComponent<IInteractable>())
                    {
                        lastInteractable.HideInteractableButton();
                    }
                }

        */
    }

    private RaycastHit2D RayCastInteractable()
    {
        Vector2 moveVector = gameInput.GetMovementVector();
        raycastDistance = 3f;

        Vector2 moveDir = new Vector2(moveVector.x, moveVector.y);

        if (moveDir != Vector2.zero)
        {
            lastMovingDir = moveDir;
        }

        RaycastHit2D raycastHit2D = Physics2D.Raycast(interactableCheck.transform.position, lastMovingDir, raycastDistance);
        Debug.DrawRay(interactableCheck.transform.position, lastMovingDir * raycastDistance, Color.red);

        return raycastHit2D;
    }


    //movement section
    private void HandleMovememnt()
    {
        if (canMove)
        {
            Vector2 moveVector = gameInput.GetMovementVector();

            Vector2 moveDir = new Vector2(moveVector.x, moveVector.y);

            if (moveDir.x > 0)
            {
                isGoingRight = true;
            }
            if (moveDir.x < 0)
            {
                isGoingRight = false;
            }

            currentSpeed = playerRB.velocity.x;

            if (currentSpeed < targetSpeed && isGoingRight)
            {
                float x = currentSpeed + playerAcceleration;

                playerRB.velocity = new Vector2(moveDir.x * x, playerRB.velocity.y);
            }
            if (currentSpeed > -targetSpeed && !isGoingRight)
            {
                float x = -currentSpeed + playerAcceleration;
                playerRB.velocity = new Vector2(moveDir.x * x, playerRB.velocity.y);
            }

            if (moveDir == Vector2.zero && currentSpeed > 0 && isGoingRight)
            {
                playerRB.velocity = new Vector2(currentSpeed - playerDeacceleratin, playerRB.velocity.y);

            }
            if (moveDir == Vector2.zero && currentSpeed < 0 && !isGoingRight)
            {
                playerRB.velocity = new Vector2(currentSpeed + playerDeacceleratin, playerRB.velocity.y);
            }
        }
    }

    //Jumping section
    private void gameInput_OnJumpPerformed(object sender, EventArgs e)
    {
        if (GroundCheck())
        {
            playerRB.velocity = Vector2.up * jumpingForce;
        }

        if (playerRB.velocity.y != 0 && canDoubleJump)
        {
            playerRB.velocity = Vector2.up * jumpingForce;
            canDoubleJump = false;
        }

        if (canClimb)
        {
            canClimb = false;
            playerRB.velocity = Vector2.up * jumpingForce;
            canDoubleJump = true;
        }

    }

    private void gameInput_OnJumpReleased(object sender, EventArgs e)
    {
        if (playerRB.velocity.y > 0)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
        }
    }

    private void BetterJumping()
    {
        if (playerRB.velocity.y < 0 && !GroundCheck())
        {
            playerRB.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    //dashing

    private void gameInput_OnDashPerformed(object sender, EventArgs e)
    {
        if (canDash && playerRB.velocity.x != 0)
        {
            StartCoroutine(PerformDash());
        }

    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        canDash = false;

        playerRB.gravityScale = 0;
        if (playerRB.velocity.x > 0)
        {
            playerRB.velocity = new Vector2(1 * dashMultiplier, 0f);
        }
        if (playerRB.velocity.x < 0)
        {
            playerRB.velocity = new Vector2(-1 * dashMultiplier, 0f);
        }


        yield return new WaitForSeconds(dashTimeDuration);

        playerRB.gravityScale = playerGravity;
        isDashing = false;
        playerRB.velocity = new Vector2(playerRB.velocity.x * 0.5f, playerRB.velocity.y);

        yield return new WaitForSeconds(dashTimeCooldown);

        canDash = true;
    }

    //ledge detection

    private void CheckForLedge()
    {
        if (ledgeDetection && isFacingRight)
        {
            climbingPosition = GetComponentInChildren<LedgeDetection>().transform.position + ledgePositionOffset;
            canClimb = true;
        }

        if (ledgeDetection && !isFacingRight)
        {
            climbingPosition = GetComponentInChildren<LedgeDetection>().transform.position + new Vector3(-ledgePositionOffset.x, ledgePositionOffset.y);
            canClimb = true;
        }

        if (canClimb)
        {
            canDash = false;
            isHoldingLedge = true;
            transform.position = climbingPosition;
            playerRB.velocity = new Vector2(0, 0);
        }
        if (!canClimb)
        {
            isHoldingLedge = false;
            canDash = true;
        }

    }

    public bool IsHoldingLedge()
    {
        return isHoldingLedge;
    }

    //random functions
    public bool IsMoving()
    {
        return playerRB.velocity.x != 0;
    }
    public bool isRecivingInput()
    {
        return gameInput.GetMovementVector() != Vector2.zero;
    }

    public void ClearLastInteractable()
    {
        lastInteractable = null;
    }
    private void FlipSprite()
    {
        Vector2 moveVector = gameInput.GetMovementVector();


        if (moveVector.x > 0 && !isFacingRight && !canClimb)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
            OnDirectionChanged?.Invoke(this, EventArgs.Empty);

            isFacingRight = !isFacingRight;
        }
        if (moveVector.x < 0 && isFacingRight && !canClimb)
        {
            Vector3 currentScale = gameObject.transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
            OnDirectionChanged?.Invoke(this, EventArgs.Empty);

            isFacingRight = !isFacingRight;
        }
    }
    public bool GroundCheck()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0, groundLayerMask);
    }

    private void DoubleJumpReset()
    {
        if (GroundCheck() && canDoubleJump == false)
        {
            canDoubleJump = true;
        }
    }

    public Rigidbody2D GetPlayerRB()
    {
        return playerRB;
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(groundCheck.position, groundCheckSize);
    }

    private IEnumerator knockbackEffect(Vector2 distance)
    {
        canMove = false;
        isKnockbacking = true;
        playerRB.gravityScale = 0;
        playerRB.AddForce(distance, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.4f);
        playerRB.gravityScale = playerGravity;
        playerRB.velocity = new Vector2(playerRB.velocity.x * 0.5f, playerRB.velocity.y);
        isKnockbacking = false;
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            if (PlayerDamage.Instance.CanGetDamaged())
            {
                PlayerDamage.Instance.Damage(other.gameObject.GetComponent<Enemy>().GetDamege());
                Vector2 distance = new Vector2(transform.position.x - other.transform.position.x, 0).normalized * knockbackForce;
                StartCoroutine(knockbackEffect(distance));
            }
            else
            {
                Vector2 distance = new Vector2(transform.position.x - other.transform.position.x, 0).normalized * knockbackForce;
                StartCoroutine(knockbackEffect(distance));
            }

        }
    }
}
