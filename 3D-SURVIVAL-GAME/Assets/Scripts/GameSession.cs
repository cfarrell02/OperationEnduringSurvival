using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    //Things to be saved
    public GameObject[] inventory;


    // Start is called before the first frame update
    void Start()
    {
        int numOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numOfGameSessions > 1)
        {
            Destroy(gameObject);

        }
        else
        {

            DontDestroyOnLoad(gameObject);
        }
        foreach (GameObject item in inventory)
        {

                    DontDestroyOnLoad(item);

            }
        }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
