using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    private CapsuleCollider2D capsuleCollider2D;

    private void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            Debug.Log("collision");
            player.TakeDamage(transform, 1);
        }
    }
    
}
