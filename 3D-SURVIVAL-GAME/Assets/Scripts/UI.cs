using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI : MonoBehaviour
{

    bool paused = false;
    public Animator uiAnimator;
    public TextMeshProUGUI difficulty;

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

    public void OpenSettings()
    {
        uiAnimator.SetBool("settingsOpen", true);
    }
    public void CloseSettings()
    {
        uiAnimator.SetBool("settingsOpen", false);
    }
    public void AdjustDifficulty(int level)
    {
        print(level);
        PlayerPrefs.SetInt("Difficulty", level);
        difficulty.SetText("Difficulty (" + level+")");
        
    }
}
