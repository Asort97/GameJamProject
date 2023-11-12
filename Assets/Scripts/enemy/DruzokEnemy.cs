using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DruzokEnemy : Enemy
{
    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
        Attack();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log($"GIVEVIEIV");
            other.gameObject.GetComponent<IDamageble>().TakeDamage(damage);

            StartCoroutine(waitForStop());
        }
    }

    IEnumerator waitForStop()
    {
        Vector2 repelDirection = transform.position - playerTransform.position;
        rb.AddForce(repelDirection * repelForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);

        rb.velocity = Vector2.zero;
    }

}
