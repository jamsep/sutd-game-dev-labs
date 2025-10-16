using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // events
    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent gamePause;
    public UnityEvent gameResume;
    public IntVariable gameScore;
    public GameConstants gc;

    private bool isPaused = false;

    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        SceneManager.activeSceneChanged += SceneSetup;
    }

    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(gameScore.Value);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        // reset score
        gameScore.Value = 0;
        SetScore(gameScore.Value);
        gameRestart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        gameScore.ApplyChange(increment);
        SetScore(gameScore.Value);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(gameScore.Value);
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    public void GamePause()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        gamePause.Invoke();
    }

    private void GameResume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        gameResume.Invoke();
    }

    public void GamePauseToggle()
    {
        if (isPaused)
        {
            GameResume();
        }
        else
        {
            GamePause();
        }
    }


    public void DecreaseLife()
    {
        if (gc == null)
        {
            Debug.LogError("GameManager: GameConstants not assigned in inspector.");
            return;
        }

        gc.currentlives = Mathf.Max(0, gc.currentlives - 1);

        Debug.Log($"Lives left: {gc.currentlives}");

        if (gc.currentlives <= 0)
        {
            GameOver();
        }

    }

    public void IncreaseLife(int amount = 1)
    {
        if (gc == null)
        {
            Debug.LogError("GameManager: GameConstants not assigned in inspector.");
            return;
        }

        gc.currentlives = Mathf.Min(gc.maxLives, gc.currentlives + amount);
        Debug.Log($"Lives left: {gc.currentlives}");
    }
}