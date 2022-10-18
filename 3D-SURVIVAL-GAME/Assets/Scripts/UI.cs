using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{

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
            returnToMenu();

        }
    }

    public void returnToMenu()
    {
        PlayerPrefs.DeleteAll();
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }

    public void LoadNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        print(currentLevel);
        SceneManager.LoadScene(currentLevel+1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll();
    }
}
