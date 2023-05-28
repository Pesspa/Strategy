using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject[] pointsOfSpawn;
    public int enemyCount;
    public GameObject enemyPrefab;
    public Enemy enemy;
    void FixedUpdate()
    {
        Check();
    }
    private void Check()
    {
        enemy = FindObjectOfType<Enemy>();
        if(enemy == null) 
        {
            Spawn(enemyCount);
        }
    }
    private void Spawn(int countOfEnemies)
    {
        enemyCount++;
        for(int i = 0; i < countOfEnemies; i += 3)
        {
            Instantiate(enemyPrefab, pointsOfSpawn[Random.Range(0, pointsOfSpawn.Length)].transform.position, Quaternion.identity);
        }
    }
}
