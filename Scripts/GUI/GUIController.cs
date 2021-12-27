using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GUIController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI title;
    public TextMeshProUGUI btnText;
    public GameObject gameOverPanel;

    void Awake()
    {
        GameManager.scoreUpdatedListener += UpdateScore;
        GameManager.gameOverListener += ShowGameOverPanel;
        GameManager.gameWonListener += ShowGameWonPanel;
    }

    private void ShowGameWonPanel()
    {
        if(gameOverPanel == null){
            Debug.LogError("Missing panel");
            return;
        }
        btnText.text = "One More Time!";
        title.text = "congratulation!";
        gameOverPanel.SetActive(true);
    }


    private void ShowGameOverPanel()
    {
        if(gameOverPanel == null){
            Debug.LogError("Missing panel");
            return;
        }
        btnText.text = "Try Again?";
        title.text = "Game Over!";
        gameOverPanel.SetActive(true);
    }

    public void StartGameRtn(){
        GameManager.Instance.StartGame();
        gameOverPanel.SetActive(false);
    }

    private void UpdateScore(int score)
    {
        if(scoreText == null){
            Debug.LogError("Missing score text");
            return;
        }
        scoreText.text = score.ToString();
    }
}
