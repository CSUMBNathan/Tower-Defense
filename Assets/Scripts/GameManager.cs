using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static  bool GameIsOver;

    public GameObject gameOverUI;
    public GameObject completeLevelUI;
    
    void Start()
    {
        GameIsOver = false;
    }

    void Update()
    {
        if(GameIsOver)
            return;
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    public void WinLevel()
    {
        GameIsOver = true;
        completeLevelUI.SetActive(true);
    }

    void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
