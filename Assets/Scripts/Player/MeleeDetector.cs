using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDetector : MonoBehaviour
{
    [SerializeField] private float raduisCast;
    [SerializeField] private LayerMask layerEnemyMask;
    public Collider2D[] EnemiesObjects;

    private void Update()
    {
        EnemiesObjects = Physics2D.OverlapCircleAll(transform.position, raduisCast, layerEnemyMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, raduisCast);        
    }
}
