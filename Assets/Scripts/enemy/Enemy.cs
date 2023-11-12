using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageble
{
    public Transform playerTransform;
    public float repelForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackCooldown; 
    public Rigidbody2D rb;
    public float damage;
    private float attackInterval;
    private float health = 100;

    public virtual void Move()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) > 0.1f)
        {
            Vector2 directionToPlayer = (Vector2)playerTransform.position - rb.position;

            rb.MovePosition(rb.position + directionToPlayer * moveSpeed * Time.deltaTime);

            // transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
    }

    public virtual void Attack()
    {
        // Debug.Log($"Attack");
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0f)
        {
            Destroy(gameObject);
        }
    }

}
