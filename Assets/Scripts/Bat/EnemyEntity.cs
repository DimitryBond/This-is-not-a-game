/*
using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))] // чтобы всегда был на обекте
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyAI))]
public class EnemyEntity : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    public event EventHandler OnTakeHit;
    public event EventHandler OnDeath;
    
    private int currentHalth; // текущее здоровье врага
    private PolygonCollider2D polyCollider2D;
    private CapsuleCollider2D capsuleCollider2D;
    private EnemyAI enemyAI;
    
    private void Start()
    {
        currentHalth = enemySO.enemyHealth; // инициализируем максимального здоровья врага
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            //player.TakeDamage(transform, enemySO.enemyDamageAmount);
        }
    }

    private void Awake()
    {
        polyCollider2D = GetComponent<PolygonCollider2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        enemyAI = GetComponent<EnemyAI>();
    }
    
    // получение урона
    public void TakeDamage(int damage)
    {
        currentHalth -= damage;
        OnTakeHit?.Invoke(this, EventArgs.Empty);
        DetectDeath();
    }
    
    public void PolygonColliderTurnOff()
    {
        polyCollider2D.enabled = false;
    }
    
    public void PolygonColliderTurnOn()
    {
        polyCollider2D.enabled = true;
    }
    
    private void DetectDeath()
    {
        if (currentHalth <= 0)
        {
            capsuleCollider2D.enabled = false;
            polyCollider2D.enabled = false;

            enemyAI.SetDeathState();
            
            OnDeath?.Invoke(this, EventArgs.Empty);
            //Destroy(gameObject);
        }
    }

    
    
    
}
*/
