using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void Init(float Force)
    {   
        rb.AddForce(transform.up * Force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.TryGetComponent(out IDamageble damagable))
        {
            damagable.TakeDamage(3);
        }
    }
}
