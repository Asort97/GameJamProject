using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{
    public GameObject tutor;
    public GameObject spawner;
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            tutor.SetActive(false);
            spawner.SetActive(true);
        }
    }
}
