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
    private void FixedUpdate() // ѕул пули
    {
        if(transform.position.x > 24 || transform.position.x < -24)
        {
            this.gameObject.SetActive(false);
            
        }
        if(transform.position.y > 17 || transform.position.y < -17)
        {
            this.gameObject.SetActive(false);
        }
    }
}
