using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackgroundAudio : MonoBehaviour
{
    private float timeUntilNextSound = 5f, startTime;
    private AudioSource audioSource;
    [SerializeField] private AudioClip zombieNoise;
    [SerializeField] private AudioClip[] levelNoise;
    private int levelIndex;
    // Start is called before the first frame update
    void Start()
    {
        levelIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        startTime = Time.time;
        print(levelIndex - 1 <= levelNoise.Length);
        if (levelIndex - 1 <= levelNoise.Length) {
            audioSource.clip = levelNoise[levelIndex - 1];
            audioSource.Play();
        }
        else
            audioSource.clip = null;
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
