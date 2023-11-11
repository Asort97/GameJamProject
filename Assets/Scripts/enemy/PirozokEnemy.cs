using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirozokEnemy : Enemy
{
    [SerializeField] private GameObject bulletPrefab;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
    }

    public override void Attack()
    {
        
    }
}
