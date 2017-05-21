using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Transform[] spawnpoints;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private float spawnTimeDelay;
    [SerializeField]
    private float maxEnemies = 7;
    private float currEnemies;

    void Start () {
        InvokeRepeating("spawnEnemy", 0.01f,spawnTimeDelay);
    }

    void spawnEnemy()
    {
        if (maxEnemies > currEnemies)
        {
            int spawnPoint = Random.Range(0, 7);

            Instantiate(enemies[0], spawnpoints[spawnPoint].position, spawnpoints[spawnPoint].rotation);
            currEnemies++;
        }
    }
}
