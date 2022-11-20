using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float maxSpawnArea = 10, playerSpawnLimit = 40,minSpawnLimitOffset = 0, respawnCooldown = 60;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private GameObject playerCapsule;
    public int maxEnemyCount = 20, spawnRate = 2;
    private int totalEnemies = 0;
    private float timeLastSpawned;
    
    // Start is called before the first frame update
    void Start()
    {
        timeLastSpawned = Time.time;
        //Vector3[] corners = new Vector3[4];
        //corners[0] = transform.position + new Vector3(maxSpawnArea, 0, maxSpawnArea);
        //corners[1] = transform.position + new Vector3(maxSpawnArea, 0, -maxSpawnArea);
        //corners[2] = transform.position + new Vector3(-maxSpawnArea, 0 - maxSpawnArea);
        //corners[3] = transform.position + new Vector3(-maxSpawnArea, 0, maxSpawnArea);
        //for(int i = 0; i < corners.Length - 1; ++i)
        //{
        //    print(corners[i]);
        //    Debug.DrawLine(corners[i], corners[i + 1], Color.green);
        //}
        if(playerCapsule==null)
        playerCapsule = FindObjectOfType<PlayerInventory>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float minDistance = Mathf.Sqrt(2 * (Mathf.Pow(maxSpawnArea / 2, 2))) + minSpawnLimitOffset;
        float playerDistance = Vector3.Distance(playerCapsule.transform.position, this.transform.position);
        bool isEligible = playerDistance > minDistance && playerDistance <= playerSpawnLimit;
        if (isEligible && totalEnemies < maxEnemyCount && Time.time-timeLastSpawned>spawnRate)
        {
            SpawnEnemy(1);
            timeLastSpawned = Time.time;
        }
    }

    public IEnumerator ProcessChildDeath()
    {
        yield return new WaitForSeconds(respawnCooldown);
        totalEnemies--;
    }

    

    public void SpawnEnemy(int numOfEnemies)
    {
        for (int i = 0; i < numOfEnemies; ++i)
        {
            Vector3 offSet = new Vector3(Random.Range(-maxSpawnArea, maxSpawnArea),
                0, Random.Range(-maxSpawnArea, maxSpawnArea));
            GameObject enemy = Instantiate(enemies[0], this.transform.position + offSet
                , Quaternion.identity);

            FollowScript fs = enemy.GetComponent<FollowScript>();
            enemy.GetComponent<Target>().parentSpawnPoint = this;
            fs.target = playerCapsule;
            fs.waypoints = wayPoints;

        }
        totalEnemies += numOfEnemies;
    }
}
