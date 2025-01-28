using System;
using UnityEngine;

// чтобы всегда был на обекте
[RequireComponent(typeof(Animator))] 
[RequireComponent(typeof(SpriteRenderer))] 
public class BatVisual : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private EnemyEntity enemyEntity;
    private Animator animator;
    private const string IS_RUNNING = "IsRunning";
    private const string TAKE_HIT = "TakeHit";
    private const string IS_DIE = "IsDie";
    private const string CHAISING_SPEED_MULPITLIER = "ChaisingSpeedMultiplier";
    private const string ATTACK = "Attack";
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        enemyAI.OnEnemyAttack += enemyAI_OnEnemyAttack;
        enemyEntity.OnTakeHit += enemyEntity_OnTakeHit;
        enemyEntity.OnDeath += enemyEntity_OnDeath;
    }

    private void enemyEntity_OnDeath(object sender, EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
        spriteRenderer.sortingOrder = 1;
    }

    private void enemyEntity_OnTakeHit(object sender, EventArgs e)
    {
        animator.SetTrigger(TAKE_HIT);
    }

    private void OnDestroy()
    {
        enemyAI.OnEnemyAttack -= enemyAI_OnEnemyAttack;
    }

    private void enemyAI_OnEnemyAttack(object sender, EventArgs e)
    {
        animator.SetTrigger(ATTACK);
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, enemyAI.IsRunning());
        animator.SetFloat(CHAISING_SPEED_MULPITLIER, enemyAI.GetRoamingAnimationSpeed());
    }

    public void TriggerAttackAnimationTurnOff()
    {
        enemyEntity.PolygonColliderTurnOff();
    }
    
    public void TriggerAttackAnimationTurnOn()
    {
        enemyEntity.PolygonColliderTurnOn();
    }
}
