using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Text counter;
    public int countKills;
    public static Action OnChange;

    public static float DruzokDamage = 1;
    public static float PirozokDamage  = 3;
    public static float StrelDamage = 1;

    private void OnEnable()
    {
        Enemy.OnDie += AddCountKill;
    }

    private void OnDisable()
    {
        Enemy.OnDie -= AddCountKill;
    }

    private void AddCountKill()
    {
        countKills++;
        counter.text = $"KILLS: {countKills}";
        if(countKills % 7 == 0)
        {
            OnChange?.Invoke();
        }
    }
}
