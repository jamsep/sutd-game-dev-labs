using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HUDManager : MonoBehaviour
{

    public TextMeshProUGUI endScoreText;
    public TextMeshProUGUI scoreText;

    public GameObject gameOverUI;
    public GameObject inGameUI;

    public GameObject highscoreText;
    public IntVariable gameScore;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        // other instructions
        // subscribe to events


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
