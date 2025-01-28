using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    
    private void Awake()
    {
        Instance = this;
    }


    public Vector2 GetMousePosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        return mousePosition;
    }
}