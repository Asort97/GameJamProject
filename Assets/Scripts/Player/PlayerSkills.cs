using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    private MeleeDetector meleeDetector;

    private void Start()
    {
        meleeDetector = GetComponentInChildren<MeleeDetector>();
    }

    private void Update()
    {
        Melee();
    }

    public void Melee()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(meleeDetector.EnemiesObjects.Length != 0)
            {
                foreach (var enemy in meleeDetector.EnemiesObjects)
                {
                    // урон каждому врагу
                }
            }            
        }
    }

    public void Ranged()
    {
        
    }
}
