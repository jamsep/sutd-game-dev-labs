using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI scoreText;


    public GameObject gameOverUI;
    public GameObject inGameUI;

    public GameObject highscoreText;
    public IntVariable gameScore;
    

    void Awake()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (GameManager.instance != null || currentScene != "MainMenu")
        {
            GameManager.instance.gameStart.AddListener(GameStart);
            GameManager.instance.gameOver.AddListener(GameOver);
            GameManager.instance.gameRestart.AddListener(GameStart);
            GameManager.instance.scoreChange.AddListener(SetScore);
        }
    }

        // Start is called before the first frame update
    void Start()
    {
        GameObject mainMenu = GameObject.FindWithTag("MainMenu");
        if (mainMenu != null)
        {
            InitializeForMainMenu();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeForMainMenu()
    {
        // Hide game over UI, show appropriate menu UI
        if (gameOverUI != null) gameOverUI.SetActive(false);
        if (inGameUI != null) inGameUI.SetActive(false);

        // You might want to show high score in main menu
        highscoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
        highscoreText.SetActive(true);
    }

    public void GameStart()
    {
        // hide gameover panel
        inGameUI.SetActive(true);
        gameOverUI.SetActive(false);
    }

    public void SetScore(int score)
    {
        print(score);
        endScoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }


    public void GameOver()
    {
        inGameUI.SetActive(false);
        gameOverUI.SetActive(true);
        // set highscore
        highscoreText.GetComponent<TextMeshProUGUI>().text = "TOP- " + gameScore.previousHighestValue.ToString("D6");
        // show
        highscoreText.SetActive(true);
    }
}
