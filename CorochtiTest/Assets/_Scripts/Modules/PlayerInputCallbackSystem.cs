using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputCallbackSystem
{
    #region Component Configs

    private SystemState state;
    private InputManager inputManager;
    
    public event Action<Vector2> OnMove; 
    public event Action<bool> OnJump;

    private bool isPress;

    #endregion

    #region Properties

    public SystemState State
    {
        get => state;
        set
        {
            state = value;
            switch (state)
            {
                case SystemState.Disable:
                    OnMove?.Invoke(Vector2.zero);
                    isPress = false;
                    Disable();
                    break;
                case SystemState.Enable:
                    Enable();
                    break;
            }
        }
    }

    #endregion
    public PlayerInputCallbackSystem()
    {
        inputManager = new InputManager();
        Disable();
    }

    private void Movement(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(context.ReadValue<Vector2>());
    }

    private void Jump(InputAction.CallbackContext context)
    {
        isPress = !isPress;
        OnJump?.Invoke(isPress);
    }

    private void Disable()
    {
        inputManager.Disable();
        inputManager.Player.Movement.performed -= Movement;
        inputManager.Player.Jump.performed -= Jump;
    }

    private void Enable()
    {
        inputManager.Enable();
        inputManager.Player.Movement.performed += Movement;
        inputManager.Player.Jump.performed += Jump;
    }
}
