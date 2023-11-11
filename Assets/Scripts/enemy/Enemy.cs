using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackCooldown; 
    private float attackInterval;

    public virtual void Move()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    public virtual void Attack()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) <= 0.2f)
        {
            if(attackInterval <= 0f)
            {
                Debug.Log($"Attack");

                attackInterval = attackCooldown;
            }
        }

        if(attackInterval >= 0f)
        {
            attackInterval -= Time.deltaTime;
        }
    }
}
