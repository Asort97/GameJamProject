using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour, IDamageble
{

    public static Action OnDie;
    public AudioClip dieSound;
    public float attackPushDistance = 1f; // Расстояние отталкивания
    public float attackPushDuration = 0.5f; // Продолжительность отталкивания

    public Transform body;
    public Transform playerTransform;
    public float stopDistance;
    public float moveSpeed;
    public GameObject particles;
    public Animator anim;
    public float attackCooldown; 
    public Rigidbody2D rb;
    public float damage;
    public float attackInterval;
    public bool isStop;
    public float health = 100;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;    
    }

    public virtual void Move()
    {
        if(Vector2.Distance(transform.position, playerTransform.position) > stopDistance)
        {
            isStop = false;

            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
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
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        StartCoroutine(PushEnemy());
        Instantiate(particles, transform.position, Quaternion.identity, null);

        if(health <= 0f)
        {
            OnDie?.Invoke();

            AudioManager.instance.PlaySoundDie();

            Destroy(gameObject);
        }
    }

    public IEnumerator PushEnemy()
    {
        Vector2 originalPosition = transform.position;
        Vector2 pushDirection = (transform.position - playerTransform.position).normalized;

        float elapsedTime = 0f;

        while (elapsedTime < attackPushDuration)
        {
            float t = elapsedTime / attackPushDuration;
            transform.position = Vector2.Lerp(originalPosition, originalPosition + pushDirection * attackPushDistance, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
