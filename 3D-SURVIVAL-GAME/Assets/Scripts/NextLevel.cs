using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadNextLevel(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentIndex + 1);

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "PlayerCapsule")
        {
            StartCoroutine(LoadNextLevel(4));
        }
    }
}
