using System;
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
    // private bool isRunning = false;
    
    private Vector2 inputVector;
    private Rigidbody2D rb;
    private KnockBack knockBack;
    
    // public event EventHandler OnPlayerDeath;
    // public event EventHandler OnPlayerTakeHit;
    
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

    /*public void TakeDamage(Transform damageSourse, int damage)
    {
        if (canTakeDamage && PlayerData.IsAlive)
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
        yield return new WaitForSeconds(playerSo.damageRecoveryTime);
        canTakeDamage = true;
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0 && PlayerData.IsAlive)
        {
            PlayerData.IsAlive = false;
            knockBack.StopKnockBackMovement();
            DisableMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
        }
    }

    private void DisableMovement()
    {
        GetComponent<PlayerInput>().enabled = false; // Отключаем ввод
    }
*/
    private void HandleMovement()
    {
        rb.MovePosition(rb.position + inputVector * (PlayerData.MoveSpeed * Time.fixedDeltaTime));

        PlayerData.InputVector = inputVector;

        PlayerData.IsRunning = Math.Abs(inputVector.x) > minMovingSpeed || Math.Abs(inputVector.y) > minMovingSpeed;
        
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