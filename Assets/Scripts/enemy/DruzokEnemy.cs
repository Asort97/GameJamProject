using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DruzokEnemy : Enemy
{
    private void Update()
    {
        Move();
        Attack();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PushEnemy());
            other.gameObject.GetComponent<IDamageble>().TakeDamage(BattleManager.DruzokDamage);
        }
    }
}
