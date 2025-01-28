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
    //private const string IS_DIE = "IsDie";
    //private const string TAKE_HIT = "TakeHit";
    

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
    }
    
    private void OnDisable()
    {
        playerData.OnInputVectorChanged -= Move;
        playerData.OnScreenPositionChanged += AdjustPlayerFacingDirection;
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


    /*
    private void Start()
    {
        //Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
        //Player.Instance.OnPlayerTakeHit += Player_OnPlayerTakeHit;
    }

    private void Player_OnPlayerTakeHit(object sender, EventArgs e)
    {
        animator.SetTrigger(TAKE_HIT);
    }

    private void Player_OnPlayerDeath(object sender, EventArgs e)
    {
        animator.SetBool(IS_DIE, true);
    }
    */


    private void AdjustPlayerFacingDirection(Vector2 screenPosition)
    {
        Vector2 mousePosition = GameInput.Instance.GetMousePosition();

        spriteRenderer.flipX = mousePosition.x < screenPosition.x;
    }
}