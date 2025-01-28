using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private float health;
    private bool isAlive;
    private Vector2 inputVector;
    
    public float MoveSpeed { get; set; }
    public bool IsRunning { get; set; } = true;
    public bool CanTakeDamage { get; set; } = true;
    
    public int CurrentHealth { get; set; } = 10;
    public float DamageRecoveryTime { get; set; } = 0.5f;
    

    public event Action<float> OnHealthChanged;
    public event Action OnAliveChanged;
    public event Action<Vector2> OnInputVectorChanged;
    
    public float MaxHealth { get; set; }
    
    public float Health
    {
        get => health;
        set
        {
            health = value > MaxHealth ? MaxHealth : value;
            OnHealthChanged?.Invoke(health);
        }
    }
    
    public bool IsAlive
    {
        get => isAlive;
        set
        {
            isAlive = value;
            OnAliveChanged?.Invoke();
        }
    }
    
    public Vector2 InputVector
    {
        get => inputVector;
        set
        {
            inputVector = value;
            OnInputVectorChanged?.Invoke(inputVector);
        }
    }
    
    
    public event Action<Vector2> OnScreenPositionChanged;
    private Vector2 screenPosition;
    public Vector2 ScreenPosition
    {
        get => screenPosition;
        set
        {
            screenPosition = value;
            OnScreenPositionChanged?.Invoke(screenPosition);
        }
    }
}