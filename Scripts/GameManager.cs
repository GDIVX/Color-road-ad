using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action gameOverListener;
    public static Action<int> scoreUpdatedListener;
    internal static Action gameWonListener;
    public static Action gameStart;

    public static GameManager Instance;

    public int score = 0;

    void Awake()
    {
        if(Instance == null) Instance = this;
        ColorCollision.OnColorCollision += OnColorColision;
    }

    void Start()
    {
        Time.timeScale = 0;
        SceneManager.LoadSceneAsync("Play Scene" , LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("UI" , LoadSceneMode.Additive);
    }

    private void OnColorColision(ColorCollision a, ColorCollision b)
    {
        b.Kill();

        if(b.isRamp){
            //change color of a
            a.HandleRampInteraction(b.MyColor);
            return;
        }

        if(a.MyColor == b.MyColor){
            score += 5;
            scoreUpdatedListener?.Invoke(score);

            return;
        }
        a.Kill();
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameOverListener?.Invoke();

    }

    internal void StartGame()
    {
        gameStart?.Invoke();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        score = 0;
        Time.timeScale = 1;
    }
}
