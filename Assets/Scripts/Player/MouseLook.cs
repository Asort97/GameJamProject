using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform anchorTransform;
    private Vector2 mousePos;

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Look();
    }
    
    private void Look()
    {
        Vector2 lookDir = mousePos - (Vector2)transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 180f;

        anchorTransform.rotation = Quaternion.Euler(0,0,angle);
    }
}
