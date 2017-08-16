using System.Collections;
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
    private const float playerLevelModifier = 1.5f;

    // Static game components
    //public DayNightCycle dayManager;
    public Text enemyCountHUD;
    [SerializeField]
    private Player[] playerList;
    private Player player;
    [SerializeField]
    private GameCamera topDownCam;

    private MusicManager musicManager;

    void OnEnable () {
        // Spawns the player into the game based on menu selection; 
        player = Instantiate(playerList[PlayerPrefs.GetInt("playerLoadout")], new Vector3(0f, 1f, 0f), Quaternion.Euler(0f, 0f, 0f) );
        GameObject musicObj = GameObject.FindGameObjectWithTag("musicManager");
        if (musicObj) { musicManager = musicObj.GetComponent<MusicManager>();  musicManager.toggleMenuToGameMusic(true); }
        topDownCam.setPlayerTarget(player);
        enemyProbability = baseEnemyProbability;
        StartCoroutine(initSpawnTime());
    }

    private void OnDisable()
    {
        if (!musicManager) { return; }
        musicManager.toggleMenuToGameMusic(false);
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
        int levelWeight =  Mathf.FloorToInt(player.getPlayerLevel() * playerLevelModifier);

        // Enemy type based on current probability and player level
        int enemyType = Random.Range(0, enemyProbability + levelWeight /*+ dayManager.getDaysPassed()*/);

        if (enemyType == 0)
        {
            // Spawn tamadra
            return 0;
        }
        else if( enemyType <= 30)
        {
            // Spawn easy enemy
            return 1;
        }
        else if( enemyType <= 60)
        {
            // Spawn medium enemy
            return 2;
        }
        else
        {
            // Spawn hard enemy
            return 3;
        }
    }

    public float getSpawnTime()
    {
        return spawnTimeDelay;
    }

    public void setSpawnTime(float _newSpawnDelay)
    {
        Debug.Log(_newSpawnDelay);
        spawnTimeDelay = _newSpawnDelay;
    }
}
