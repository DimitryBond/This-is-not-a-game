using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using Unity.Mathematics;
using UnityEngine;

[SelectionBase] // чтобы всегда выбирать Player вместо Visual 
public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public event EventHandler OnPlayerDeath;
    public event EventHandler OnPlayerTakeHit;
    
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private float damageRecoveryTime = 0.5f;
    
    private Vector2 inputVector;
    
    private Rigidbody2D rb;
    private KnockBack knockBack;
    
    private float minMovingSpeed = 0.1f;
    private bool isRunning= false;
    
    private int currentHealth;
    private bool canTakeDamage = true;

    private bool isAlive = true; // можно так или лучше в start?

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        
    }
    
    private void Start()
    {
        // подписались на событие и вызываем метод при атаке
        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        
        currentHealth = maxHealth;
    }

    private void FixedUpdate()
    {
        if(knockBack.GettingKnockedBack)
        {
            return;
        }
        HandleMovement();
    }

    public bool IsAlive()
    {
        return isAlive;
    }

    private void GameInput_OnPlayerAttack(object sender, EventArgs e)
    {
        ActiveWeapon.Instance.GetActiveWeapon().Attack();
    }

    public void TakeDamage(Transform damageSourse, int damage)
    {
        if (canTakeDamage && isAlive)
        {
            OnPlayerTakeHit?.Invoke(this, EventArgs.Empty);
            canTakeDamage = false;
            currentHealth = Math.Max(0, currentHealth -= damage);
            Debug.Log(currentHealth);
            knockBack.GetKnockedBack(damageSourse);
            
            StartCoroutine(DamageRecoveryRoutine());
        }
        
        DetectDeath();
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0 && isAlive)
        {
            isAlive = false;
            knockBack.StopKnockBackMovement();
            GameInput.Instance.DisableMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update()
    { 
        inputVector = GameInput.Instance.GetMovementVector();
    }

    private void HandleMovement()
    {
        rb.MovePosition(rb.position + inputVector * (moveSpeed * Time.fixedDeltaTime));

        if (Math.Abs(inputVector.x) > minMovingSpeed || Math.Abs(inputVector.y) > minMovingSpeed)
        {
            isRunning = true;
        } else {
            isRunning = false;
        }
    }

    public bool IsRunning()
    {
        return isRunning;
    }

 

    public Vector3 GetPlayerScreenPosition()
    {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

        return playerScreenPosition;
    }
}
