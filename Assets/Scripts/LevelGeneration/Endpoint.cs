using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Endpoint : MonoBehaviour
{
    //The goal for all normal levels, when the player collides or presses a button it will take them to the results screen and load the next level
    public bool toMainGame = false;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {         
            if(!toMainGame)
            {
                //Go To End Screen
                SceneManager.LoadScene(3);
            }
            else
            {

                //Go To Level
                SceneManager.LoadScene(2);

                if(gm.currentLevelNumber == 2)
                {
                    SceneManager.LoadScene(2);
                }
            }

        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
