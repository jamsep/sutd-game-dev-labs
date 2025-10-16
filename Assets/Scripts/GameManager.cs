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
    public bool IsPaused { get { return isPaused; } }
    private AudioSource[] allAudioSources;

    void Start()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
        SceneManager.activeSceneChanged += SceneSetup;
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    public void SceneSetup(Scene current, Scene next)
    {
        gameStart.Invoke();
        SetScore(gameScore.Value);
        allAudioSources = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameRestart()
    {
        // reset score
        gameScore.SetValue(0);
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
        DecreaseLife();
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    public void GamePause()
    {
        Time.timeScale = 0.0f;
        isPaused = true;
        PauseAllAudio();
        gamePause.Invoke();
    }

    private void GameResume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        ResumeAllAudio();
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

    public void PauseAllAudio()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in allAudioSources)
        {
            if (source.isPlaying)
                source.Pause();
        }
    }

    public void ResumeAllAudio()
    {
        allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (var source in allAudioSources)
        {
            if (source != null)
                source.UnPause();
        }
    }


    public void DecreaseLife()
    {
        if (gc == null)
        {
            Debug.LogError("GameManager: GameConstants not assigned in inspector.");
            return;
        }

        gc.currentLives = Mathf.Max(0, gc.currentLives - 1);
        Debug.Log($"Lives left: {gc.currentLives}");

    }

    public void IncreaseLife(int amount = 1)
    {
        if (gc == null)
        {
            Debug.LogError("GameManager: GameConstants not assigned in inspector.");
            return;
        }

        gc.currentLives = Mathf.Min(gc.maxLives, gc.currentLives + amount);
        Debug.Log($"Lives left: {gc.currentLives}");
    }
}