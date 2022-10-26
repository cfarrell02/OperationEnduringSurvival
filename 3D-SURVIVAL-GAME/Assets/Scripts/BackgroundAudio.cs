using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    private float timeUntilNextSound = 5f, startTime;
    private AudioSource audioSource;
    [SerializeField] private AudioClip zombieNoise;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= timeUntilNextSound)
        {
            audioSource.PlayOneShot(zombieNoise,.1f);
            timeUntilNextSound = Random.Range(5, 60);

        }
    }

}
