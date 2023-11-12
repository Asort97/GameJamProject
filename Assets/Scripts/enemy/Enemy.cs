using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageble
{
    public Transform body;
    public Transform playerTransform;
    public float repelForce;
    public float stopDistance;
    [SerializeField] private float moveSpeed;
    public Animator anim;
    public float attackCooldown; 
    public Rigidbody2D rb;
    public float damage;
    public float attackInterval;
    public bool isStop;
    private float health = 100;

    public virtual void Move()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) > stopDistance)
        {
            isStop = false;

            anim.SetBool("isRunning", true);

            Vector2 directionToPlayer = (Vector2)playerTransform.position - rb.position;

            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isRunning", false);

            isStop = true;
        }
    }
    
    public virtual void FlipToPlayer()
    {
        float dot = Vector2.Dot(playerTransform.position-transform.position, transform.right);

        if(dot > 0f)
        {
            body.eulerAngles = new Vector2(0, 180f);
            
        }
        else if(dot < 0f)
        {
            body.eulerAngles = new Vector2(0, 0f);
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
