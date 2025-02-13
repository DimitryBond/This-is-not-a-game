/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public static ActiveWeapon Instance {get; private set;}
    [SerializeField] private Sword sword;

    private void Awake()
    {
        Instance = this;
    }
    
    public Sword GetActiveWeapon()
    {
        return sword;
    }

    private void Update()
    {
        if (Player.Instance.IsAlive())
        {
            FollowMouswPosition();
        }
    }

    private void FollowMouswPosition()
    {
        Vector3 mousePosition = GameInput.Instance.GetMousePosition();
        Vector3 playerPosition = Player.Instance.GetPlayerScreenPosition();

        if (mousePosition.x < playerPosition.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
*/
