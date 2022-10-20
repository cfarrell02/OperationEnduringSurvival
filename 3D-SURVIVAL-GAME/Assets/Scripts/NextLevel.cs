using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    //Lift Script
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpen", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadNextLevel(int timeToWait)
    {
        animator.SetBool("isOpen", false);
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
