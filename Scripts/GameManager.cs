﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private Transform[] spawnpoints;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private float spawnTimeDelay;
    [SerializeField]
    private float maxEnemies;
    private float currEnemies;
    private const int baseEnemyProbability = 41;
    private int enemyProbability;

    public Text enemyCountHUD;
    private Player player;

    void OnEnable () {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemyProbability = baseEnemyProbability;
        StartCoroutine(initSpawnTime());
    }

    void Update()
    {
        updateEnemyCount();
    }

    private void updateEnemyCount()
    {
        enemyCountHUD.text = "Current Enemies: " + currEnemies;
    }

    void spawnEnemy()
    {
        if (maxEnemies + player.getPlayerLevel() > currEnemies)
        {
            int spawnPointPos = getSpawnPosition();
            int enemyType = getEnemyType();

            Instantiate(enemies[enemyType], spawnpoints[spawnPointPos].position, spawnpoints[spawnPointPos].rotation);
            currEnemies++;
        }
    }

    public void destroyEnemy()
    {
        currEnemies--;
    }

    IEnumerator initSpawnTime()
    {
        while( true )
        {
            spawnEnemy();
            yield return new WaitForSeconds(spawnTimeDelay);
        }
    }

    private int getSpawnPosition()
    {
        return Random.Range(0, spawnpoints.Length - 1);
    }

    private int getEnemyType()
    {
        int enemyType = Random.Range(0, enemyProbability);

        if( enemyType <= 40)
        {
            return 0;
        }
        else if( enemyType <= 80)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}