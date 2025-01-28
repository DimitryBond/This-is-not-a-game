/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SatyrVampir.Utils;
using UnityEngine.Video;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private State startingState;
    [SerializeField] private float roamingDistanceMax = 7f;
    [SerializeField] private float roamingDistanceMin = 3f;
    [SerializeField] private float roamingTimerMax = 2f;
    [SerializeField] private bool isChasingEnemy = false; // флаг является ли враг приследующим
    [SerializeField] private float chaisingDistanse = 4f; // растояние на котором начнется приследование
    [SerializeField] private float chaisingSpeedMultiplier = 2f; // во соклько раз увеличится скорость анимации
    
    [SerializeField] private bool isAttackingEnemy = false; // флаг является ли враг атакующим
    [SerializeField] private float attackingDistanse = 2f; // расстояние на котором будет атаковать 
    [SerializeField] private float attackRate = 2f; // время между атаками?
    private float nextAttackTime = 0f;
    
    private NavMeshAgent navMeshAgent;
    private State currentState;
    private float roamingTimer;
    private Vector3 targetPosition;
    private Vector3 startingPosition;
    
    private float roamingSpeed;
    private float chaisingSpeed;

    public event EventHandler OnEnemyAttack;

    private float nextCheckDirectionTime = 0f; // время проверики направления
    private float checkDirettionDuration = 0.1f; // как часто проверям 
    private Vector3 lastPosition; // последние положение врага
    
    private enum State
    {
        Idle, // покой
        Roaming, // брожение
        Chasing, // приследование
        Attacking, // атаки
        Death //смэрт
    }
    
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); 
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        currentState = startingState;
        
        roamingSpeed = navMeshAgent.speed;
        chaisingSpeed = navMeshAgent.speed * chaisingSpeedMultiplier;
    }

    private void Update()
    {
        //StateHandler();
        //MovementDirectionHandler();
    }

    public void SetDeathState()
    {
        navMeshAgent.ResetPath();
        currentState = State.Death;
    }
    private void StateHandler()
    {
        switch (currentState)
        {
            case State.Roaming:
                roamingTimer -= Time.deltaTime;
                if (roamingTimer <= 0)
                {
                    Roaming();
                    roamingTimer = roamingTimerMax;
                }
                CheckCurrentState();
                break;
            case State.Chasing:
                ChasingTarget();
                CheckCurrentState();
                break; 
            case State.Attacking:
                AttackingTarget();
                CheckCurrentState();
                break; 
            case State.Death:
                break; 
            default: 
            case State.Idle:
                break;    
        }
    }

    private void ChasingTarget()
    {
        navMeshAgent.SetDestination(Player.Instance.transform.position); 
    }

    public float GetRoamingAnimationSpeed()
    {
        return navMeshAgent.speed / roamingSpeed;
            
    }

    private void CheckCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.Instance.transform.position);
        State newState = State.Roaming; // по умолчанию бродит

        if (isChasingEnemy)
        {
            if (distanceToPlayer < chaisingDistanse)
            {
                newState = State.Chasing;
            }
        }

        if (isAttackingEnemy)
        {
            if (distanceToPlayer <= attackingDistanse)
            {
                newState = State.Attacking;
            } 
        }

        if (newState != currentState)
        {
            if (newState == State.Chasing)
            {
                navMeshAgent.ResetPath();
                navMeshAgent.speed = chaisingSpeed;
            }
            else if (newState == State.Roaming)
            {
                navMeshAgent.speed = roamingSpeed;
                roamingTimer = 0f;
            }
            else if (newState == State.Attacking)
            {
                navMeshAgent.ResetPath();
            }
            
            currentState = newState;
        }


    }

    private void AttackingTarget()
    {
        if (Time.time > nextAttackTime)
        {
            OnEnemyAttack?.Invoke(this, EventArgs.Empty);

            nextAttackTime = Time.time + attackRate;
        }
        
    }

    private void MovementDirectionHandler()
    {
        if (Time.time > nextCheckDirectionTime)
        {
            if (IsRunning())
            {
                ChangeFacingDirection(lastPosition, transform.position);
            } 
            else if (currentState == State.Attacking)
            {
                ChangeFacingDirection(transform.position, Player.Instance.transform.position);
            }
            
            lastPosition = transform.position;
            nextCheckDirectionTime = Time.time + checkDirettionDuration;
        }
    }
    
    public bool IsRunning()
    {
        // можно написать через get, те сделать свойством
        // если скорость агента равна нулю тогда он не бегит
        if (navMeshAgent.velocity == Vector3.zero)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Roaming()
    {
        startingPosition = transform.position;
        targetPosition = GetRoamingPosition();
        navMeshAgent.SetDestination(targetPosition);
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Utils.GetRandomDir() * UnityEngine.Random.Range(roamingDistanceMin, roamingDistanceMax);
    }

    private void ChangeFacingDirection(Vector3 sourceDirection, Vector3 targetPosition)
    {
        if (sourceDirection.x > targetPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
*/
