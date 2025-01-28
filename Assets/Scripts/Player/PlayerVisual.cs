using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerVisual : MonoBehaviour
{
    private Animator animator;
    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";
    private const string TAKE_HIT = "TakeHit";
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());

        if (Player.Instance.IsAlive())
        {
            AdjustPlayerFacingDirection();
        }
    }


    private void Start()
    {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        Player.Instance.OnPlayerTakeHit += Player_OnPlayerTakeHit;
    }

    private void Player_OnPlayerTakeHit(object sender, EventArgs e)
    {
        animator.SetTrigger(TAKE_HIT);
    }

    private void Player_OnPlayerDeath(object sender, EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
    }


    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePosition = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if (mousePosition.x < playerPosition.x)
        {
            spriteRenderer.flipX = true;
        }
        else {
            spriteRenderer.flipX = false;
        }
    }
}
