using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    public float attackDistance = 5f;
    public float retreatDistance = 3f;
    public float attackNewCooldown = 2f;
    public float shootCooldown = 3f;
    public float moveSpeed = 5f;
    public Transform playerTransform;

    private float timeSinceLastAttack = 0f;
    private float timeSinceLastShoot = 0f;

    private enum BossState
    {
        Idle,
        Attack,
        Retreat,
        Shoot,
        Approach
    }

    private BossState currentState = BossState.Idle;

    void Update()
    {
        switch (currentState)
        {
            case BossState.Idle:
                if (Vector3.Distance(transform.position, playerTransform.position) < attackDistance)
                {
                    currentState = BossState.Attack;
                }
                else if (timeSinceLastShoot > 5f)
                {
                    currentState = BossState.Attack;
                }
                break;

            case BossState.Attack:
                Move();
                if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
                {
                    currentState = BossState.Retreat;
                }
                else if (Time.time - timeSinceLastAttack > attackNewCooldown)
                {
                    Attack();
                    timeSinceLastAttack = Time.time;
                }
                break;

            case BossState.Retreat:
                MoveAwayFromPlayer();
                if (Vector3.Distance(transform.position, playerTransform.position) < retreatDistance)
                {
                    currentState = BossState.Shoot;
                }
                break;

            case BossState.Shoot:
                MoveAwayFromPlayer();
                if (Time.time - timeSinceLastShoot > shootCooldown)
                {
                    Shoot();
                    timeSinceLastShoot = Time.time;
                }
                else if (Vector3.Distance(transform.position, playerTransform.position) > retreatDistance)
                {
                    currentState = BossState.Approach;
                }
                break;

            case BossState.Approach:
                Move();
                if (Vector3.Distance(transform.position, playerTransform.position) < attackDistance)
                {
                    currentState = BossState.Attack;
                }
                break;
        }
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }

    void MoveAwayFromPlayer()
    {
        Vector3 direction = transform.position - playerTransform.position;
        direction.Normalize();
        Vector3 targetPosition = transform.position + direction * retreatDistance;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void Attack()
    {
        playerTransform.GetComponent<IDamageble>().TakeDamage(10);
    }

    void Shoot()
    {
        // Логика для выстрела
    }

}
