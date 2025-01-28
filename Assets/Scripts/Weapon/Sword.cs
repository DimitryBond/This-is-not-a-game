using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class Sword : MonoBehaviour
{
    [SerializeField] private int damageAmount = 2; 
    public event EventHandler OnSwordSwing;
    
    private PolygonCollider2D swordCollider;

    private void Awake()
    {
        swordCollider = GetComponent<PolygonCollider2D>(); // иициализируем и кэшируем
    }

    private void Start()
    {
        AttackColliderTurnOff(); // при старте открючаем коллайдер
    }
    
    public void Attack()
    {
        AttackColliderTurnOffOn(); // при атаке включаем коллайдер
        OnSwordSwing?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity))
        {
            enemyEntity.TakeDamage(damageAmount);
        }
    }

    public void AttackColliderTurnOff() // чтобы мы могли отключить его через визуал
    {
        swordCollider.enabled = false;
    }

    private void AttackColliderTurnOn()
    {
        swordCollider.enabled = true;
    }

    private void AttackColliderTurnOffOn() // чтобы атака срабатывала даже если анимация не закончилась 
    {
        AttackColliderTurnOff();
        AttackColliderTurnOn();
    }
}
