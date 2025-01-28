using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Player player;
    private PlayerData playerData;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private const string IS_RUNNING = "IsRunning";
    private const string TAKE_HIT = "TakeHit";
    private const string IS_DIE = "IsDie";

    private void Awake()
    {
        playerData = player.PlayerData;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerData.OnInputVectorChanged += Move;
        playerData.OnScreenPositionChanged += AdjustPlayerFacingDirection;
        player.OnPlayerTakeHit += PlayerTakeHit;
        player.OnPlayerDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        playerData.OnInputVectorChanged -= Move;
        playerData.OnScreenPositionChanged -= AdjustPlayerFacingDirection;
        player.OnPlayerTakeHit -= PlayerTakeHit;
        player.OnPlayerDeath -= PlayerDeath;
    }

    private void Move(Vector2 inputVector)
    {
        animator.SetBool(IS_RUNNING, playerData.IsRunning);
    }

    private void Update()
    {
        /*
        if (Player.Instance.IsAlive())
        {
            AdjustPlayerFacingDirection();
        }*/
    }

    private void PlayerTakeHit()
    {
        animator.SetTrigger(TAKE_HIT);
    }

    private void PlayerDeath()
    {
        animator.SetBool(IS_DIE, true);
    }
    


    private void AdjustPlayerFacingDirection(Vector2 screenPosition)
    {
        Vector2 mousePosition = GameInput.Instance.GetMousePosition();

        spriteRenderer.flipX = mousePosition.x < screenPosition.x;
    }
}