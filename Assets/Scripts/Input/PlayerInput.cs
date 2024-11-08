using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput
{
    public event Action<int> OnScroll;

    public event Action<bool> OnRun;
    public event Action<bool> OnCrouch;

    public event Action OnJump;

    private readonly DefaultInputActions inputActions;

    public Vector2 Move => inputActions.Player.Move.ReadValue<Vector2>();
    public Vector2 Look => inputActions.Player.Look.ReadValue<Vector2>();

    public bool IsEnabled { get; private set; }

    public PlayerInput()
    {
        inputActions = new DefaultInputActions();
        inputActions.Player.Run.performed += OnRunPerformed;
        inputActions.Player.Run.canceled += OnRunCanceled;

        inputActions.Player.Crouch.performed += OnCrouchPerformed;
        inputActions.Player.Crouch.canceled += OnCrouchCanceled;

        inputActions.Player.Jump.performed += OnJumpPerformed;

        Enable();
    }

    public void Enable()
    {
        IsEnabled = true;
        inputActions.Enable();
    }

    public void Disable()
    {
        IsEnabled = false;
        inputActions.Disable();
    }

    public void Update()
    {

    }

    private void OnRunPerformed(InputAction.CallbackContext context)
    {
        OnRun?.Invoke(true);
    }

    private void OnRunCanceled(InputAction.CallbackContext context)
    {
        OnRun?.Invoke(false);
    }

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        OnCrouch?.Invoke(true);
    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        OnCrouch?.Invoke(false);
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}