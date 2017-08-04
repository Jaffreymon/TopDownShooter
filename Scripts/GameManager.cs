using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // Interacted game components
    [SerializeField]
    private Transform[] spawnpoints;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private float spawnTimeDelay;
    [SerializeField]
    private float maxEnemies;
    [SerializeField]
    private Transform enemyList;

    // Private game variables
    private const int baseEnemyProbability = 41;
    private int enemyProbability;
    private float playerLevelModifier = 0.33f;
    private float dayCountModifier = 0.25f;

    // Static game components
    public DayNightCycle dayManager;
    public Text enemyCountHUD;
    [SerializeField]
    private Player[] playerList;
    private Player player;

    void OnEnable () {
        //PlayerPrefs.GetInt("playerLoadout")
        // Spawns the player into the game based on menu selection; 
        player = Instantiate(playerList[PlayerPrefs.GetInt("playerLoadout")], new Vector3(0f, 1f, 0f), Quaternion.Euler(0f, 0f, 0f) );
        enemyProbability = baseEnemyProbability;
        StartCoroutine(initSpawnTime());
    }

    void Update()
    {
        updateEnemyCount();
    }

    private void updateEnemyCount()
    {
        enemyCountHUD.text = "Current Enemies: " + enemyList.childCount;
    }

    void spawnEnemy()
    {
        if (maxEnemies + player.getPlayerLevel() > enemyList.childCount)
        {
            int spawnPointPos = getSpawnPosition();
            int enemyType = getEnemyType();

            GameObject freshSpawn = Instantiate(enemies[enemyType], spawnpoints[spawnPointPos].position, spawnpoints[spawnPointPos].rotation);
            freshSpawn.transform.SetParent(enemyList);
        }
    }

    IEnumerator initSpawnTime()
    {
        while( true )
        {
            //spawnEnemy();
            yield return new WaitForSeconds(spawnTimeDelay);
        }
    }

    // Gets random spawn point on the map
    private int getSpawnPosition()
    {
        return Random.Range(0, spawnpoints.Length - 1);
    }

    // Gets random enemy type to spawn
    private int getEnemyType()
    {
        // Enemy type based on current probability, player level, and day count
        int levelWeight =  Mathf.FloorToInt(player.getPlayerLevel() * playerLevelModifier);
        int dayWeight = Mathf.FloorToInt(player.getPlayerLevel() * dayCountModifier);
        int enemyType = Random.Range(0, enemyProbability + levelWeight + dayWeight);

        if( enemyType <= 40)
        {
            return 0;
        }
        else if( enemyType <= 60)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public float getSpawnTime()
    {
        return spawnTimeDelay;
    }

    public void setSpawnTime(float _newSpawnDelay)
    {
        spawnTimeDelay = _newSpawnDelay;
    }
}
