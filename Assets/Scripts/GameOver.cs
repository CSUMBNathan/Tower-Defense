using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;

    public TextMeshProUGUI roundsText;
    public string mainMenuScene = "MainMenu";

    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();
    }

    public void Retry()
    {
        
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);    }

    public void Menu()
    {
        sceneFader.FadeTo(mainMenuScene);
    }
}
