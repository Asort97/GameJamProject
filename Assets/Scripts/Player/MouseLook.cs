using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private Vector2 mousePos;

    private void Start()
    {
        
    }
    private void Update()
    {
         mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        Look();
    }

    private void Look()
    {
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
