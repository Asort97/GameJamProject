using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDetector : MonoBehaviour
{
    public float raduisCast;
    [SerializeField] private LayerMask layerEnemyMask;
    [SerializeField] private GameObject swordBack;
    public Collider2D[] EnemiesObjects;

    private void Update()
    {
        EnemiesObjects = Physics2D.OverlapCircleAll(transform.position, raduisCast, layerEnemyMask);
    }

    public void DisableSword()
    {
        swordBack.SetActive(false);
    }
    public void EnableSword()
    {
        swordBack.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, raduisCast);        
    }
}
