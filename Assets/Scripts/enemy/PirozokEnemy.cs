using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirozokEnemy : Enemy
{
    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PushEnemy());
            other.gameObject.GetComponent<IDamageble>().TakeDamage(BattleManager.PirozokDamage);
        }
    }

    // IEnumerator waitForStop()
    // {
    //     Vector2 repelDirection = transform.position - playerTransform.position;
    //     rb.AddForce(repelDirection * repelForce, ForceMode2D.Impulse);
    //     yield return new WaitForSeconds(0.2f);

    //     rb.velocity = Vector2.zero;
    // }
}
