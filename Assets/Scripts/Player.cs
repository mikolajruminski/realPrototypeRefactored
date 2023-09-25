using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private GameInput gameInput;
    private Rigidbody2D playerRB;


    [SerializeField] private float speed = 7f;
    [SerializeField] private float dashMultiplier = 15f;
    [SerializeField] private float dashTimeDuration = 1f;
    [SerializeField] private float dashTimeCooldown = 2f;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ladderMask;


    private float jumpingForce = 10f;
    private float fallMultiplier = 4f;
    private float lowJumpMultiplier = 2f;
    private float ladderSpeed = 8f;
    private float playerGravity;

    private float vertical;

    private bool canDoubleJump = false;
    private bool canDash = true;
    private bool isDashing;
    private bool isClimbing;
    private bool isLadder;

    private void Awake()
    {
        Instance = this;
        gameInput = gameInput.GetComponent<GameInput>();
        playerRB = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerGravity = playerRB.gravityScale;
        gameInput.OnJumpPerformed += gameInput_OnJumpPerformed;
        gameInput.OnJumpReleased += gameInput_OnJumpReleased;
        gameInput.OnDashPerformed += gameInput_OnDashPerformed;
    }

    private void Update()
    {
        DoubleJumpReset();
        LadderDetection();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        HandleMovememnt();
        BetterJumping();
        LadderMovement();

    }

    //movement section
    private void HandleMovememnt()
    {
        Vector2 moveVector = gameInput.GetMovementVector();

        Vector2 moveDir = new Vector2(moveVector.x, moveVector.y);


        playerRB.velocity = new Vector2(moveDir.x * speed, playerRB.velocity.y);
    }

    private void LadderMovement()
    {
        if (isClimbing)
        {
            playerRB.gravityScale = 0f;
            playerRB.velocity = new Vector2(playerRB.velocity.x, vertical * ladderSpeed);
        }
        else
        {
            playerRB.gravityScale = playerGravity;
        }

    }

    private void LadderDetection()
    {
        float distance = 0.2f;
        isLadder = Physics2D.Raycast(transform.position, Vector2.up, distance, ladderMask);

        Vector2 moveVector = gameInput.GetMovementVector();
        vertical = moveVector.y;

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
        if (!isLadder)
        {
            isClimbing = false;
        }

    }

    //Jumping section
    private void gameInput_OnJumpPerformed(object sender, EventArgs e)
    {
        if (GroundCheck())
        {
            playerRB.velocity = Vector2.up * jumpingForce;
        }

        if (!GroundCheck() && canDoubleJump)
        {
            playerRB.velocity = Vector2.up * jumpingForce;
            canDoubleJump = false;
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
        if (canDash)
        {
            StartCoroutine(PerformDash());
        }

    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        canDash = false;

        playerRB.gravityScale = 0;
        playerRB.velocity = new Vector2(playerRB.velocity.x * dashMultiplier, 0f);

        yield return new WaitForSeconds(dashTimeDuration);

        playerRB.gravityScale = playerGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashTimeCooldown);
        canDash = true;
        Debug.Log("DashEnd");
    }


    private bool GroundCheck()
    {
        float groundCheckRadius = 0.2f;
        return Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayerMask);
    }

    private void DoubleJumpReset()
    {
        if (GroundCheck())
        {
            canDoubleJump = true;
        }
    }
}
