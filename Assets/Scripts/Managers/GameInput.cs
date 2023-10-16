using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    public event EventHandler OnJumpPerformed;
    public event EventHandler OnJumpReleased;
    public event EventHandler OnDashPerformed;
    public event EventHandler OnInteractPerformed;
    public event EventHandler OnMovementReleased;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
        playerInputActions.Movement.Enable();
        playerInputActions.Movement.Move.canceled += playerInputActions_Movement_Move_cancelled;
        playerInputActions.Movement.Jump.performed += playerInputActions_Movement_Jump_performed;
        playerInputActions.Movement.Jump.canceled += playerInputActions_Movement_Jump_canceled;
        playerInputActions.Movement.Dash.performed += playerInputActions_Movement_Dash_performed;
        playerInputActions.Movement.Interact.performed += playerInputActions_Movement_Interact_performed;
    }

    private void playerInputActions_Movement_Jump_performed(InputAction.CallbackContext callbackContext)
    {
        OnJumpPerformed?.Invoke(this, EventArgs.Empty);
    }
    private void playerInputActions_Movement_Move_cancelled(InputAction.CallbackContext callbackContext)
    {
        OnMovementReleased?.Invoke(this, EventArgs.Empty);
    }

    private void playerInputActions_Movement_Interact_performed(InputAction.CallbackContext callbackContext)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
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
