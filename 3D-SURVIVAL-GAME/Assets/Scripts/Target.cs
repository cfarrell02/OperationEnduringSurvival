using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 100f;
    public SpawnPoint parentSpawnPoint;

    public bool TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
            return true;
        }
        return false;
    }

    void Die()
    {
        StartCoroutine(parentSpawnPoint.ProcessChildDeath());
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty");
        health += 20 * difficulty;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
