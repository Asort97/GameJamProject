using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject druzokEnemy;
    [SerializeField] private GameObject pirozokEnemy;
    [SerializeField] private GameObject strelEnemy;

    public float spawnInterval;

    public Transform[] spawnPoints;

    private void Start()
    {
        StartCoroutine(SpawnerEnemies());
    }

    public void Spawn()
    {
        int i = Random.Range(0, spawnPoints.Length);
        int chance = Random.Range(0,100);

        
        if(chance < 75)
        {
            Instantiate(druzokEnemy, spawnPoints[i].position, Quaternion.identity);
        }

        if(chance >= 75 & chance < 90)
        {
            Instantiate(strelEnemy, spawnPoints[i].position, Quaternion.identity);
        }

        if(chance >= 90)
        {
            Instantiate(pirozokEnemy, spawnPoints[i].position, Quaternion.identity);
        }
    }

    IEnumerator SpawnerEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            Spawn();
        }
        
    }
}
