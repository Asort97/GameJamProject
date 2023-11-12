using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    [Serializable]
    public struct SwordStates
    {
        public string anim;
        public float attackRange;
    }

    [SerializeField] private Animator swordAnimator;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject minaPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform anchorPoint;

    [Space(5)]
    [SerializeField] private float forceBullet;
    [SerializeField] private float forceDash;
    [SerializeField] private float timeForDash;

    [Space(5)]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float minaCooldown;
    [SerializeField] private float dashCooldown;

    [SerializeField] private float attackDamage;
    [SerializeField] private float shootDamage;

    [Space(5)]
    [SerializeField] private SwordStates[] swordStates;

    [Space(5)]

    private Rigidbody2D rb;

    private float attackCd;
    private float shootCd;
    private float minaCd;
    private float dashCd;
    private int currentSwordState;
    private MeleeDetector meleeDetector;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meleeDetector = GetComponentInChildren<MeleeDetector>();
    }

    private void Update()
    {
        Melee();
        Ranged();
        Dash();
        MinaSpawn();
    }

    private void Melee()
    {
        if(attackCd <= 0f)
        {
            if(Input.GetMouseButton(0))
            {
                swordAnimator.SetTrigger(swordStates[currentSwordState].anim);
                
                if(meleeDetector.EnemiesObjects.Length != 0)
                {
                    foreach (var enemy in meleeDetector.EnemiesObjects)
                    {
                        if(enemy.TryGetComponent(out IDamageble damagable))
                        {
                            damagable.TakeDamage(attackDamage);
                        }
                    }
                }            

                attackCd = attackCooldown;
            }
        }

        else
        {
            attackCd -= Time.deltaTime;
        }
    }
    
    private void Dash()
    {
        if(dashCd <= 0f)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(DashCO());

                dashCd = dashCooldown;
            }
        }    
        else
        {
            dashCd -= Time.deltaTime;
        }
    }
    
    IEnumerator DashCO()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.AddForce(direction * forceDash, ForceMode2D.Impulse);
        yield return new WaitForSeconds(timeForDash);
        rb.velocity = Vector2.zero;
    }

    private void MinaSpawn()
    {
        if(minaCd <= 0f)
        {
            if(Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse2))
            {
                Instantiate(minaPrefab, transform.position, Quaternion.identity, null);

                minaCd = minaCooldown;
            }
        }
        else
        {
            minaCd -= Time.deltaTime;
        }
    }

    private void Ranged()
    {
        if(shootCd <= 0f)
        {
            if(Input.GetMouseButton(1))
            {
                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation, null);

                bullet.GetComponent<Bullet>().Init(forceBullet);

                shootCd = shootCooldown;
            }
        }

        if(shootCd >= 0f)
        {
            shootCd -= Time.deltaTime;
        }
    }   
}
