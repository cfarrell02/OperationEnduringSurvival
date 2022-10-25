using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

    bool paused = false;
    public Animator uiAnimator;

    private void Awake()
    {
        GameObject[] ui = GameObject.FindGameObjectsWithTag("UI");
        if (ui.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Pause();
        }
    }

    public void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        print(currentLevel);
        SceneManager.LoadScene(currentLevel+1);
    }
    public void returnToMenu()
    {
        PlayerPrefs.DeleteAll();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
    void Pause()
    {
        print(paused);
        if (paused) paused = false;
        else paused = true;

        if (!paused)
        {
            Time.timeScale = 0;
            uiAnimator.SetBool("isPaused", true);
        }
        else
        {
            Time.timeScale = 1;
            uiAnimator.SetBool("isPaused", false);
        }
    }
}
