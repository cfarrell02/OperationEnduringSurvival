using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float maxSpawnArea;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private GameObject playerCapsule;
    public int maxEnemyCount = 20, spawnRate = 2;
    private int totalEnemies = 0;
    private float timeLastSpawned;
    
    // Start is called before the first frame update
    void Start()
    {
        timeLastSpawned = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (totalEnemies < maxEnemyCount && Time.time-timeLastSpawned>spawnRate)
        {
            SpawnEnemy(1);
            timeLastSpawned = Time.time;
        }
    }

    public void ProcessChildDeath()
    {
        totalEnemies--;
    }

    public void SpawnEnemy(int numOfEnemies)
    {
        for (int i = 0; i < numOfEnemies; ++i)
        {
            Vector3 offSet = new Vector3(Random.Range(-maxSpawnArea, maxSpawnArea),
                0, Random.Range(-maxSpawnArea, maxSpawnArea));
            GameObject enemy = Instantiate(enemies[0], this.transform.position + offSet
                , Quaternion.identity, this.transform);

            FollowScript fs = enemy.GetComponent<FollowScript>();
            fs.target = playerCapsule;
            fs.waypoints = wayPoints;

        }
        totalEnemies += numOfEnemies;
    }
}
