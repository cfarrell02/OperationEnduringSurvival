using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healthValue;
    public AudioClip healthSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.name != "PlayerCapsule") return;
        GameObject player = other.gameObject;
        PlayerHealth health = player.GetComponent<PlayerHealth>();
        if (health.addHealth(healthValue))
        {
            audioSource.PlayOneShot(healthSound);
            Destroy(gameObject);
        }

    }
}
