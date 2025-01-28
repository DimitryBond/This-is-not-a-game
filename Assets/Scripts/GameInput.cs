using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;


public class GameInput : MonoBehaviour
{
    public static GameInput Instance {get; private set; }
    private PlayerInputActions playerInputActions;

    public event EventHandler OnPlayerAttack;

    private void Awake()
    {
        Instance = this;
        
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        // вызов события при нажатии ЛКМ
        playerInputActions.Combat.Attack.started += PlayerAttack_started;
    }

    private void PlayerAttack_started(InputAction.CallbackContext context)
    {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        return inputVector;
    } 

       public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();

        return mousePosition;
    }

    public void DisableMovement()
    {
        playerInputActions.Disable();
    }

}
