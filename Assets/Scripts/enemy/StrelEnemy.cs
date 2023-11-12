using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrelEnemy : Enemy
{
    public float speedBullet;
    public GameObject bulletPref;
    public Transform anchorTransform;
    public Transform shootPoint;

    private void Update()
    {
        Vector2 lookDir = playerTransform.position - transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;

        anchorTransform.rotation = Quaternion.Euler(0,0,angle);

        
        FlipToPlayer();
        Attack();
        Move();
        
    }

    public override void Attack()
    {
        if(attackInterval <= 0f && isStop)
        {
            GameObject bullet = Instantiate(bulletPref, shootPoint.position, shootPoint.rotation, null);
            bullet.GetComponent<Bullet>().Init(speedBullet);

            attackInterval = attackCooldown;
        }
        
        if(attackInterval >= 0f)
        {
            attackInterval -= Time.deltaTime;
        }
    }
}
