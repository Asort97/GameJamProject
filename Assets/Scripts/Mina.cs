using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mina : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float timerToExplode;  
    [SerializeField] private float raduis;  
    [SerializeField] private LayerMask layer;
    [SerializeField] private Animator anim;

    private void Start()
    {
        StartCoroutine(waitForExplode());
    }

    IEnumerator waitForExplode()
    {
        yield return new WaitForSeconds(timerToExplode);

        anim.SetTrigger("isExplode");
    }

    public void Explode()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, raduis, layer);

        foreach (var enemy in enemies)
        {
            if (enemy.TryGetComponent(out IDamageble damageble))
            {
                damageble.TakeDamage(damage);
            }
        }
    }
    
    public void ExplodeDestroy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, raduis);
    }
}
