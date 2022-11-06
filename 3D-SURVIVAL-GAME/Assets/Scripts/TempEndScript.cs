using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempEndScript : MonoBehaviour
{
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadNextLevel(int timeToWait)
    {
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(timeToWait);
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(currentIndex + 1);
        

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "PlayerCapsule")
        {
            print("Loading level " + SceneManager.GetActiveScene().buildIndex + 1);   
            StartCoroutine(LoadNextLevel(4));
            
        }
    }
}
