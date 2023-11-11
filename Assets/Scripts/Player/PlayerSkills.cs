using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
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

    private float attackCd;
    private float shootCd;
    private float minaCd;
    private float dashCd;

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
                if(meleeDetector.EnemiesObjects.Length != 0)
                {
                    foreach (var enemy in meleeDetector.EnemiesObjects)
                    {
                        if(enemy.TryGetComponent(out IDamageble damagable))
                        {
                            damagable.TakeDamage(attackDamage);
                        }
                    }

                    attackCd = attackCooldown;
                }            
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
