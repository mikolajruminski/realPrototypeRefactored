using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnJumpPerformed;
    public event EventHandler OnJumpReleased;
    public event EventHandler OnDashPerformed;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Movement.Enable();
        playerInputActions.Movement.Jump.performed += playerInputActions_Movement_Jump_performed;
        playerInputActions.Movement.Jump.canceled += playerInputActions_Movement_Jump_canceled;
        playerInputActions.Movement.Dash.performed += playerInputActions_Movement_Dash_performed;
    }

    private void playerInputActions_Movement_Jump_performed(InputAction.CallbackContext callbackContext)
    {
        OnJumpPerformed?.Invoke(this, EventArgs.Empty);
    }

    private void playerInputActions_Movement_Jump_canceled(InputAction.CallbackContext callbackContext)
    {
        OnJumpReleased?.Invoke(this, EventArgs.Empty);
    }

    private void playerInputActions_Movement_Dash_performed(InputAction.CallbackContext callbackContext)
    {
        OnDashPerformed?.Invoke(this, EventArgs.Empty);
    }


    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Movement.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
