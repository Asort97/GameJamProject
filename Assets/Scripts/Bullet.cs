using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private float currentDamage;

    public void Init(float Force, float damage)
    {   
        currentDamage = damage;

        rb.AddForce(transform.up * Force, ForceMode2D.Impulse);


        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out IDamageble damagable))
        {
            damagable.TakeDamage(currentDamage);

            Destroy(gameObject);
        }
    }
}
