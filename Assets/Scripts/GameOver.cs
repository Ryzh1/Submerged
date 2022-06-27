using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject player;

    public GameObject gameOverScreen;

    void Update()
    {
        if(player == null)
        {
            Invoke("ShowGameOverScreen", 1f);
        }
    }

    void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

}
