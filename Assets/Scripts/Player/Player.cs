using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[SelectionBase] // чтобы всегда выбирать Player вместо Visual 
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public readonly PlayerData PlayerData = new();
    
    [SerializeField] private PlayerSO playerSo;
    
    // private int currentHealth;
    // private bool canTakeDamage = true;
    
    
    private readonly float minMovingSpeed = 0.1f;
    
    private Vector2 inputVector;
    private Rigidbody2D rb;
    private KnockBack knockBack;
    
    public event Action OnPlayerTakeHit;
    public event Action OnPlayerDeath;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
        
        PlayerData.MaxHealth = playerSo.maxHealth;
        PlayerData.Health = playerSo.maxHealth;
        PlayerData.MoveSpeed = playerSo.moveSpeed;
        PlayerData.IsAlive = true;
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (knockBack.GettingKnockedBack)
        {
            return;
        }

        HandleMovement();
    }

    public void TakeDamage(Transform damageSource, int damage)
    {
        if (PlayerData.CanTakeDamage && PlayerData.IsAlive)
        {
            OnPlayerTakeHit?.Invoke();
            PlayerData.CanTakeDamage = false;
            PlayerData.Health -= damage;
            Debug.Log(PlayerData.Health);
            knockBack.GetKnockedBack(damageSource);

            StartCoroutine(DamageRecoveryRoutine());
        }

        DetectDeath();
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(playerSo.damageRecoveryTime);
        PlayerData.CanTakeDamage = true;
    }

   private void DetectDeath()
    {
        if (PlayerData.Health <= 0 && PlayerData.IsAlive)
        {
            PlayerData.IsAlive = false;
            knockBack.StopKnockBackMovement();
            DisableMovement();
            OnPlayerDeath?.Invoke();
        }
    }

    private void DisableMovement()
    {
        GetComponent<PlayerInput>().enabled = false; // Отключаем ввод
    }

    private void HandleMovement()
    {
        rb.MovePosition(rb.position + inputVector * (PlayerData.MoveSpeed * Time.fixedDeltaTime));

        PlayerData.InputVector = inputVector;

        PlayerData.IsRunning = inputVector.sqrMagnitude > minMovingSpeed * minMovingSpeed;
        
        PlayerData.ScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
    }

    // получение вектора движения игрока
    private void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>().normalized;
    }
    
    private void OnFire()
    {
        // ActiveWeapon.Instance.GetActiveWeapon().Attack();
        Debug.Log("Fire");
        /*if (nextAttackTime > Time.time)
            return;

        nextAttackTime = Time.time + AttackCooldown;
        OnPlayerAttack?.Invoke();*/
    }
}