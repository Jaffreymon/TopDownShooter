using System.Collections;
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
    private float maxEnemies = 5;
    private float currEnemies;

    public Text enemyCountHUD;

    private Player player;

    void OnEnable () {
        InvokeRepeating("spawnEnemy", 0.01f,spawnTimeDelay);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            int spawnPoint = Random.Range(0, 7);

            Instantiate(enemies[0], spawnpoints[spawnPoint].position, spawnpoints[spawnPoint].rotation);
            currEnemies++;
        }
    }

    public void destroyEnemy()
    {
        currEnemies--;
    }
}
