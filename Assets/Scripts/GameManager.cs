using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public int score;
    public int enemiesDefeated;
    public int chestsFound;

    public Time levelTime;

    public  TMP_Text enemyCountText;
    public TMP_Text chestCountText;
    public TMP_Text scoreCountText;

    public int currentLevelNumber;

    

    private static GameObject gmInstance;
    
    //void Awake()
    //{
    //    //DontDestroyOnLoad(this);

    //    int numGameManagers = FindObjectsOfType<GameManager>().Length;
    //    if (numGameManagers != 1)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        CalculateTotalScore();
        DisplayStats();

        

    }

    void DisplayStats()
    {
        scoreCountText.text = score.ToString();
        enemyCountText.text = "x" + enemiesDefeated.ToString();
        chestCountText.text = "x" + chestsFound.ToString();
    }

    void CalculateTotalScore()
    {
        score = (enemiesDefeated * 50) + (chestsFound * 250);
    }
}
