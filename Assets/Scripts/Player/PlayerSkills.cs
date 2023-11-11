using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Transform anchorPoint;

    [Space(5)]
    [SerializeField] private float forceBullet;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float shootCooldown;
    [SerializeField] private float minaCooldown;
    [SerializeField] private float dashCooldown;

    [SerializeField] private float attackDamage;
    [SerializeField] private float shootDamage;

    [Space(5)]
    [SerializeField] private SwordStates[] swordStates;

    [Space(5)]

    private float attackCd;
    private float shootCd;
    private float minaCd;
    private float dashCd;
    private int currentSwordState;
    private MeleeDetector meleeDetector;

    private void Start()
    {
        meleeDetector = GetComponentInChildren<MeleeDetector>();
    }

    private void Update()
    {
        Melee();
        Ranged();
    }

    public void Melee()
    {
        if(attackCd <= 0f)
        {
            if(Input.GetMouseButtonDown(0))
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

    public void Ranged()
    {
        if(shootCd <= 0f)
        {
            if(Input.GetMouseButtonDown(1))
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
